namespace Jcw.Common.Gui.WinForms.Forms
{
    partial class NavigationMdiParentFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MdiMenuStrip = new Jcw.Common.Gui.WinForms.Controls.JcwMenuStrip ();
            this.ArtworkPictureBox = new System.Windows.Forms.PictureBox ();
            this.components = new System.ComponentModel.Container ();
            this.SuspendLayout ();
            // 
            // MdiMenuStrip
            // 
            this.MdiMenuStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.MdiMenuStrip.Name = "MdiMenuStrip";
            this.MdiMenuStrip.TabIndex = 0;
            this.MdiMenuStrip.Margin = new System.Windows.Forms.Padding (0);
            this.MdiMenuStrip.Padding = new System.Windows.Forms.Padding (0);
            this.MdiMenuStrip.Text = "Mdi Menu Strip";
            // 
            // ArtworkPictureBox
            // 
            this.ArtworkPictureBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ArtworkPictureBox.Margin = new System.Windows.Forms.Padding (0);
            this.ArtworkPictureBox.Padding = new System.Windows.Forms.Padding (0);
            this.ArtworkPictureBox.Visible = false;
            // 
            // GCogBaseFrm
            // 
            this.Controls.Add (this.MdiMenuStrip);
            this.Controls.Add (this.ArtworkPictureBox);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.IsMdiContainer = true;
            this.Padding = new System.Windows.Forms.Padding (0);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NavigationMdiParentFrm";
            this.ResumeLayout (false);
            this.PerformLayout ();
        }

        #endregion

        protected Jcw.Common.Gui.WinForms.Controls.JcwMenuStrip MdiMenuStrip;
        protected System.Windows.Forms.PictureBox ArtworkPictureBox;
    }
}