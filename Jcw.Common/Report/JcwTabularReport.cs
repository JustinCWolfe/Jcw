using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;

using Jcw.Common.Data;

namespace Jcw.Common.Report
{
    public class JcwTabularReport : PrintDocument
    {
        #region Fields

        protected float m_headerHeight = 1;
        protected float m_footerHeight = 1;
        protected float m_maxGroupRowHeight = 1.5f;
        protected float m_minGroupRowHeight = 0.5f;
        protected float m_maxDetailRowHeight = 1.0f;
        protected float m_minDetailRowHeight = 0.2f;
        protected float m_headerReportPaddingHeight = .3f;
        protected float m_groupRowPaddingHeight = .3f;

        protected int m_currentPage;
        protected int m_currentRow;
        protected int m_currentGroupColumn;
        protected int m_currentGroupMember;

        protected Font m_headerFont;
        protected Font m_groupFont;
        protected Font m_detailFont;
        protected Font m_footerFont;
        protected Font m_defaultReportFont = new Font ("Arial", 10, FontStyle.Regular, GraphicsUnit.Point);

        protected Brush m_headerBrush;
        protected Brush m_groupBrush;
        protected Brush m_detailBrush;
        protected Brush m_footerBrush;
        protected Brush m_defaultReportBrush = Brushes.Black;

        protected string m_title;
        protected string m_date;
        protected string m_name;
        protected string m_company;

        protected DataView m_dataView;

        #endregion

        #region Properties

        [Category ("Appearance")]
        protected float HeaderHeight
        {
            get { return m_headerHeight; }
            set { m_headerHeight = value; }
        }

        [Category ("Appearance")]
        [Description ("Specify the space, in inches, between the header and the report text")]
        protected float HeaderReportPaddingHeight
        {
            get { return m_headerReportPaddingHeight; }
            set { m_headerReportPaddingHeight = value; }
        }

        [Category ("Appearance")]
        protected float FooterHeight
        {
            get { return m_footerHeight; }
            set { m_footerHeight = value; }
        }

        [Category ("Appearance")]
        protected float MaxGroupRowHeight
        {
            get { return m_maxGroupRowHeight; }
            set { m_maxGroupRowHeight = value; }
        }

        [Category ("Appearance")]
        protected float MinGroupRowHeight
        {
            get { return m_minGroupRowHeight; }
            set { m_minGroupRowHeight = value; }
        }

        [Category ("Appearance")]
        [Description ("Specify the space, in inches, between the group row and the group member rows text")]
        protected float GroupRowPaddingHeight
        {
            get { return m_groupRowPaddingHeight; }
            set { m_groupRowPaddingHeight = value; }
        }

        [Category ("Appearance")]
        protected float MaxDetailRowHeight
        {
            get { return m_maxDetailRowHeight; }
            set { m_maxDetailRowHeight = value; }
        }

        [Category ("Appearance")]
        protected float MinDetailRowHeight
        {
            get { return m_minDetailRowHeight; }
            set { m_minDetailRowHeight = value; }
        }

        [Category ("Appearance")]
        [Description ("Font used throughout report unless overriden by HeaderFont, FooterFont, DetailFont or a setting in the ColumnInformation instance for a specific Column")]
        public Font DefaultReportFont
        {
            get { return m_defaultReportFont; }
            set { m_defaultReportFont = value; }
        }

        [Category ("Appearance")]
        [Description ("Font used for the page header")]
        public Font HeaderFont
        {
            get
            {
                return (m_headerFont == null) ?
                    m_defaultReportFont :
                    m_headerFont;
            }
            set { m_headerFont = value; }
        }

        [Category ("Appearance")]
        [Description ("Font used for items in the Group Row unless overriden by Font setting in Column Information")]
        public Font GroupFont
        {
            get
            {
                return (m_groupFont == null) ?
                    m_defaultReportFont :
                    m_groupFont;
            }
            set { m_groupFont = value; }
        }

