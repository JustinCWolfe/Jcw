using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

using DevExpress.XtraBars.Docking;

namespace Jcw.Common.Controls
{
    public partial class JcwDockManager : JcwUserControl
    {
        #region Events

        public event EventHandler OnJcwDockChanged;
        public event EventHandler OnJcwVisibleChanged;

        #endregion

        #region Constants

        private int DefaultFloatPanelHeight = 800;
        private int DefaultControlPanelWidth = 400;

        #endregion

        #region Fields

        private int m_autoHideContainerWidth;
        private Font m_autoHideContainerFont;
        private ContainerControl m_containerControl = null;

        #endregion

        #region Properties

        /// <summary>
        ///  Set the parent form for the docking manager
        /// </summary>
        public ContainerControl DockManagerParentForm
        {
            set { m_containerControl = value; }
        }

        /// <summary>
        /// Return the width of the active dock panel in this dock manager
        /// </summary>
        [Browsable ( false )]
        public int DockPanelWidth
        {
            get
            {
                int width = m_autoHideContainerWidth * this.devExpressDockManager.AutoHideContainers.Count;

                // check to see which, if any, panels are visible and add their width to the width of the auto hide containers above
                // only add width for panels that are visible and NOT floating
                foreach ( DockPanel dp in this.devExpressDockManager.Panels )
                {
                    if ( dp.Visibility == DockVisibility.Visible && dp.Dock != DockingStyle.Float )
                        width += dp.Width;
                }

                return width;
            }
        }

        #endregion

        #region Contructors

        public JcwDockManager ()
        {
            InitializeComponent ();
        }

        public JcwDockManager ( ContainerControl form )
            : this ( form, 21, JcwStyle.JcwStyleFont )
        {
        }

        public JcwDockManager ( ContainerControl form, int autoHideContainerWidth )
            : this ( form, autoHideContainerWidth, JcwStyle.JcwStyleFont )
        {
        }

        public JcwDockManager ( ContainerControl form, int autoHideContainerWidth, Font autoHideContainerFont )
        {
            m_containerControl = form;
            m_autoHideContainerWidth = autoHideContainerWidth;
            m_autoHideContainerFont = autoHideContainerFont;

            InitializeComponent ();

            // convert images to bitmaps
            Bitmap arrowUp = Properties.Resources.ArrowUp;
            Bitmap arrowRight = Properties.Resources.ArrowRight;
            Bitmap arrowDown = Properties.Resources.ArrowDown;
            Bitmap arrowLeft = Properties.Resources.ArrowLeft;

            // make the background color in this bitmap transparent. the background
            // color is the color of the pixel at (1,1) in the image
            arrowUp.MakeTransparent ( arrowUp.GetPixel ( 1, 1 ) );
            arrowRight.MakeTransparent ( arrowRight.GetPixel ( 1, 1 ) );
            arrowDown.MakeTransparent ( arrowDown.GetPixel ( 1, 1 ) );
            arrowLeft.MakeTransparent ( arrowLeft.GetPixel ( 1, 1 ) );

            // store the bitmaps in the image list
            this.imageList1.Images.Add ( "up", arrowUp );
            this.imageList1.Images.Add ( "right", arrowRight );
            this.imageList1.Images.Add ( "down", arrowDown );
            this.imageList1.Images.Add ( "left", arrowLeft );
        }

        protected override void Dispose ( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                if ( this.devExpressDockManager != null )
                {
                    foreach ( DockPanel dp in this.devExpressDockManager.Panels )
                    {
                        foreach ( Control ctl in dp.ControlContainer.Controls )
                            ctl.Dispose ();
                        dp.ControlContainer.Controls.Clear ();
                    }
                }

                components.Dispose ();
            }
            base.Dispose ( disposing );
        }

        #endregion

        #region Event Handlers

        private void devExpressDockManager_VisibilityChanged ( object sender, VisibilityChangedEventArgs e )
        {
            // if the panel is pinned it becomes visible
            if ( e.OldVisibility == DockVisibility.AutoHide && e.Visibility == DockVisibility.Visible )
            {
                if ( OnJcwVisibleChanged != null )
                    OnJcwVisibleChanged ( this, EventArgs.Empty );
            }
            // if the panel was visible but is being auto hidden
            else if ( e.OldVisibility == DockVisibility.Visible && e.Visibility == DockVisibility.AutoHide )
            {
                if ( OnJcwVisibleChanged != null )
                    OnJcwVisibleChanged ( this, EventArgs.Empty );
            }

            // set the size of the control to nothing here because the hide panel has hijacked the original
            // space where the user control was placed. without this step you get a white strip the same
            // width as the hide panel to the immediate right of the hide panel after pinning
            this.Size = new Size ( 0, 0 );
        }

