using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Xml.Serialization;

using Jcw.Charting.Interfaces;

namespace Jcw.Charting.Metadata
{
    [Serializable]
    public class Zone : ICloneable, IMark
    {
        #region Constants

        protected static readonly Color DefaultColor = Color.LightBlue;

        #endregion

        #region Properties

        public AxisCoordinate StartCoordinate { get; set; }
        public AxisCoordinate EndCoordinate { get; set; }

        /// <summary>
        /// Property provided for data grid view binding
        /// </summary>
        [XmlIgnore]
        public double StartCoordinateValue
        {
            get { return StartCoordinate.Coordinate; }
        }

        /// <summary>
        /// Property provided for data grid view binding
        /// </summary>
        [XmlIgnore]
        public double EndCoordinateValue
        {
            get { return EndCoordinate.Coordinate; }
        }

        /// <summary>
        /// Property provided for data grid view binding
        /// </summary>
        [XmlIgnore]
        public double ZoneTotalValue
        {
            get { return EndCoordinate.Coordinate - StartCoordinate.Coordinate; }
        }

        /// <summary>
        ///  The axis opposite the axis that defines the range of the zone.
        /// </summary>
        public string OppositeAxisTitle { get; set; }

        [XmlIgnore]
        public Color ZoneColor { get; set; }

        public string ZoneColorName
        {
            get { return ZoneColor.Name; }
            set { ZoneColor = Color.FromName (value); }
        }

        #endregion

        #region Constructors

        public Zone ()
            : this (null, 0, null, 0, null, null, DefaultColor)
        {
        }

        public Zone (string nameStart, double startCoord, string nameEnd, double endCoord, string axisTitle)
            : this (nameStart, startCoord, nameEnd, endCoord, axisTitle, null, DefaultColor)
        {
        }

        public Zone (string nameStart, double startCoord, string nameEnd, double endCoord, string axisTitle, Color zoneColor)
            : this (nameStart, startCoord, nameEnd, endCoord, axisTitle, null, zoneColor)
        {
        }

        /// <summary>
        /// This is the primary constructor
        /// </summary>
        /// <param name="nameStart"></param>
        /// <param name="startCoord"></param>
        /// <param name="nameEnd"></param>
        /// <param name="endCoord"></param>
        /// <param name="axisTitle"></param>
        /// <param name="oppositeAxisTitle"></param>
        /// <param name="zoneColor"></param>
        public Zone (string nameStart, double startCoord, string nameEnd, double endCoord, string axisTitle, string oppositeAxisTitle, Color zoneColor)
        {
            StartCoordinate = new AxisCoordinate (nameStart, startCoord, axisTitle);
            EndCoordinate = new AxisCoordinate (nameEnd, endCoord, axisTitle);

            OppositeAxisTitle = oppositeAxisTitle;
            ZoneColor = zoneColor;
        }

        #endregion

        #region Overrides

        public override bool Equals (object obj)
        {
            Zone z = obj as Zone;
            if (z != null)
            {
                if (z.OppositeAxisTitle != OppositeAxisTitle)
                    return false;

                if (z.ZoneColorName != ZoneColorName)
                    return false;

                // StartCoordinate and EndCoordinate are reference types so cannot use == operator because 
                // it tests for the exact same object where I want to check based on the object contents.

                if (z.StartCoordinate == null)
                {
                    if (StartCoordinate != null)
                        return false;
                }
                else
                {
                    if (!z.StartCoordinate.Equals (StartCoordinate))
                        return false;
                }

                if (z.EndCoordinate == null)
                {
                    if (EndCoordinate != null)
                        return false;
                }
                else
                {
                    if (!z.EndCoordinate.Equals (EndCoordinate))
                        return false;
                }

                return true;
            }

            return base.Equals (obj);
        }

        public override int GetHashCode ()
        {
            int hashCode = 0;

            if (StartCoordinate != null)
            {
                hashCode ^= StartCoordinate.GetHashCode ();
            }
            if (EndCoordinate != null)
            {
                hashCode ^= EndCoordinate.GetHashCode ();
            }
            if (!string.IsNullOrEmpty (ZoneColorName))
            {
                hashCode ^= ZoneColorName.GetHashCode ();
            }
            if (!string.IsNullOrEmpty (OppositeAxisTitle))
            {
                hashCode ^= OppositeAxisTitle.GetHashCode ();
            }

            return hashCode;
        }

        public override string ToString ()
        {
            StringBuilder sb = new StringBuilder ();

            if (StartCoordinate != null)
            {
                sb.Append (StartCoordinate.ToString ());
                sb.Append (", ");
            }
            if (EndCoordinate != null)
            {
                sb.Append (EndCoordinate.ToString ());
                sb.Append (", ");
            }
            sb.Append (string.Format ("ZoneColor={0}, OppositeAxisTitle={1}", ZoneColorName, OppositeAxisTitle));

            return sb.ToString ();
        }

        #endregion

        #region ICloneable Implementation

        public virtual object Clone ()
        {
            Zone z = new Zone ();

            z.ZoneColor = ZoneColor;
            z.EndCoordinate = EndCoordinate.Clone () as AxisCoordinate;
            z.StartCoordinate = StartCoordinate.Clone () as AxisCoordinate;
            z.OppositeAxisTitle = OppositeAxisTitle;

            return z;
        }

        #endregion

        #region IMark Implementation

        public bool UserEditable { get; set; }

        #endregion
    }
}
