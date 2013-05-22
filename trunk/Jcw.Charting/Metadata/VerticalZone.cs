using System;
using System.Drawing;
using System.Xml.Serialization;

using Jcw.Charting.Interfaces;

namespace Jcw.Charting.Metadata
{
    [Serializable]
    public class VerticalZone : Zone
    {
        #region Constants

        private const string ZoneStart = "xStart";
        private const string ZoneEnd = "xEnd";

        #endregion

        #region Constructors

        public VerticalZone ()
            : this (0, 0, null, null, DefaultColor)
        {
        }

        public VerticalZone (double startCoord, double endCoord, string axisTitle)
            : this (startCoord, endCoord, axisTitle, null, DefaultColor)
        {
        }

        public VerticalZone (double startCoord, double endCoord, string xAxisTitle, Color markColor)
            : this (startCoord, endCoord, xAxisTitle, null, markColor)
        {
        }

        public VerticalZone (Zone zone)
            : this (zone.StartCoordinateValue, zone.EndCoordinateValue, zone.StartCoordinate.AxisTitle, zone.OppositeAxisTitle, zone.ZoneColor)
        {
        }

        /// <summary>
        /// This is the primary constructor.
        /// </summary>
        /// <param name="startCoord"></param>
        /// <param name="endCoord"></param>
        /// <param name="axisTitle"></param>
        /// <param name="oppositeAxisTitle"></param>
        /// <param name="markColor"></param>
        public VerticalZone (double startCoord, double endCoord, string axisTitle, string oppositeAxisTitle, Color markColor)
            : base (ZoneStart, startCoord, ZoneEnd, endCoord, axisTitle, oppositeAxisTitle, markColor)
        {
        }

        #endregion

        #region Overrides

        public override object Clone ()
        {
            return new VerticalZone (base.Clone () as Zone);
        }

        #endregion
    }
}
