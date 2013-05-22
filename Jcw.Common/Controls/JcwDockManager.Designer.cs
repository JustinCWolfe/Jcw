namespace Jcw.Common.Controls
{
    partial class JcwDockManager
    {
        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ()
        {
            this.components = new System.ComponentModel.Container ();
            this.devExpressDockManager = new DevExpress.XtraBars.Docking.DockManager ( this.components );
            this.imageList1 = new System.Windows.Forms.ImageList ( this.components );
            this.devExpressBarAndDockingController = new DevExpress.XtraBars.BarAndDockingController ( this.components );
            ( (System.ComponentModel.ISupportInitialize) ( this.devExpressDockManager ) ).BeginInit ();
            ( (System.ComponentModel.ISupportInitialize) ( this.devExpressBarAndDockingController ) ).BeginInit ();
            this.SuspendLayout ();
            // 
            // imageList1
            // 
            this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
            // 
            // devExpressDockManager
            // 
            this.devExpressDockManager.Controller = this.devExpressBarAndDockingController;
            this.devExpressDockManager.DockingOptions.HideImmediatelyOnAutoHide = true;
            this.devExpressDockManager.Form = m_containerControl;
            this.devExpressDockManager.Images = this.imageList1;
            this.devExpressDockManager.TopZIndexControls.AddRange ( new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "Jcw.Common.Controls.JcwMenuStrip",
            "Jcw.Common.Controls.JcwStatusStrip" } );
            this.devExpressDockManager.VisibilityChanged += new DevExpress.XtraBars.Docking.VisibilityChangedEventHandler ( devExpressDockManager_VisibilityChanged );
            // 
            // devExpressBarAndDockingController
            // 
            this.devExpressBarAndDockingController.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            this.devExpressBarAndDockingController.LookAndFeel.UseDefaultLookAndFeel = true;
            this.devExpressBarAndDockingController.PropertiesBar.AllowLinkLighting = false;
            this.devExpressBarAndDockingController.AppearancesDocking.ActiveTab.Font = m_autoHideContainerFont;
            this.devExpressBarAndDockingController.AppearancesDocking.FloatFormCaption.Font = m_autoHideContainerFont;
            this.devExpressBarAndDockingController.AppearancesDocking.FloatFormCaptionActive.Font = m_autoHideContainerFont;
            this.devExpressBarAndDockingController.AppearancesDocking.HideContainer.Font = m_autoHideContainerFont;
            this.devExpressBarAndDockingController.AppearancesDocking.HidePanelButton.Font = m_autoHideContainerFont;
            this.devExpressBarAndDockingController.AppearancesDocking.HidePanelButtonActive.Font = m_autoHideContainerFont;
            this.devExpressBarAndDockingController.AppearancesDocking.Panel.Font = m_autoHideContainerFont;
            this.devExpressBarAndDockingController.AppearancesDocking.PanelCaption.Font = m_autoHideContainerFont;
            this.devExpressBarAndDockingController.AppearancesDocking.PanelCaptionActive.Font = m_autoHideContainerFont;
            this.devExpressBarAndDockingController.AppearancesDocking.ActiveTab.Options.UseFont = true;
            this.devExpressBarAndDockingController.AppearancesDocking.FloatFormCaption.Options.UseFont = true;
            this.devExpressBarAndDockingController.AppearancesDocking.FloatFormCaptionActive.Options.UseFont = true;
            this.devExpressBarAndDockingController.AppearancesDocking.HideContainer.Options.UseFont = true;
            this.devExpressBarAndDockingController.AppearancesDocking.HidePanelButton.Options.UseFont = true;
            this.devExpressBarAndDockingController.AppearancesDocking.HidePanelButtonActive.Options.UseFont = true;
            this.devExpressBarAndDockingController.AppearancesDocking.Panel.Options.UseFont = true;
            this.devExpressBarAndDockingController.AppearancesDocking.PanelCaption.Options.UseFont = true;
            this.devExpressBarAndDockingController.AppearancesDocking.PanelCaptionActive.Options.UseFont = true;
            // 
            // JcwDockManager 
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Dock = System.Windows.Forms.DockStyle.Left;
            this.Name = "jcwDockManager";
            this.Size = new System.Drawing.Size ( m_autoHideContainerWidth, 0 );
            ( (System.ComponentModel.ISupportInitialize) ( this.devExpressDockManager ) ).EndInit ();
            ( (System.ComponentModel.ISupportInitialize) ( this.devExpressBarAndDockingController ) ).EndInit ();
            this.ResumeLayout ( false );
            this.PerformLayout ();
        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.ComponentModel.IContainer components = null;
        private DevExpress.XtraBars.Docking.DockManager devExpressDockManager;
        private DevExpress.XtraBars.BarAndDockingController devExpressBarAndDockingController;
    }
}