        private void devExpressDockPanel_DockChanged ( object sender, EventArgs e )
        {
            DockPanel dp = sender as DockPanel;

            if ( dp != null )
            {
                // set the dock image index based on the dock style
                switch ( dp.Dock )
                {
                    case DockingStyle.Float:
                        dp.ImageIndex = -1;
                        break;
                    case DockingStyle.Top:
                        dp.ImageIndex = 2;
                        break;
                    case DockingStyle.Right:
                        dp.ImageIndex = 3;
                        break;
                    case DockingStyle.Bottom:
                        dp.ImageIndex = 0;
                        break;
                    case DockingStyle.Left:
                        dp.ImageIndex = 1;
                        break;
                }

                if ( OnJcwDockChanged != null )
                    OnJcwDockChanged ( this, EventArgs.Empty );
            }
        }

        #endregion

        #region Public Methods

        public void HideAllUnpinnedPanels ()
        {
            foreach ( DockPanel dp in this.devExpressDockManager.Panels )
            {
                // verify that the mouse is not currently over our dock panel
                if ( dp.IsDisposed == false && dp.Bounds.Contains ( dp.PointToClient ( Control.MousePosition ) ) == false )
                {
                    // verify that the dock panel visibility is auto hide (it isn't hidden or pinned) and it is not floating
                    if ( dp.Visibility == DockVisibility.AutoHide && dp.Dock != DockingStyle.Float )
                        dp.HideSliding ();
                }
            }
        }

        public void SetDockPanelVisibilityByPanelText ( string panelText, bool visible )
        {
            DockPanel dp = FindDockPanelByPanelText ( panelText );
            if ( dp != null )
            {
                dp.Visibility = ( visible ) ?
                    DockVisibility.Visible :
                    DockVisibility.AutoHide;
            }
        }

        public ControlCollection FindDockPanelControlsByPanelText ( string panelText )
        {
            DockPanel dp = FindDockPanelByPanelText ( panelText );
            if ( dp != null )
                return dp.ControlContainer.Controls;

            return null;
        }

        /// <summary>
        /// Add standard auto hide dock panel with the passed-in text label
        /// </summary>
        /// <param name="panelText"></param>
        public void AddAutoHidePanel ( string panelText )
        {
            AddAutoHidePanel ( panelText, DefaultFloatPanelHeight, DefaultControlPanelWidth );
        }

        /// <summary>
        /// Add standard auto hide dock panel with the passed-in text label and user control to add to panel
        /// </summary>
        /// <param name="panelText"></param>
        /// <param name="control"></param>
        public void AddAutoHidePanel ( string panelText, JcwUserControl control )
        {
            // add new dock panel with same docking style as the currently active docking panel
            DockingStyle ds;
            if ( this.devExpressDockManager.ActivePanel == null )
                ds = DockingStyle.Left;
            else
                ds = this.devExpressDockManager.ActivePanel.Dock;

            DockPanel devExpressDockPanel = this.devExpressDockManager.AddPanel ( ds );
            devExpressDockPanel.Appearance.Font = m_autoHideContainerFont;
            devExpressDockPanel.Appearance.Options.UseFont = true;
            devExpressDockPanel.DockChanged += devExpressDockPanel_DockChanged;
            // some extra horizontal padding for the dock panel title bar
            devExpressDockPanel.FloatSize = new Size ( control.Width, control.Height + m_autoHideContainerWidth );
            devExpressDockPanel.ImageIndex = GetImageIndex ( ds );
            devExpressDockPanel.Name = "devExpressDockPanel";
            devExpressDockPanel.Options.AllowDockBottom = false;
            devExpressDockPanel.Options.AllowDockFill = false;
            devExpressDockPanel.Options.AllowDockTop = false;
            devExpressDockPanel.Options.ShowCloseButton = false;
            devExpressDockPanel.Options.ShowMaximizeButton = true;
            devExpressDockPanel.Options.ShowAutoHideButton = true;
            devExpressDockPanel.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            devExpressDockPanel.Size = new Size ( control.Width, 0 );
            devExpressDockPanel.SavedIndex = 0;
            devExpressDockPanel.TabText = panelText;
            devExpressDockPanel.Text = panelText;
            devExpressDockPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;

            // user control will take up the whole panel
            control.Dock = DockStyle.Fill;
            devExpressDockPanel.ControlContainer.Controls.Add ( control );
        }