        [Category ("Appearance")]
        [Description ("Font used for items in the Detail Row unless overriden by Font setting in Column Information")]
        public Font DetailFont
        {
            get
            {
                return (m_detailFont == null) ?
                    m_defaultReportFont :
                    m_detailFont;
            }
            set { m_detailFont = value; }
        }

        [Category ("Appearance")]
        [Description ("Font used for the page footer")]
        public Font FooterFont
        {
            get
            {
                return (m_footerFont == null) ?
                    m_defaultReportFont :
                    m_footerFont;
            }
            set { m_footerFont = value; }
        }

        [Browsable (false)]
        [Description ("Brush used throughout report unless overriden by HeaderBrush, FooterBrush, DetailBrush or a setting in the ColumnInformation instance for a specific Column")]
        public Brush DefaultReportBrush
        {
            get { return m_defaultReportBrush; }
            set { m_defaultReportBrush = value; }
        }

        [Browsable (false)]
        [Description ("Brush used for the page header")]
        public Brush HeaderBrush
        {
            get
            {
                return (m_headerBrush == null) ?
                    m_defaultReportBrush :
                    m_headerBrush;
            }
            set { m_headerBrush = value; }
        }

        [Browsable (false)]
        [Description ("Brush used for items in the Group Row unless overriden by Brush setting in Column Information")]
        public Brush GroupBrush
        {
            get
            {
                return (m_groupBrush == null) ?
                    m_defaultReportBrush :
                    m_groupBrush;
            }
            set { m_groupBrush = value; }
        }

        [Browsable (false)]
        [Description ("Brush used for items in the Detail Row unless overriden by Brush setting in Column Information")]
        public Brush DetailBrush
        {
            get
            {
                return (m_detailBrush == null) ?
                    m_defaultReportBrush :
                    m_detailBrush;
            }
            set { m_detailBrush = value; }
        }

        [Browsable (false)]
        [Description ("Brush used for the page footer")]
        public Brush FooterBrush
        {
            get
            {
                return (m_footerBrush == null) ?
                    m_defaultReportBrush :
                    m_footerBrush;
            }
            set { m_footerBrush = value; }
        }

        [Category ("Data")]
        public string Company
        {
            get { return m_company; }
            set { m_company = value; }
        }

        [Category ("Data")]
        public string Title
        {
            get { return m_title; }
            set { m_title = value; }
        }

        [Category ("Data")]
        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        [Category ("Data")]
        public string Date
        {
            get { return m_date; }
            set { m_date = value; }
        }

        [Category ("Data")]
        public DataView DataView
        {
            get { return m_dataView; }
            set { m_dataView = value; }
        }

        #endregion

        #region Methods

        #endregion

        #region PrintDocument Overrides

        protected override void OnBeginPrint (PrintEventArgs e)
        {
            // initialize the current page, row, group column and group member before starting to print
            m_currentPage = 0;
            m_currentRow = 0;
            m_currentGroupColumn = 0;
            m_currentGroupMember = 0;
        }

