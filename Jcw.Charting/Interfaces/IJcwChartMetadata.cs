using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

using Jcw.Charting.Metadata;
using Jcw.Common;

namespace Jcw.Charting.Interfaces
{
    /// <summary>
    /// Metadata interface
    /// </summary>
    /// <typeparam name="R">VerticalLine</typeparam>
    /// <typeparam name="S">HorizontalLine</typeparam>
    /// <typeparam name="T">VerticalZone</typeparam>
    /// <typeparam name="U">HorizontalZone</typeparam>
    /// <typeparam name="V">Note</typeparam>
    public interface IJcwChartMetadata<R, S, T, U, V>
    {
        #region Properties

        /// <summary>
        /// Indicates whether or not the chart was zoomed in at the time its metadata was last saved
        /// </summary>
        bool IsChartZoomedIn { get; set; }

        /// <summary>
        /// Indicates whether or not the chart zoomed in button was selected when its metadata was last saved
        /// </summary>
        bool IsChartZoomInSelected { get; set; }

        /// <summary>
        /// Indicates whether or not the chart zoomed out button was selected when its metadata was last saved
        /// </summary>
        bool IsChartZoomOutSelected { get; set; }

        /// <summary>
        /// The horizontal minimum value for a zoomed in chart
        /// </summary>
        double ChartHorizontalMinimum { get; set; }

        /// <summary>
        /// The horizontal maximum for a zoomed in chart
        /// </summary>
        double ChartHorizontalMaximum { get; set; }

        /// <summary>
        /// Indicates whether or not vertical lines are visible on the chart
        /// </summary>
        bool AreVerticalLinesVisible { get; set; }

        /// <summary>
        /// Get all vertical line markers in the chart metadata
        /// </summary>
        R[] ChartVerticalLines { get; set; }

        /// <summary>
        /// Get binding list of all vertical line markers in the chart metadata
        /// </summary>
        BindingList<R> ChartVerticalLineBindingList { get; }

        /// <summary>
        /// Indicates whether or not horizontal lines are visible on the chart
        /// </summary>
        bool AreHorizontalLinesVisible { get; set; }

        /// <summary>
        /// Get all horizontal line markers in the chart metadata
        /// </summary>
        S[] ChartHorizontalLines { get; set; }

        /// <summary>
        /// Get binding list of all horizontal line markers in the chart metadata
        /// </summary>
        BindingList<S> ChartHorizontalLineBindingList { get; }

        /// <summary>
        /// Indicates whether or not vertical zones are visible on the chart
        /// </summary>
        bool AreVerticalZonesVisible { get; set; }

        /// <summary>
        /// Get all vertical zone markers in the chart metadata
        /// </summary>
        T[] ChartVerticalZones { get; set; }

        /// <summary>
        /// Get binding list of all vertical zone markers in the chart metadata
        /// </summary>
        BindingList<T> ChartVerticalZoneBindingList { get; }

        /// <summary>
        /// Indicates whether or not horizontal zones are visible on the chart
        /// </summary>
        bool AreHorizontalZonesVisible { get; set; }

        /// <summary>
        /// Get all horizontal zone markers in the chart metadata
        /// </summary>
        U[] ChartHorizontalZones { get; set; }

        /// <summary>
        /// Get binding list of all horizontal zone markers in the chart metadata
        /// </summary>
        BindingList<U> ChartHorizontalZoneBindingList { get; }

        /// <summary>
        /// Indicates whether or not notes are visible on the chart
        /// </summary>
        bool AreNotesVisible { get; set; }

        /// <summary>
        /// Get all notes in the chart metadata
        /// </summary>
        V[] ChartNotes { get; set; }

        /// <summary>
        /// Get binding list of all chart notes in the chart metadata
        /// </summary>
        BindingList<V> ChartNoteBindingList { get; }

        int MetadataVersion { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Add a vertical line object to the chart metadata
        /// </summary>
        /// <param name="mark">vertical mark object</param>
        void AddChartVerticalLine (R mark);

        /// <summary>
        /// Remove the passed-in vertical line object from the chart metadata
        /// </summary>
        /// <param name="mark">vertical mark object</param>
        void RemoveVerticalLine (R mark);

        /// <summary>
        /// Remove vertical line object from the chart metadata based on the axis coordinate and title
        /// </summary>
        /// <param name="mark">vertical mark object</param>
        void RemoveVerticalLine (double coordinate, string axisTitle);

        /// <summary>
        /// Remove all chart vertical lines
        /// </summary>
        void ClearVerticalLines ();

        /// <summary>
        /// Add a horizontal line object to the chart metadata
        /// </summary>
        /// <param name="mark">horizontal mark object</param>
        void AddChartHorizontalLine (S mark);

        /// <summary>
        /// Remove the passed-in horizontal line object from the chart metadata
        /// </summary>
        /// <param name="mark">horizontal mark object</param>
        void RemoveHorizontalLine (S mark);

        /// <summary>
        /// Remove horizontal line object from the chart metadata based on the axis coordinate and title
        /// </summary>
        /// <param name="mark">horizontal mark object</param>
        void RemoveHorizontalLine (double coordinate, string axisTitle);

        /// <summary>
        /// Remove all chart horizontal lines
        /// </summary>
        void ClearHorizontalLines ();

        /// <summary>
        /// Add a vertical zone object to the chart metadata
        /// </summary>
        /// <param name="mark">vertical zone object</param>
        void AddChartVerticalZone (T zone);

        /// <summary>
        /// Remove the passed-in vertical zone object from the chart metadata
        /// </summary>
        /// <param name="mark">vertical zone object</param>
        void RemoveVerticalZone (T zone);

        /// <summary>
        /// Remove all chart vertical zones
        /// </summary>
        void ClearVerticalZones ();

        /// <summary>
        /// Add a horizontal zone object to the chart metadata
        /// </summary>
        /// <param name="mark">horizontal zone object</param>
        void AddChartHorizontalZone (U zone);

        /// <summary>
        /// Remove the passed-in horizontal zone object from the chart metadata
        /// </summary>
        /// <param name="mark">horizontal zone object</param>
        void RemoveHorizontalZone (U zone);

        /// <summary>
        /// Remove all chart horizontal zones
        /// </summary>
        void ClearHorizontalZones ();

        /// <summary>
        /// Add a note at a specified x,y coordinate to the chart metadata
        /// </summary>
        /// <param name="note">note object</param>
        void AddChartNote (V note);

        /// <summary>
        /// Remote passed-in specified note from the chart metadata
        /// </summary>
        /// <param name="note">note object</param>
        void RemoveChartNote (V note);

        /// <summary>
        /// Remove note object from the chart metadata based on the axis coordinates and titles
        /// </summary>
        /// <param name="mark">note object</param>
        void RemoveChartNote(double xCoordinate, string xAxisTitle, double yCoordinate, string yAxisTitle, Color markColor, string markText);

        /// <summary>
        /// Remove all chart notes 
        /// </summary>
        void ClearChartNotes ();

        #endregion
    }
}