        /// <summary>
        /// Add standard auto hide dock panel with the passed-in text label, float panel height and control panel width
        /// </summary>
        /// <param name="panelText"></param>
        /// <param name="floatPanelHeight"></param>
        /// <param name="controlPanelWidth"></param>
        public void AddAutoHidePanel ( string panelText, int floatPanelHeight, int controlPanelWidth )
        {
            // add new dock panel with same docking style as the currently active docking panel
            DockingStyle ds;
            if ( this.devExpressDockManager.ActivePanel == null )
                ds = DockingStyle.Left;
            else
                ds = this.devExpressDockManager.ActivePanel.Dock;

            DockPanel devExpressDockPanel = this.devExpressDockManager.AddPanel ( ds );
            devExpressDockPanel.Appearance.Font = m_autoHideContainerFont;
            devExpressDockPanel.Appearance.Options.UseFont = true;
            devExpressDockPanel.DockChanged += devExpressDockPanel_DockChanged;
            // some extra horizontal padding for the dock panel title bar
            devExpressDockPanel.FloatSize = new System.Drawing.Size ( controlPanelWidth, floatPanelHeight + m_autoHideContainerWidth );
            devExpressDockPanel.ImageIndex = GetImageIndex ( ds );
            devExpressDockPanel.Location = new System.Drawing.Point ( -controlPanelWidth, 0 );
            devExpressDockPanel.Name = "devExpressDockPanel";
            devExpressDockPanel.Options.AllowDockBottom = false;
            devExpressDockPanel.Options.AllowDockFill = false;
            devExpressDockPanel.Options.AllowDockTop = false;
            devExpressDockPanel.Options.ShowCloseButton = false;
            devExpressDockPanel.Options.ShowMaximizeButton = true;
            devExpressDockPanel.Options.ShowAutoHideButton = true;
            devExpressDockPanel.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            devExpressDockPanel.SavedIndex = 0;
            devExpressDockPanel.Size = new System.Drawing.Size ( controlPanelWidth, 0 );
            devExpressDockPanel.TabText = panelText;
            devExpressDockPanel.Text = panelText;
            devExpressDockPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;

            devExpressDockPanel.HideSliding ();
        }

        /// <summary>
        /// Add standard dock panel with the passed-in text label and user control to add to panel
        /// </summary>
        /// <param name="panelText"></param>
        /// <param name="control"></param>
        public void AddPanel ( string panelText, JcwUserControl control )
        {
            // add new dock panel with same docking style as the currently active docking panel
            DockingStyle ds;
            if ( this.devExpressDockManager.ActivePanel == null )
                ds = DockingStyle.Left;
            else
                ds = this.devExpressDockManager.ActivePanel.Dock;

            DockPanel devExpressDockPanel = this.devExpressDockManager.AddPanel ( ds );
            devExpressDockPanel.Appearance.Font = m_autoHideContainerFont;
            devExpressDockPanel.Appearance.Options.UseFont = true;
            devExpressDockPanel.DockChanged += devExpressDockPanel_DockChanged;
            // some extra horizontal padding for the dock panel title bar
            devExpressDockPanel.FloatSize = new Size ( control.Width, control.Height + m_autoHideContainerWidth );
            devExpressDockPanel.ImageIndex = GetImageIndex ( ds );
            devExpressDockPanel.Name = "devExpressDockPanel";
            devExpressDockPanel.Options.AllowDockBottom = false;
            devExpressDockPanel.Options.AllowDockFill = false;
            devExpressDockPanel.Options.AllowDockTop = false;
            devExpressDockPanel.Options.ShowCloseButton = false;
            devExpressDockPanel.Options.ShowMaximizeButton = true;
            devExpressDockPanel.Options.ShowAutoHideButton = true;
            devExpressDockPanel.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            devExpressDockPanel.SavedIndex = 0;
            devExpressDockPanel.Size = new Size ( control.Width, 0 );
            devExpressDockPanel.TabText = panelText;
            devExpressDockPanel.Text = panelText;
            devExpressDockPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;

            // user control will take up the whole panel
            control.Dock = DockStyle.Fill;
            devExpressDockPanel.ControlContainer.Controls.Add ( control );
        }

        #endregion

        #region Private Methods

        private DockPanel FindDockPanelByPanelText (string panelText)
        {
            foreach (DockPanel dockPanel in this.devExpressDockManager.Panels)
            {
                if (panelText.Equals (dockPanel.Text))
                {
                    return dockPanel;
                }
            }

            return null;
        }

        private int GetImageIndex ( DockingStyle ds )
        {
            switch ( ds )
            {
                case DockingStyle.Top:
                    return this.imageList1.Images.IndexOfKey ( "down" );
                case DockingStyle.Right:
                    return this.imageList1.Images.IndexOfKey ( "left" );
                case DockingStyle.Bottom:
                    return this.imageList1.Images.IndexOfKey ( "up" );
                case DockingStyle.Left:
                    return this.imageList1.Images.IndexOfKey ( "right" );
            }

            // the default docking style is left - so right arrow is shown
            return 1;
        }

        #endregion
    }

    #region IJcwDockable interface

    public interface IJcwDockable
    {
    }

    #endregion
}