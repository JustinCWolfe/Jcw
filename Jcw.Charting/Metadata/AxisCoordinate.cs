using System;
using System.Drawing;
using System.Text;
using System.Xml.Serialization;

using Jcw.Charting.Interfaces;

namespace Jcw.Charting.Metadata
{
    [Serializable]
    public class AxisCoordinate : ICloneable, IMark
    {
        #region Properties

        public string Name { get; set; }
        public double Coordinate { get; set; }
        public string AxisTitle { get; set; }

        #endregion

        #region Constructors

        public AxisCoordinate ()
        {
        }

        public AxisCoordinate (string name, double coordinate, string axisTitle)
        {
            Name = name;
            Coordinate = coordinate;
            AxisTitle = axisTitle;
        }

        #endregion

        #region Overrides

        public override bool Equals (object obj)
        {
            AxisCoordinate ac = obj as AxisCoordinate;
            if (ac != null)
            {
                // For value types and string objects, == is ok to use here.
                return (Coordinate == ac.Coordinate && AxisTitle == ac.AxisTitle && Name == ac.Name);
            }

            return base.Equals (obj);
        }

        public override int GetHashCode ()
        {
            int hashCode = 0;

            hashCode ^= Coordinate.GetHashCode ();
            if (AxisTitle != null)
            {
                hashCode ^= AxisTitle.GetHashCode ();
            }
            if (Name != null)
            {
                hashCode ^= Name.GetHashCode ();
            }

            return hashCode;
        }

        public override string ToString ()
        {
            return string.Format ("AxisTitle={0}, Coord={1}, Name={2}", AxisTitle, Coordinate, Name);
        }

        #endregion

        #region ICloneable Implementation

        public virtual object Clone ()
        {
            AxisCoordinate ac = new AxisCoordinate ();

            ac.AxisTitle = AxisTitle;
            ac.Coordinate = Coordinate;
            ac.Name = Name;

            return ac;
        }

        #endregion

        #region IMark Implementation

        public bool UserEditable { get; set; }

        #endregion
    }
}