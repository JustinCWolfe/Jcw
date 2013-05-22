using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Xml.Serialization;

using Jcw.Charting.Interfaces;

namespace Jcw.Charting.Metadata
{
    [Serializable]
    public class CircleMarker : Mark
    {
        #region Constructors

        public CircleMarker ()
            : this ( 0, null, 0, null )
        {
        }

        public CircleMarker ( double xCoord, string xAxisTitle, double yCoord, string yAxisTitle )
            : this ( xCoord, xAxisTitle, yCoord, yAxisTitle, null, null )
        {
        }

        public CircleMarker ( double xCoord, string xAxisTitle, double yCoord, string yAxisTitle, string text, string value )
            : base ( xCoord, xAxisTitle, yCoord, yAxisTitle, text, value )
        {
        }

        #endregion

        #region Overrides

        public override object Clone ()
        {
            CircleMarker m = new CircleMarker ();

            m.Text = this.Text;
            m.Value = this.Value;
            m.MarkColor = this.MarkColor;
            m.XCoordinate = this.XCoordinate.Clone () as AxisCoordinate;
            m.YCoordinate = this.YCoordinate.Clone () as AxisCoordinate;

            return m;
        }

        public override bool UserEditable {get;set;}

        #endregion
    }
}
