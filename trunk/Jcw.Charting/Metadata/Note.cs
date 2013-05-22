using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Xml.Serialization;

using Jcw.Charting.Interfaces;

namespace Jcw.Charting.Metadata
{
    [Serializable]
    public class Note : Mark
    {
        #region Properties

        public string NoteText
        {
            get { return Text; }
            set { Text = value; }
        }

        #endregion

        #region Constructors

        public Note ()
            : this ( 0, null, 0, null )
        {
        }

        public Note ( double xCoord, string xAxisTitle, double yCoord, string yAxisTitle )
            : this ( xCoord, xAxisTitle, yCoord, yAxisTitle, null, null )
        {
        }

        public Note ( double xCoord, string xAxisTitle, double yCoord, string yAxisTitle, string text, string value )
            : this ( xCoord, xAxisTitle, yCoord, yAxisTitle, text, value, Color.Transparent )
        {
        }

        public Note ( double xCoord, string xAxisTitle, double yCoord, string yAxisTitle, string text, string value, Color markColor )
            : base ( xCoord, xAxisTitle, yCoord, yAxisTitle, text, value )
        {
            MarkColor = markColor;
        }

        #endregion

        #region Overrides

        public override object Clone ()
        {
            Note n = new Note ();

            n.Value = this.Value;
            n.NoteText = this.NoteText;
            n.MarkColor = this.MarkColor;
            n.XCoordinate = this.XCoordinate.Clone () as AxisCoordinate;
            n.YCoordinate = this.YCoordinate.Clone () as AxisCoordinate;

            return n;
        }

        public override bool UserEditable { get; set; }

        #endregion
    }
}