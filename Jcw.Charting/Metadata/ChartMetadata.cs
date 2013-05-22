using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

using Jcw.Charting.Interfaces;

namespace Jcw.Charting.Metadata
{
    [Serializable]
    public class ChartMetadata : IJcwChartMetadata<VerticalLine, HorizontalLine, VerticalZone, HorizontalZone, Note>, ICloneable
    {
        #region Constructors

        public ChartMetadata ()
        {
            MetadataVersion = 2;
            AreVerticalLinesVisible = true;
            AreHorizontalLinesVisible = true;
            AreVerticalZonesVisible = true;
            AreHorizontalZonesVisible = true;
            AreNotesVisible = true;
            AreCircleMarkersVisible = true;
        }

        #endregion

        #region IJcwChartMetadata Implementation

        #region Properties

        public int MetadataVersion { get; set; }
        public bool IsChartZoomedIn { get; set; }
        public bool IsChartZoomInSelected { get; set; }
        public bool IsChartZoomOutSelected { get; set; }
        public double ChartHorizontalMinimum { get; set; }
        public double ChartHorizontalMaximum { get; set; }
        public bool AreVerticalLinesVisible { get; set; }
        public bool AreHorizontalLinesVisible { get; set; }
        public bool AreVerticalZonesVisible { get; set; }
        public bool AreHorizontalZonesVisible { get; set; }
        public bool AreNotesVisible { get; set; }
        public bool AreCircleMarkersVisible { get; set; }

        private HashSet<VerticalLine> m_verticalLines = new HashSet<VerticalLine> ();
        public VerticalLine[] ChartVerticalLines
        {
            get
            {
                VerticalLine[] vl = new VerticalLine[m_verticalLines.Count];
                m_verticalLines.CopyTo (vl, 0);
                return vl;
            }
            set
            {
                m_verticalLines.Clear ();

                HashSet<VerticalLine> uniqueVerticalLines = new HashSet<VerticalLine> (value);
                foreach (VerticalLine line in uniqueVerticalLines)
                {
                    m_verticalLines.Add (line);
                }
            }
        }

        [XmlIgnore]
        public BindingList<VerticalLine> ChartVerticalLineBindingList
        {
            get
            {
                BindingList<VerticalLine> bl = new BindingList<VerticalLine> ();
                foreach (VerticalLine v in m_verticalLines)
                {
                    bl.Add (v);
                }
                return bl;
            }
        }

        private HashSet<HorizontalLine> m_horizontalLines = new HashSet<HorizontalLine> ();
        public HorizontalLine[] ChartHorizontalLines
        {
            get
            {
                HorizontalLine[] hl = new HorizontalLine[m_horizontalLines.Count];
                m_horizontalLines.CopyTo (hl, 0);
                return hl;
            }
            set
            {
                m_horizontalLines.Clear ();

                HashSet<HorizontalLine> uniqueHorizontalLines = new HashSet<HorizontalLine> (value);
                foreach (HorizontalLine line in uniqueHorizontalLines)
                {
                    m_horizontalLines.Add (line);
                }
            }
        }

        [XmlIgnore]
        public BindingList<HorizontalLine> ChartHorizontalLineBindingList
        {
            get
            {
                BindingList<HorizontalLine> bl = new BindingList<HorizontalLine> ();
                foreach (HorizontalLine h in m_horizontalLines)
                {
                    bl.Add (h);
                }
                return bl;
            }
        }

        private HashSet<VerticalZone> m_verticalZones = new HashSet<VerticalZone> ();
        public VerticalZone[] ChartVerticalZones
        {
            get
            {
                VerticalZone[] vz = new VerticalZone[m_verticalZones.Count];
                m_verticalZones.CopyTo (vz, 0);
                return vz;
            }
            set
            {
                m_verticalZones.Clear ();

                HashSet<VerticalZone> uniqueVerticalZones = new HashSet<VerticalZone> (value);
                foreach (VerticalZone zone in uniqueVerticalZones)
                {
                    m_verticalZones.Add (zone);
                }
            }
        }

