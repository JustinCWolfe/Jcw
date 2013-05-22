using System;
using System.Drawing;
using System.Xml.Serialization;

using Jcw.Charting.Interfaces;

namespace Jcw.Charting.Metadata
{
    [Serializable]
    public class HorizontalZone : Zone
    {
        #region Constants

        private const string ZoneStart = "yStart";
        private const string ZoneEnd = "yEnd";

        #endregion

        #region Constructors

        public HorizontalZone ()
            : this (0, 0, null, null, DefaultColor)
        {
        }

        public HorizontalZone (double startCoord, double endCoord, string yAxisTitle)
            : this (startCoord, endCoord, yAxisTitle, null, DefaultColor)
        {
        }

        public HorizontalZone (double startCoord, double endCoord, string yAxisTitle, Color markColor)
            : this (startCoord, endCoord, yAxisTitle, null, markColor)
        {
        }

        public HorizontalZone (Zone zone)
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
        public HorizontalZone (double startCoord, double endCoord, string axisTitle, string oppositeAxisTitle, Color markColor)
            : base (ZoneStart, startCoord, ZoneEnd, endCoord, axisTitle, oppositeAxisTitle, markColor)
        {
        }

        #endregion

        #region Overrides

        public override object Clone ()
        {
            return new HorizontalZone (base.Clone () as Zone);
        }

        #endregion
    }
}
