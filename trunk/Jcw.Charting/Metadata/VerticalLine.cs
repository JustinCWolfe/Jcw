using System;
using System.Drawing;
using System.Xml.Serialization;

using Jcw.Charting.Interfaces;

namespace Jcw.Charting.Metadata
{
    [Serializable]
    public class VerticalLine : Line
    {
        #region Constants

        private const string DefaultName = "X";

        #endregion

        #region Constructors

        public VerticalLine ()
            : base ()
        {
            Coordinate.Name = DefaultName;
        }

        public VerticalLine (double coord, string axisTitle)
            : base (DefaultName, coord, axisTitle)
        {
        }

        public VerticalLine (double coord, string axisTitle, string text, Color markColor, int width)
            : base (DefaultName, coord, axisTitle, text, markColor, width)
        {
        }

        public VerticalLine (Line line)
            : this (line.CoordinateValue, line.Coordinate.AxisTitle, line.Text, line.MarkColor, line.LineWidth)
        {
        }

        #endregion

        #region Overrides

        public override object Clone ()
        {
            return new VerticalLine (base.Clone () as Line);
        }

        #endregion
    }
}