        [XmlIgnore]
        public BindingList<VerticalZone> ChartVerticalZoneBindingList
        {
            get
            {
                BindingList<VerticalZone> bl = new BindingList<VerticalZone> ();
                foreach (VerticalZone v in m_verticalZones)
                {
                    bl.Add (v);
                }
                return bl;
            }
        }

        private HashSet<HorizontalZone> m_horizontalZones = new HashSet<HorizontalZone> ();
        public HorizontalZone[] ChartHorizontalZones
        {
            get
            {
                HorizontalZone[] hz = new HorizontalZone[m_horizontalZones.Count];
                m_horizontalZones.CopyTo (hz, 0);
                return hz;
            }
            set
            {
                m_horizontalZones.Clear ();

                HashSet<HorizontalZone> uniqueHorizontalZones = new HashSet<HorizontalZone> (value);
                foreach (HorizontalZone zone in uniqueHorizontalZones)
                {
                    m_horizontalZones.Add (zone);
                }
            }
        }

        [XmlIgnore]
        public BindingList<HorizontalZone> ChartHorizontalZoneBindingList
        {
            get
            {
                BindingList<HorizontalZone> bl = new BindingList<HorizontalZone> ();
                foreach (HorizontalZone h in m_horizontalZones)
                {
                    bl.Add (h);
                }
                return bl;
            }
        }

        private HashSet<Note> m_notes = new HashSet<Note> ();
        public Note[] ChartNotes
        {
            get
            {
                Note[] n = new Note[m_notes.Count];
                m_notes.CopyTo (n, 0);
                return n;
            }
            set
            {
                m_notes.Clear ();

                HashSet<Note> uniqueNotes = new HashSet<Note> (value);
                foreach (Note note in uniqueNotes)
                {
                    m_notes.Add (note);
                }
            }
        }

        [XmlIgnore]
        public BindingList<Note> ChartNoteBindingList
        {
            get
            {
                BindingList<Note> bl = new BindingList<Note> ();
                foreach (Note n in m_notes)
                {
                    bl.Add (n);
                }
                return bl;
            }
        }

        private HashSet<CircleMarker> m_circleMarkers = new HashSet<CircleMarker> ();
        public CircleMarker[] ChartCircleMarkers
        {
            get
            {
                CircleMarker[] m = new CircleMarker[m_circleMarkers.Count];
                m_circleMarkers.CopyTo (m, 0);
                return m;
            }
            set
            {
                m_circleMarkers.Clear ();

                HashSet<CircleMarker> uniqueCircleMarkers = new HashSet<CircleMarker> (value);
                foreach (CircleMarker circle in uniqueCircleMarkers)
                {
                    m_circleMarkers.Add (circle);
                }
            }
        }

        [XmlIgnore]
        public BindingList<CircleMarker> ChartCircleMarkersBindingList
        {
            get
            {
                BindingList<CircleMarker> bl = new BindingList<CircleMarker> ();
                foreach (CircleMarker m in m_circleMarkers)
                {
                    bl.Add (m);
                }
                return bl;
            }
        }

        #endregion

        #region Methods

        public void AddChartVerticalLine (VerticalLine line)
        {
            m_verticalLines.Add (line);
        }

        public void RemoveVerticalLine (VerticalLine line)
        {
            m_verticalLines.Remove (line);
        }

        public void RemoveVerticalLine (double coordinate, string axisTitle)
        {
            VerticalLine vl = new VerticalLine (coordinate, axisTitle);
            RemoveVerticalLine (vl);
        }

        public void ClearVerticalLines ()
        {
            m_verticalLines.Clear ();
        }

        public void AddChartHorizontalLine (HorizontalLine line)
        {
            m_horizontalLines.Add (line);
        }

        public void RemoveHorizontalLine (HorizontalLine line)
        {
            m_horizontalLines.Remove (line);
        }

        public void RemoveHorizontalLine (double coordinate, string axisTitle)
        {
            HorizontalLine hl = new HorizontalLine (coordinate, axisTitle);
            RemoveHorizontalLine (hl);
        }

        public void ClearHorizontalLines ()
        {
            m_horizontalLines.Clear ();
        }

