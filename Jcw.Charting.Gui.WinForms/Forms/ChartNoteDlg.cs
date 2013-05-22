using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Jcw.Charting.Metadata;
using Jcw.Common.Gui.WinForms.Forms;
using Jcw.Resources.Properties;

namespace Jcw.Charting.Gui.WinForms.Forms
{
    partial class ChartNoteDlg : JcwBaseFixedStyleFrm
    {
        #region Properties

        internal Note ChartNote { get; set; }

        #endregion

        #region Constructors

        public ChartNoteDlg ()
        {
            InitializeComponent ();
        }

        #endregion

        #region Event Handlers

        private void ChartNoteDlg_Load (object sender, EventArgs e)
        {
            this.jcwMemoEdit1.DataBindings.Clear ();
            this.jcwMemoEdit1.DataBindings.Add ("Text", ChartNote, "NoteText");
            this.jcwMemoEdit1.Select ();
        }

        private void AddNoteButton_Click (object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty (this.jcwMemoEdit1.Text))
            {
                this.dxErrorProvider.SetError (this.jcwMemoEdit1, JcwResources.GetString ("MissingChartNoteText"));
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close ();
        }

        private void JcwCancelButton_Click (object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close ();
        }

        #endregion
    }
}
