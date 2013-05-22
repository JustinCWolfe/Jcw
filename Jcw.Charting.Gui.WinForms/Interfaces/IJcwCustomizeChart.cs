using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Jcw.Common.Gui.WinForms.Controls;

namespace Jcw.Charting.Gui.WinForms.Interfaces
{
    public interface IJcwCustomizeChart
    {
        List<IJcwChartCustomButtonProperties> GetCustomChartButtonProperties ();
        List<IJcwChartCustomContextMenuProperties> GetCustomContextMenuProperties ();
        List<IJcwChartCustomToolstripButtonProperties> GetCustomToolstripButtonProperties ();

        void SetSelectionProperties (double x, double y, string seriesName);
    }

    #region Context Menu Members

    public class JcwChartCustomContextMenu
    {
        IJcwChartCustomContextMenuProperties ContextMenuProperties { get; set; }
    }

    public class JcwChartCustomContextMenuProperties : IJcwChartCustomContextMenuProperties
    {
        public EventHandler ClickEventHandler { get; set; }
        public string Text { get; set; }
        public string HotKey { get; set; }
    }

    public interface IJcwChartCustomContextMenuProperties
    {
        EventHandler ClickEventHandler { get; set; }
        string Text { get; set; }
        string HotKey { get; set; }
    }

    #endregion

    #region Button Members

    public class JcwChartCustomButton
    {
        public PictureBox Button { get; private set; }
        public IJcwChartCustomButtonProperties ButtonProperties { get; private set; }
        public Cursor HotSpotCursor { get; private set; }

        public JcwChartCustomButton (PictureBox button, IJcwChartCustomButtonProperties buttonProperties, Cursor hotSpotCursor)
        {
            Button = button;
            ButtonProperties = buttonProperties;
            HotSpotCursor = hotSpotCursor;
        }
    }

    public class JcwChartCustomButtonProperties : IJcwChartCustomButtonProperties
    {
        public EventHandler ClickEventHandler { get; set; }
        public Bitmap CursorImage { get; set; }
        public Bitmap Image { get; set; }
        public Bitmap SelectedImage { get; set; }
        public string TooltipText { get; set; }
    }

    public interface IJcwChartCustomButtonProperties
    {
        EventHandler ClickEventHandler { get; set; }
        Bitmap CursorImage { get; set; }
        Bitmap Image { get; set; }
        Bitmap SelectedImage { get; set; }
        string TooltipText { get; set; }
    }

    #endregion

    #region Toolstrip Button Members

    public class JcwChartCustomToolstripButton
    {
        public ToolStripButton Button { get; private set; }
        public IJcwChartCustomToolstripButtonProperties ButtonProperties { get; private set; }
        public Cursor HotSpotCursor { get; private set; }

        public JcwChartCustomToolstripButton (ToolStripButton button, IJcwChartCustomToolstripButtonProperties buttonProperties, Cursor hotSpotCursor)
        {
            Button = button;
            ButtonProperties = buttonProperties;
            HotSpotCursor = hotSpotCursor;
        }
    }

    public class JcwChartCustomToolstripButtonProperties : IJcwChartCustomToolstripButtonProperties
    {
        public EventHandler ClickEventHandler { get; set; }
        public Bitmap CursorImage { get; set; }
        public string TooltipText { get; set; }
        public string Text { get; set; }
    }

    public interface IJcwChartCustomToolstripButtonProperties
    {
        EventHandler ClickEventHandler { get; set; }
        Bitmap CursorImage { get; set; }
        string TooltipText { get; set; }
        string Text { get; set; }
    }

    #endregion
}