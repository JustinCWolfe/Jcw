using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using Jcw.Common;
using Jcw.Charting.Interfaces;
using Jcw.Charting.Gui.WinForms.Interfaces;
using Jcw.Charting.Metadata;
using Jcw.Common.Gui.WinForms.Controls;

namespace Jcw.Charting.Gui.WinForms.Controls
{
    public partial class JcwChartMetadataCtl : JcwUserControl, IJcwChartDockPanelControl, IJcwChartMetadataSave
    {
        #region Constructors

        public JcwChartMetadataCtl ()
        {
            InitializeComponent ();

            // don't auto generate data grid view columns when binding to datasources below
            NoteDataGridView.AutoGenerateColumns = false;
            VerticalLinesDataGridView.AutoGenerateColumns = false;
            HorizontalLinesDataGridView.AutoGenerateColumns = false;

            ChartNoteXColumn.DataPropertyName = "XCoordinateValue";
            ChartNoteYColumn.DataPropertyName = "YCoordinateValue";
            ChartNoteTextColumn.DataPropertyName = "NoteText";

            ChartVerticalLinesXColumn.DataPropertyName = "CoordinateValue";
            ChartVerticalLinesTextColumn.DataPropertyName = "Text";

            ChartHorizontalLinesYColumn.DataPropertyName = "CoordinateValue";
            ChartHorizontalLinesTextColumn.DataPropertyName = "Text";

            AutoHide = true;
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose (bool disposing)
        {
            if (disposing && (components != null))
            {
                Metadata = null;
                ChartForm = null;
                ChartControl = null;

                components.Dispose ();
            }

            base.Dispose (disposing);
        }

        #endregion

        #region IJcwChartMetadataSave Implementation

        #region Properties

        public IJcwChartFrm ChartForm { get; set; }

        #endregion

        #region Public Methods

        public void SaveRequired (bool saveRequired, ChartMetadata metadata)
        {
            if (saveRequired)
            {
                Metadata = metadata;

                // rebind data grid view to chart metadata binding lists
                NoteDataGridView.DataSource = GetFilteredChartNotes ();
                VerticalLinesDataGridView.DataSource = GetFilteredChartVerticalLines ();
                HorizontalLinesDataGridView.DataSource = GetFilteredChartHorizontalLines ();
            }
        }

        public void BeginInit ()
        {
            Metadata = null;
        }

        public void EndInit ()
        {
            // rebind data grid view to chart metadata binding lists
            NoteDataGridView.DataSource = GetFilteredChartNotes ();
            VerticalLinesDataGridView.DataSource = GetFilteredChartVerticalLines ();
            HorizontalLinesDataGridView.DataSource = GetFilteredChartHorizontalLines ();

            NoteDataGridView.Invalidate ();
            VerticalLinesDataGridView.Invalidate ();
            HorizontalLinesDataGridView.Invalidate ();
        }

        #endregion

        #endregion

        #region IJcwChartDockPanelControl Implementation

        #region Properties

        public bool AutoHide { get; set; }
        public string PanelCaption { get; set; }
        public IJcwChartCtl ChartControl { get; set; }
        public ChartMetadata Metadata { get; set; }

        #endregion

        #endregion

        #region Event Handlers

        private void JcwChartMetadataCtl_Load (object sender, EventArgs e)
        {
            // If saving chart metadata is not enabled, do not show the metadata delete buttons.
            ChartNoteDeleteColumn.Visible = ChartForm.CanSaveChartMetadata;
            ChartVerticalLinesDeleteColumn.Visible = ChartForm.CanSaveChartMetadata;
            ChartHorizontalLinesDeleteColumn.Visible = ChartForm.CanSaveChartMetadata;

            NoteDataGridView.DataSource = GetFilteredChartNotes ();
            VerticalLinesDataGridView.DataSource = GetFilteredChartVerticalLines ();
            HorizontalLinesDataGridView.DataSource = GetFilteredChartHorizontalLines ();

            NoteDataGridView.Refresh ();
            VerticalLinesDataGridView.Refresh ();
            HorizontalLinesDataGridView.Refresh ();
        }

        private void NoteDataGridView_CellClick (object sender, DataGridViewCellEventArgs e)
        {
            // if the click occurred on a data grid view row (not on the header) and on the delete chart note column
            if (e.RowIndex >= 0 && e.ColumnIndex == ChartNoteDeleteColumn.Index)
            {
                DataGridViewRow clickedRow = NoteDataGridView.Rows[e.RowIndex];

                // assemble arguments to pass to remove note method
                Note n = clickedRow.DataBoundItem as Note;
                double xCoordinate = Convert.ToDouble (clickedRow.Cells[ChartNoteXColumn.Name].Value);
                double yCoordinate = Convert.ToDouble (clickedRow.Cells[ChartNoteYColumn.Name].Value);

                // remove vertical line from metadata and remove grid view row
                Metadata.RemoveChartNote (xCoordinate, n.XCoordinate.AxisTitle, yCoordinate, n.YCoordinate.AxisTitle, n.MarkColor, n.NoteText);
                NoteDataGridView.Rows.Remove (clickedRow);

                ChartForm.ChartMetadataSaveRequired (Metadata);

                // set the current cell to the cell in the first column of the first visible row
                int firstVisibleRowIndex = NoteDataGridView.Rows.GetFirstRow (DataGridViewElementStates.Visible);
                if (firstVisibleRowIndex != -1)
                {
                    NoteDataGridView.CurrentCell = NoteDataGridView[0, firstVisibleRowIndex];
                }
            }
        }

        private void VerticalLinesDataGridView_CellClick (object sender, DataGridViewCellEventArgs e)
        {
            // if the click occurred on a data grid view row (not on the header) and on the delete vertical marker column
            if (e.RowIndex >= 0 && e.ColumnIndex == ChartVerticalLinesDeleteColumn.Index)
            {
                DataGridViewRow clickedRow = VerticalLinesDataGridView.Rows[e.RowIndex];

                // assemble arguments to pass to remove vertical line method
                Line l = clickedRow.DataBoundItem as Line;
                double xCoordinate = Convert.ToDouble (clickedRow.Cells[ChartVerticalLinesXColumn.Name].Value);

                // remove vertical line from metadata and remove grid view row
                Metadata.RemoveVerticalLine (xCoordinate, l.Coordinate.AxisTitle);
                VerticalLinesDataGridView.Rows.Remove (clickedRow);

                ChartForm.ChartMetadataSaveRequired (Metadata);

                // set the current cell to the cell in the first column of the first visible row
                int firstVisibleRowIndex = VerticalLinesDataGridView.Rows.GetFirstRow (DataGridViewElementStates.Visible);
                if (firstVisibleRowIndex != -1)
                {
                    VerticalLinesDataGridView.CurrentCell = VerticalLinesDataGridView[0, firstVisibleRowIndex];
                }
            }
        }

        private void HorizontalLinesDataGridView_CellClick (object sender, DataGridViewCellEventArgs e)
        {
            // if the click occurred on a data grid view row (not on the header) and on the delete horizontal marker column
            if (e.RowIndex >= 0 && e.ColumnIndex == ChartHorizontalLinesDeleteColumn.Index)
            {
                DataGridViewRow clickedRow = HorizontalLinesDataGridView.Rows[e.RowIndex];

                // assemble arguments to pass to remove horizontal line method
                Line l = clickedRow.DataBoundItem as Line;
                double yCoordinate = Convert.ToDouble (clickedRow.Cells[ChartHorizontalLinesYColumn.Name].Value);

                // remove horizontal line from metadata and remove grid view row
                Metadata.RemoveHorizontalLine (yCoordinate, l.Coordinate.AxisTitle);
                HorizontalLinesDataGridView.Rows.Remove (clickedRow);

                ChartForm.ChartMetadataSaveRequired (Metadata);

                // set the current cell to the cell in the first column of the first visible row
                int firstVisibleRowIndex = HorizontalLinesDataGridView.Rows.GetFirstRow (DataGridViewElementStates.Visible);
                if (firstVisibleRowIndex != -1)
                {
                    HorizontalLinesDataGridView.CurrentCell = HorizontalLinesDataGridView[0, firstVisibleRowIndex];
                }
            }
        }

        #endregion

        #region Private Methods

        private BindingList<Note> GetFilteredChartNotes ()
        {
            BindingList<Note> filteredChartNotes = new BindingList<Note> ();

            if (!Metadata.AreNotesVisible)
            {
                return filteredChartNotes;
            }

            // filter out chart notes that aren't on this chart - only chart notes that were added
            // to this specific chart should be visible
            foreach (Note n in Metadata.ChartNoteBindingList)
            {
                if (n.XCoordinate.AxisTitle == ChartControl.XAxisTitle && n.YCoordinate.AxisTitle == ChartControl.YAxisTitle)
                {
                    filteredChartNotes.Add (n);
                }
            }

            return filteredChartNotes;
        }

        private BindingList<Line> GetFilteredChartHorizontalLines ()
        {
            BindingList<Line> filteredChartHorizontalLines = new BindingList<Line> ();

            if (!Metadata.AreHorizontalLinesVisible)
            {
                return filteredChartHorizontalLines;
            }

            // filter out chart horizontal lines that aren't on this chart - only chart horizontal lines that were added
            // to this specific chart should be visible
            foreach (Line l in Metadata.ChartHorizontalLineBindingList)
            {
                if (l.Coordinate.AxisTitle == ChartControl.YAxisTitle)
                {
                    filteredChartHorizontalLines.Add (l);
                }
            }

            return filteredChartHorizontalLines;
        }

        private BindingList<Line> GetFilteredChartVerticalLines ()
        {
            BindingList<Line> filteredChartVerticalLines = new BindingList<Line> ();

            // filter out chart vertical lines that aren't on this chart - only chart vertical lines that were added
            // to this specific chart should be visible
            foreach (Line l in Metadata.ChartVerticalLineBindingList)
            {
                // if the vertical line has text like e# it is an event line so always include it
                if (!string.IsNullOrEmpty (l.Text) && Regex.IsMatch (l.Text, @"^e\d+$"))
                {
                    filteredChartVerticalLines.Add (l);
                }
                // not an event line so we need to check if this vertical line belongs on our chart and also if vertical lines are visible
                else if (l.Coordinate.AxisTitle == ChartControl.YAxisTitle && Metadata.AreVerticalLinesVisible)
                {
                    filteredChartVerticalLines.Add (l);
                }
            }

            return filteredChartVerticalLines;
        }

        #endregion
    }
}
