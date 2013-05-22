namespace Jcw.Common.Gui.WinForms.Controls
{
    partial class JcwUserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose ( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose ();
            }
            base.Dispose ( disposing );
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ()
        {
            this.components = new System.ComponentModel.Container ();
            this.dxErrorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider ( this.components );
            this.toolTip = new System.Windows.Forms.ToolTip ( this.components );
            this.errorProvider = new System.Windows.Forms.ErrorProvider ( this.components );
            this.ContextMenuStrip = new Jcw.Common.Gui.WinForms.Controls.JcwContextMenuStrip ();
            ( (System.ComponentModel.ISupportInitialize) ( this.dxErrorProvider ) ).BeginInit ();
            ( (System.ComponentModel.ISupportInitialize) ( this.errorProvider ) ).BeginInit ();
            this.SuspendLayout ();
            // 
            // dxErrorProvider
            // 
            this.dxErrorProvider.ContainerControl = this;
            //
            // contextMenuStrip
            // 
            this.ContextMenuStrip.Font = new System.Drawing.Font ( "Arial", 8F );
            this.ContextMenuStrip.ImageScalingSize = new System.Drawing.Size ( 20, 20 );
            this.ContextMenuStrip.Name = "contextMenuStrip";
            this.ContextMenuStrip.ShowImageMargin = false;
            this.ContextMenuStrip.Size = new System.Drawing.Size ( 36, 4 );
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // JcwUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF ( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Load += new System.EventHandler ( Control_Load );
            this.Name = "JcwUserControl";
            ( (System.ComponentModel.ISupportInitialize) ( this.dxErrorProvider ) ).EndInit ();
            ( (System.ComponentModel.ISupportInitialize) ( this.errorProvider ) ).EndInit ();
            this.ResumeLayout ( false );
        }

        #endregion

        protected DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider;
        protected System.Windows.Forms.ToolTip toolTip;
        protected System.Windows.Forms.ErrorProvider errorProvider;
    }
}