        protected override void OnPrintPage (PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            g.PageUnit = GraphicsUnit.Inch;

            // convert margins (which are measured in 1/100ths of an inch) to their inch values 
            float leftMargin = (float)e.MarginBounds.Left / 100;
            float topMargin = (float)e.MarginBounds.Top / 100;
            float bottomMargin = (float)e.MarginBounds.Bottom / 100;
            float width = (float)e.MarginBounds.Width / 100;

            float currentPosition = topMargin;

            // increment the current page field since the print page event is fired once for each printed report page
            m_currentPage++;

            // print page header
            RectangleF headerBounds = new RectangleF (leftMargin, topMargin, width, m_headerHeight);
            float headerSize = PrintPageHeader (headerBounds, e, false);

            // advance file position by size of the header and add padding between header and start of report
            currentPosition += headerSize + m_headerReportPaddingHeight;

            // print page footer
            RectangleF footerBounds = new RectangleF (leftMargin, topMargin, width, m_footerHeight);
            footerBounds.Y = topMargin + bottomMargin - footerBounds.Height;

            Dictionary<int, string[]> groupingOptions = (m_dataView as JcwReportDataView).GetReportGroupingOptions ();

            // if there are no groups defined set a base group so that the print page logic will work correctly
            int rootGroupColumnIndex = 0;
            string rootGroupMemberName = "ALL";
            if (groupingOptions.Count == 0)
                groupingOptions.Add (rootGroupColumnIndex, new string[] { rootGroupMemberName });

            for (int groupColumnIndex = m_currentGroupColumn ; groupColumnIndex < groupingOptions.Count ; groupColumnIndex++)
            {
                DataColumn groupColumn = m_dataView.Table.Columns[groupColumnIndex];

                for (int groupMemberIndex = m_currentGroupMember ; groupMemberIndex < groupingOptions[groupColumnIndex].Length ; groupMemberIndex++)
                {
                    string groupMember = groupingOptions[groupColumnIndex][groupMemberIndex];

                    float currentRowHeight = default (float);
                    DataView groupDataView = new DataView (m_dataView.Table);

                    e.HasMorePages = false;

                    // print the report section name (group row) if we are on the first group column and the group column
                    // is not the special group column used in cases where no group columns are defined
                    if ((groupColumnIndex == rootGroupColumnIndex && groupMember == rootGroupMemberName) == false)
                    {
                        groupDataView.RowFilter = string.Format ("{0} = '{1}'", groupColumn.ColumnName, groupMember);

                        // if the current row index is not zero, we stopped printing the previous page due to a detail row printing overflow
                        // in that case, we stored the current row and current group and, since we already printed the group row, we do 
                        // not need to reprint it here so simply advance to the detail row printing section
                        if (m_currentRow == 0)
                        {
                            // calculate size of group header row 
                            currentRowHeight = PrintGroupRow (leftMargin, currentPosition, width, e, groupMember, true);

                            // the group row padding and group row text will fit on the page so advance position by padding and print group header row
                            if (currentPosition + m_groupRowPaddingHeight + currentRowHeight < footerBounds.Y)
                            {
                                // advance the current row position by size of the padding between group row and start of group member text
                                currentPosition += m_groupRowPaddingHeight;
                                currentPosition += PrintGroupRow (leftMargin, currentPosition, width, e, groupMember, false);
                            }
                            else
                            {
                                m_currentGroupMember = groupMemberIndex;
                                m_currentGroupColumn = groupColumnIndex;
                                e.HasMorePages = true;
                                return;
                            }
                        }
                    }

                    // print group member detail rows
                    for (int rowIndex = m_currentRow ; rowIndex < groupDataView.Count ; rowIndex++)
                    {
                        // we are printing detail rows so reset the current row state variable which tracks page overflows for this detail row printing session
                        // if there is a page overflow in this detail row printing session, this state variable will be set to that detail row index and we will 
                        // end printing for the current page
                        m_currentRow = 0;

                        // calculate size of group member detail row 
                        currentRowHeight = PrintDetailRow (leftMargin, currentPosition, width, e, groupDataView[rowIndex], true);

                        // it will fit on the page so print group member detail row
                        if (currentPosition + currentRowHeight < footerBounds.Y)
                            currentPosition += PrintDetailRow (leftMargin, currentPosition, width, e, groupDataView[rowIndex], false);
                        // it won't fit on the page so record current row index so we starting printing in the right spot on the next page
                        else
                        {
                            m_currentGroupMember = groupMemberIndex;
                            m_currentGroupColumn = groupColumnIndex;
                            m_currentRow = rowIndex;
                            e.HasMorePages = true;
                            return;
                        }
                    }
                }
            }
        }

        protected override void OnEndPrint (PrintEventArgs e)
        {
            base.OnEndPrint (e);
        }

        #endregion

        #region Printing Virtual Methods