        public void AddChartVerticalZone (VerticalZone zone)
        {
            m_verticalZones.Add (zone);
        }

        public void RemoveVerticalZone (VerticalZone zone)
        {
            m_verticalZones.Remove (zone);
        }

        public void ClearVerticalZones ()
        {
            m_verticalZones.Clear ();
        }

        public void AddChartHorizontalZone (HorizontalZone zone)
        {
            m_horizontalZones.Add (zone);
        }

        public void RemoveHorizontalZone (HorizontalZone zone)
        {
            m_horizontalZones.Remove (zone);
        }

        public void ClearHorizontalZones ()
        {
            m_horizontalZones.Clear ();
        }

        public void AddChartNote (Note note)
        {
            m_notes.Add (note);
        }

        public void RemoveChartNote (Note note)
        {
            foreach (Note n in m_notes)
            {
                Debug.WriteLine ("Existing note: " + n.GetHashCode ());
            }
            Debug.WriteLine ("Note to remove: " + note.GetHashCode ());

            m_notes.Remove (note);
        }

        public void RemoveChartNote(double xCoordinate, string xAxisTitle, double yCoordinate, string yAxisTitle, Color markColor, string markText)
        {
            Note n = new Note (xCoordinate, xAxisTitle, yCoordinate, yAxisTitle)
            {
                MarkColor = markColor,
                NoteText = markText
            };
            RemoveChartNote (n);
        }

        public void ClearChartNotes ()
        {
            m_notes.Clear ();
        }

        public void AddChartCircleMarker (CircleMarker circle)
        {
            m_circleMarkers.Add (circle);
        }

        public void RemoveChartCircleMarker (CircleMarker circle)
        {
            m_circleMarkers.Remove (circle);
        }

        public void RemoveChartCircleMarker (double xCoordinate, string xAxisTitle, double yCoordinate, string yAxisTitle)
        {
            CircleMarker m = new CircleMarker (xCoordinate, xAxisTitle, yCoordinate, yAxisTitle);
            RemoveChartCircleMarker (m);
        }

        public void ClearChartCircleMarkers ()
        {
            m_circleMarkers.Clear ();
        }

        #endregion

        #endregion

        #region ICloneable Implementation

        public object Clone ()
        {
            ChartMetadata cmd = new ChartMetadata ();

            cmd.AreHorizontalLinesVisible = AreHorizontalLinesVisible;
            cmd.AreHorizontalZonesVisible = AreHorizontalZonesVisible;
            cmd.AreNotesVisible = AreNotesVisible;
            cmd.AreVerticalLinesVisible = AreVerticalLinesVisible;
            cmd.AreVerticalZonesVisible = AreVerticalZonesVisible;
            cmd.AreCircleMarkersVisible = AreCircleMarkersVisible;
            cmd.IsChartZoomedIn = IsChartZoomedIn;
            cmd.IsChartZoomInSelected = IsChartZoomInSelected;
            cmd.IsChartZoomOutSelected = IsChartZoomOutSelected;
            cmd.MetadataVersion = MetadataVersion;
            cmd.ChartHorizontalMinimum = ChartHorizontalMinimum;
            cmd.ChartHorizontalMaximum = ChartHorizontalMaximum;

            foreach (Note n in m_notes)
            {
                cmd.AddChartNote (n.Clone () as Note);
            }

            foreach (VerticalLine vl in m_verticalLines)
            {
                cmd.AddChartVerticalLine (vl.Clone () as VerticalLine);
            }

            foreach (VerticalZone vz in m_verticalZones)
            {
                cmd.AddChartVerticalZone (vz.Clone () as VerticalZone);
            }

            foreach (HorizontalLine hl in m_horizontalLines)
            {
                cmd.AddChartHorizontalLine (hl.Clone () as HorizontalLine);
            }

            foreach (HorizontalZone hz in m_horizontalZones)
            {
                cmd.AddChartHorizontalZone (hz.Clone () as HorizontalZone);
            }

            foreach (CircleMarker m in m_circleMarkers)
            {
                cmd.AddChartCircleMarker (m.Clone () as CircleMarker);
            }

            return cmd;
        }

        #endregion
    }
}
