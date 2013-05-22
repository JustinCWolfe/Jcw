using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Jcw.Common.Gui.WinForms.Controls;

namespace Jcw.Charting.Gui.WinForms.Controls
{
    public partial class JcwAggregateIncludeDataGridView : JcwDataGridView
    {
        #region Events

        public EventHandler OnRecalculateAggregate;

        #endregion

        #region Constructors

        public JcwAggregateIncludeDataGridView ()
            : base ()
        {
            InitializeComponent ();
        }

        #endregion

        #region Event Handlers

        private void GridView_CurrentCellDirtyStateChanged (object sender, EventArgs e)
        {
            JcwDataGridView gridView = sender as JcwDataGridView;
            if (gridView != null)
            {
                // Validate that the row being edited in fact can be edited by checking the CanIncludeInAggregate property on the data bound object.
                ChartStatistic statistic = gridView.CurrentRow.DataBoundItem as ChartStatistic;
                if (statistic != null)
                {
                    if (statistic.CanIncludeInAggregate)
                    {
                        // Commit the change when a checkbox cell is clicked so that by the time we get into the CellContentClick 
                        // handler the cells value has already been updated.
                        if (gridView.CurrentCell is DataGridViewCheckBoxCell)
                        {
                            gridView.CommitEdit (DataGridViewDataErrorContexts.Commit);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Adding this handler so that we can prevent double click from toggling check box cell state. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridView_CellContentDoubleClick (object sender, DataGridViewCellEventArgs e)
        {
            GridViewClickHandler (sender, e);
        }

        private void GridView_CellContentClick (object sender, DataGridViewCellEventArgs e)
        {
            GridViewClickHandler (sender, e);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Method to perform actions that should occur after derived class data view objects are initialized completely.
        /// </summary>
        public void AfterInitialize ()
        {
            Columns.Add (IncludeColumn);
            IncludeColumn.DataPropertyName = "IncludeInAggregate";
        }

        #endregion

        #region Private Methods

        private void GridViewClickHandler (object sender, DataGridViewCellEventArgs e)
        {
            JcwDataGridView gridView = sender as JcwDataGridView;
            if (gridView != null && gridView.CurrentRow != null)
            {
                // Validate that the row being edited in fact can be edited by checking the CanIncludeInAggregate property on the data bound object.
                ChartStatistic statistic = gridView.CurrentRow.DataBoundItem as ChartStatistic;
                if (statistic != null)
                {
                    if (statistic.CanIncludeInAggregate)
                    {
                        // If the current cell is the include column cell, raise the recalculate aggregate event.
                        if (gridView.CurrentCell.ColumnIndex.Equals (IncludeColumn.Index))
                        {
                            if (OnRecalculateAggregate != null)
                            {
                                OnRecalculateAggregate (this, EventArgs.Empty);
                            }
                        }
                    }
                    else
                    {
                        gridView.CancelEdit ();
                    }
                }
            }
        }

        #endregion
    }
}
