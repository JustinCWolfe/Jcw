using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Xml.Serialization;

using Jcw.Charting.Interfaces;

namespace Jcw.Charting.Metadata
{
    [Serializable]
    public abstract class Mark : ICloneable, IMark
    {
        #region Properties

        public string Text { get; set; }
        public string Value { get; set; }
        public AxisCoordinate XCoordinate { get; set; }

        /// <summary>
        /// Property provided for data grid view binding
        /// </summary>
        [XmlIgnore]
        public double XCoordinateValue
        {
            get { return XCoordinate.Coordinate; }
        }

        public AxisCoordinate YCoordinate { get; set; }

        /// <summary>
        /// Property provided for data grid view binding
        /// </summary>
        [XmlIgnore]
        public double YCoordinateValue
        {
            get { return YCoordinate.Coordinate; }
        }

        [XmlIgnore]
        public Color MarkColor { get; set; }

        public string MarkColorName
        {
            get { return MarkColor.Name; }
            set { MarkColor = Color.FromName (value); }
        }

        #endregion

        #region Constructors

        public Mark ()
            : this (0, null, 0, null, null, null)
        {
        }

        public Mark (double xCoord, string xAxisTitle, double yCoord, string yAxisTitle, string text, string value)
        {
            Text = text;
            Value = value;
            XCoordinate = new AxisCoordinate ("X", xCoord, xAxisTitle);
            YCoordinate = new AxisCoordinate ("Y", yCoord, yAxisTitle);
        }

        #endregion

        #region Overrides

        public override bool Equals (object obj)
        {
            Mark m = obj as Mark;
            if (m != null)
            {
                if (m.Text != Text)
                    return false;

                if (m.MarkColorName != MarkColorName)
                    return false;

                // XCoordinate and YCoordinate are reference types so cannot use == operator because it 
                // test for the exact same object where I want to check based on the object contents.

                if (m.XCoordinate == null)
                {
                    if (XCoordinate != null)
                        return false;
                }
                else
                {
                    if (!m.XCoordinate.Equals (XCoordinate))
                        return false;
                }

                if (m.YCoordinate == null)
                {
                    if (YCoordinate != null)
                        return false;
                }
                else
                {
                    if (!m.YCoordinate.Equals (YCoordinate))
                        return false;
                }

                return true;
            }

            return base.Equals (obj);
        }

        public override int GetHashCode ()
        {
            int hashCode = 0;

            if (XCoordinate != null)
            {
                hashCode ^= XCoordinate.GetHashCode ();
            }
            if (YCoordinate != null)
            {
                hashCode ^= YCoordinate.GetHashCode ();
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

            if (XCoordinate != null)
            {
                sb.Append (XCoordinate.ToString ());
                sb.Append (", ");
            }
            if (YCoordinate != null)
            {
                sb.Append (YCoordinate.ToString ());
                sb.Append (", ");
            }
            sb.Append (string.Format ("Text={0}, MarkColor={1}", Text, MarkColorName));

            return sb.ToString ();
        }

        #endregion

        #region Abstract ICloneable Implementation

        public abstract object Clone ();

        #endregion

        #region Abstract IMark Implementation

        public abstract bool UserEditable { get; set; }

        #endregion
    }
}
