using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Xml.Serialization;

using Jcw.Charting.Interfaces;

namespace Jcw.Charting.Metadata
{
    [Serializable]
    public class Line : ICloneable, IMark
    {
        #region Constants

        private static readonly Color DefaultColor = Color.DarkSlateBlue;
        private const int DefaultWidth = 1;

        #endregion

        #region Properties

        public AxisCoordinate Coordinate { get; set; }

        /// <summary>
        /// Property provided for data grid view binding
        /// </summary>
        [XmlIgnore]
        public double CoordinateValue
        {
            get { return Coordinate.Coordinate; }
        }

        [XmlIgnore]
        public Color MarkColor { get; set; }

        public string MarkColorName
        {
            get { return MarkColor.Name; }
            set { MarkColor = Color.FromName (value); }
        }

        public int LineWidth { get; set; }
        public string Text { get; set; }

        #endregion

        #region Constructors

        public Line ()
            : this (null, 0, null, null, DefaultColor, DefaultWidth)
        {
        }

        public Line (string name, double coord, string axisTitle)
            : this (name, coord, axisTitle, null, DefaultColor, DefaultWidth)
        {
        }

        public Line (string name, double coord, string axisTitle, string text)
            : this (name, coord, axisTitle, text, DefaultColor, DefaultWidth)
        {
        }

        public Line (string name, double coord, string axisTitle, string text, Color markColor)
            : this (name, coord, axisTitle, text, markColor, DefaultWidth)
        {
        }

        public Line (string name, double coord, string axisTitle, string text, Color markColor, int width)
        {
            Coordinate = new AxisCoordinate (name, coord, axisTitle);
            LineWidth = width;
            MarkColor = markColor;
            Text = text;
        }

        #endregion

        #region Overrides

        public override bool Equals (object obj)
        {
            Line l = obj as Line;
            if (l != null)
            {
                if (l.Text != Text)
                    return false;

                if (l.MarkColorName != MarkColorName)
                    return false;

                // Coordinate is a reference type so cannot use == operator because it tests for the exact same 
                // object where I want to check based on the object contents.

                if (l.Coordinate == null)
                {
                    if (Coordinate != null)
                        return false;
                }
                else
                {
                    if (!l.Coordinate.Equals (Coordinate))
                        return false;
                }

                return true;
            }

            return base.Equals (obj);
        }

        public override int GetHashCode ()
        {
            int hashCode = 0;

            if (Coordinate != null)
            {
                hashCode ^= Coordinate.GetHashCode ();
            }
            if (!string.IsNullOrEmpty (MarkColorName))
            {
                hashCode ^= MarkColorName.GetHashCode ();
            }
            if (!string.IsNullOrEmpty (Text))
            {
                hashCode ^= Text.GetHashCode ();
            }

            return hashCode;
        }

        public override string ToString ()
        {
            StringBuilder sb = new StringBuilder ();

            if (Coordinate != null)
            {
                sb.Append (Coordinate.ToString ());
                sb.Append (", ");
            }
            sb.Append (string.Format ("Text={0}, LineColor={1}", Text, MarkColorName));

            return sb.ToString ();
        }

        #endregion

        #region ICloneable Implementation

        public virtual object Clone ()
        {
            Line l = new Line ();

            l.Coordinate = Coordinate.Clone () as AxisCoordinate;
            l.LineWidth = LineWidth;
            l.MarkColor = MarkColor;
            l.Text = Text;

            return l;
        }

        #endregion

        #region IMark Implementation

        public bool UserEditable { get; set; }

        #endregion
    }
}