        protected virtual float PrintPageHeader (RectangleF bounds, PrintPageEventArgs e, bool calculateSizeOnly)
        {
            Graphics g = e.Graphics;

            RectangleF headerTextLayout = new RectangleF (bounds.X, bounds.Y, bounds.Width, bounds.Height);

            string headerTextLeft = string.Join ("\n", new string[] { this.Company, this.Title });
            StringFormat headerStringFormatLeft = new StringFormat (StringFormatFlags.LineLimit);
            headerStringFormatLeft.Alignment = StringAlignment.Near;
            headerStringFormatLeft.Trimming = StringTrimming.EllipsisWord;

            string headerTextRight = string.Join ("\n", new string[] { this.Date, this.Name });
            StringFormat headerStringFormatRight = new StringFormat (StringFormatFlags.LineLimit);
            headerStringFormatRight.Alignment = StringAlignment.Far;
            headerStringFormatRight.Trimming = StringTrimming.EllipsisWord;

            // get the height, critical to use all the same options as the eventual DrawString call
            int charsFitted, linesFilled;
            float headerHeightLeft = g.MeasureString (headerTextLeft, this.HeaderFont, headerTextLayout.Size, headerStringFormatLeft, out charsFitted, out linesFilled).Height;
            float headerHeightRight = g.MeasureString (headerTextRight, this.HeaderFont, headerTextLayout.Size, headerStringFormatRight, out charsFitted, out linesFilled).Height;

            if (!calculateSizeOnly)
            {
                g.DrawString (headerTextLeft, this.HeaderFont, this.HeaderBrush, headerTextLayout, headerStringFormatLeft);
                g.DrawString (headerTextRight, this.HeaderFont, this.HeaderBrush, headerTextLayout, headerStringFormatRight);
            }

            return Math.Min (bounds.Height, Math.Max (headerHeightLeft, headerHeightRight));
        }

        protected virtual float PrintPageFooter (RectangleF bounds, PrintPageEventArgs e, bool calculateSizeOnly)
        {
            Graphics g = e.Graphics;

            string footerText = string.Format ("Page {0}", m_currentPage);
            RectangleF footerTextLayout = new RectangleF (bounds.X, bounds.Y, bounds.Width, bounds.Height);

            StringFormat footerStringFormat = new StringFormat (StringFormatFlags.LineLimit);
            footerStringFormat.Alignment = StringAlignment.Center;
            footerStringFormat.Trimming = StringTrimming.EllipsisWord;

            if (!calculateSizeOnly)
                g.DrawString (footerText, this.FooterFont, this.FooterBrush, footerTextLayout, footerStringFormat);

            return bounds.Height;
        }

        protected virtual float PrintGroupRow (float x, float y, float width, PrintPageEventArgs e, string groupName, bool calculateSizeOnly)
        {
            Graphics g = e.Graphics;

            RectangleF groupTextLayout = new RectangleF (x, y, width, this.MaxGroupRowHeight);
            StringFormat groupStringFormat = new StringFormat (StringFormatFlags.LineLimit);
            groupStringFormat.Trimming = StringTrimming.EllipsisCharacter;
            groupStringFormat.Alignment = StringAlignment.Near;

            float groupHeight = default (float);

            int charsFitted, linesFilled;
            groupHeight = Math.Max (g.MeasureString (groupName, this.GroupFont, groupTextLayout.Size, groupStringFormat, out charsFitted, out linesFilled).Height, groupHeight);

            if (!calculateSizeOnly)
                g.DrawString (groupName, this.GroupFont, this.GroupBrush, groupTextLayout, groupStringFormat);

            groupTextLayout.X = x;
            groupTextLayout.Y = y;
            groupTextLayout.Width = width;
            groupTextLayout.Height = Math.Max (Math.Min (groupHeight, this.MaxGroupRowHeight), this.MinGroupRowHeight);

            return groupTextLayout.Height;
        }

