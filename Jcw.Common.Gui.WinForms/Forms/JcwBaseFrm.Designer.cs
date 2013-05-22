namespace Jcw.Common.Gui.WinForms.Forms
{
    partial class JcwBaseFrm
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

        #region Windows Form Designer generated code

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
            this.printDocument = new Jcw.Common.Report.JcwTabularReport ();
            this.printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog ();
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
            // printPreviewDialog
            // 
            this.printPreviewDialog.AutoScrollMargin = new System.Drawing.Size ( 0, 0 );
            this.printPreviewDialog.AutoScrollMinSize = new System.Drawing.Size ( 0, 0 );
            this.printPreviewDialog.ClientSize = new System.Drawing.Size ( 680, 850 );
            this.printPreviewDialog.Document = this.printDocument;
            this.printPreviewDialog.Enabled = true;
            this.printPreviewDialog.MinimumSize = new System.Drawing.Size ( 300, 400 );
            this.printPreviewDialog.Name = "printPreviewDialog";
            this.printPreviewDialog.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.printPreviewDialog.Visible = false;
            this.printPreviewDialog.UseAntiAlias = true;
            // 
            // JcwBaseFrm
            // 
            this.BackColor = Jcw.Common.JcwStyle.JcwStyleControlBackColor;
            this.ClientSize = new System.Drawing.Size ( 300, 300 );
            this.Font = new System.Drawing.Font ( "Arial", 8F );
            this.Load += new System.EventHandler ( Form_Load );
            this.KeyPreview = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler ( JcwBaseFrm_KeyDown );
            this.Name = "JcwBaseFrm";
            ( (System.ComponentModel.ISupportInitialize) ( this.dxErrorProvider ) ).EndInit ();
            ( (System.ComponentModel.ISupportInitialize) ( this.errorProvider ) ).EndInit ();
            this.ResumeLayout ( false );

        }

        #endregion

        protected DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider;
        protected System.Windows.Forms.ToolTip toolTip;
        protected System.Windows.Forms.ErrorProvider errorProvider;
        private Jcw.Common.Report.JcwTabularReport printDocument;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog;
    }
}