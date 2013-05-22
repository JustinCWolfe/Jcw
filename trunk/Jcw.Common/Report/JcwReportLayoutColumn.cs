using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Jcw.Common.Data
{
    public class JcwReportLayoutColumn
    {
        #region Fields

        private float m_width = 1;
        private Font m_detailFont = null;
        private bool m_isGroupColumn = false;
        private StringAlignment m_alignment = StringAlignment.Near;

        #endregion

        #region Properties

        /// <summary>
        /// Is the column a grouping column or a regular data column
        /// </summary>
        public bool IsGroupColumn
        {
            get { return m_isGroupColumn; }
        }

        /// <summary>
        /// Width of the layout column in inches
        /// </summary>
        public float Width
        {
            get { return m_width; }
        }

        /// <summary>
        /// Font of text content in layout column
        /// </summary>
        public Font DetailFont
        {
            get { return m_detailFont; }
        }

        /// <summary>
        /// Alignment of text content in layout column
        /// </summary>
        public StringAlignment Alignment
        {
            get { return m_alignment; }
        }

        #endregion

        #region Constructor

        public JcwReportLayoutColumn ()
        {
        }

        public JcwReportLayoutColumn ( float width )
            : this ()
        {
            m_width = width;
        }

        public JcwReportLayoutColumn ( float width, bool isGroupColumn )
            : this ( width )
        {
            m_isGroupColumn = isGroupColumn;
        }

        public JcwReportLayoutColumn ( float width, StringAlignment alignment )
            : this ( width )
        {
            m_alignment = alignment;
        }

        public JcwReportLayoutColumn ( float width, Font detailFont )
            : this ( width )
        {
            m_detailFont = detailFont;
        }

        public JcwReportLayoutColumn ( float width, Font detailFont, StringAlignment alignment )
            : this ( width, alignment )
        {
            m_detailFont = detailFont;
        }

        public JcwReportLayoutColumn ( float width, Font detailFont, StringAlignment alignment, bool isGroupColumn )
            : this ( width, detailFont, alignment )
        {
            m_isGroupColumn = isGroupColumn;
        }

        #endregion

        #region Static Methods

        public static string GetString ( object value )
        {
            return ( value != null ) ?
                Convert.ToString ( value ) :
                null;
        }

        #endregion
    }
}