        protected virtual float PrintDetailRow (float x, float y, float width, PrintPageEventArgs e, DataRowView row, bool calculateSizeOnly)
        {
            Graphics g = e.Graphics;

            RectangleF detailTextLayout = new RectangleF (x, y, width, this.MaxDetailRowHeight);
            StringFormat detailStringFormat = new StringFormat (StringFormatFlags.LineLimit);
            detailStringFormat.Trimming = StringTrimming.EllipsisCharacter;

            float detailHeight = default (float);

            for (int columnIndex = 0 ; columnIndex < m_dataView.Table.Columns.Count ; columnIndex++)
            {
                JcwReportDataColumn column = m_dataView.Table.Columns[columnIndex] as JcwReportDataColumn;
                JcwReportLayoutColumn layoutColumn = (m_dataView as JcwReportDataView).GetLayoutColumn (column.ColumnName);

                // if the current column is a group column, skip it because we've printed its text prior to entering this method
                if (layoutColumn.IsGroupColumn)
                    continue;

                // if a font is not specified for the column, use the DetailFont property of the report class
                Font detailRowFont = (layoutColumn.DetailFont == null) ?
                    this.DetailFont :
                    layoutColumn.DetailFont;

                string detailText = JcwReportLayoutColumn.GetString (JcwReportDataView.GetField (row, column.ColumnName));
                detailStringFormat.Alignment = layoutColumn.Alignment;
                detailTextLayout.Width = layoutColumn.Width;

                // none of it will fit onto the page
                if ((detailTextLayout.X - x) >= width)
                    break;
                // some of it won't fit onto the page
                else if ((detailTextLayout.X + detailTextLayout.Width - x) > width)
                    detailTextLayout.Width = width - detailTextLayout.X - x;

                int charsFitted, linesFilled;
                detailHeight = Math.Max (g.MeasureString (detailText, detailRowFont, detailTextLayout.Size, detailStringFormat, out charsFitted, out linesFilled).Height, detailHeight);

                // no space between columns? this code lines the columns up flush, but if you wished to add a space between them you would need to add it to X here, 
                // in the check above to determine if the column will fit, and in the corresponding parts of the next For loop. 
                detailTextLayout.X += detailTextLayout.Width;
            }

            detailTextLayout.X = x;
            detailTextLayout.Height = Math.Max (Math.Min (detailHeight, this.MaxDetailRowHeight), this.MinDetailRowHeight);

            if (!calculateSizeOnly)
            {
                for (int columnIndex = 0 ; columnIndex < m_dataView.Table.Columns.Count ; columnIndex++)
                {
                    JcwReportDataColumn column = m_dataView.Table.Columns[columnIndex] as JcwReportDataColumn;
                    JcwReportLayoutColumn layoutColumn = (m_dataView as JcwReportDataView).GetLayoutColumn (column.ColumnName);

                    if (layoutColumn.IsGroupColumn)
                        continue;

                    // if a font is not specified, use the DetailFont property of the report class
                    Font detailRowFont = (layoutColumn.DetailFont == null) ?
                        this.DetailFont :
                        layoutColumn.DetailFont;

                    string detailText = JcwReportLayoutColumn.GetString (JcwReportDataView.GetField (row, column.ColumnName));
                    detailStringFormat.Alignment = layoutColumn.Alignment;
                    detailTextLayout.Width = layoutColumn.Width;

                    // none of it will fit onto the page
                    if ((detailTextLayout.X - x) >= width)
                        break;
                    // some of it won't fit onto the page
                    else if ((detailTextLayout.X + detailTextLayout.Width - x) > width)
                        detailTextLayout.Width = width - detailTextLayout.X - x;

                    g.DrawString (detailText, detailRowFont, this.DetailBrush, detailTextLayout, detailStringFormat);
                    detailTextLayout.X += detailTextLayout.Width;
                }

                detailTextLayout.X = x;
                detailTextLayout.Y = y;
                detailTextLayout.Height = detailHeight;
                detailTextLayout.Width = width;
            }

            return detailTextLayout.Height;
        }

        #endregion
    }
}
