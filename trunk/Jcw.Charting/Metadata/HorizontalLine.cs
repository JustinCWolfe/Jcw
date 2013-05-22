using System;
using System.Drawing;
using System.Xml.Serialization;

using Jcw.Charting.Interfaces;

namespace Jcw.Charting.Metadata
{
    [Serializable]
    public class HorizontalLine : Line
    {
        #region Constants

        private const string DefaultName = "Y";

        #endregion

        #region Constructors

        public HorizontalLine ()
            : base ()
        {
            Coordinate.Name = DefaultName;
        }

        public HorizontalLine (double coord, string axisTitle)
            : base (DefaultName, coord, axisTitle)
        {
        }

        public HorizontalLine (double coord, string axisTitle, string text, Color markColor, int width)
            : base (DefaultName, coord, axisTitle, text, markColor, width)
        {
        }

        public HorizontalLine (Line line)
            : this (line.CoordinateValue, line.Coordinate.AxisTitle, line.Text, line.MarkColor, line.LineWidth)
        {
        }

        #endregion

        #region Overrides

        public override object Clone ()
        {
            return new HorizontalLine (base.Clone () as Line);
        }

        #endregion
    }
}
