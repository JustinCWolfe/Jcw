namespace Jcw.Common.Forms
{
    partial class JcwSplashScreenFrm
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
        private void InitializeComponent ()
        {
            this.components = new System.ComponentModel.Container ();
            this.statusLabel = new System.Windows.Forms.Label ();
            this.splashScreenOpacityTimer = new System.Windows.Forms.Timer ( this.components );
            this.SuspendLayout ();
			// 
			// splashScreenOpacityTimer 
			// 
            this.splashScreenOpacityTimer.Tick += new System.EventHandler ( this.splashScreenOpacityTimer_Tick );
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.TabIndex = 4;
            this.statusLabel.Visible = false;
            // 
            // JcwSplashScreenFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF ( 6F, 14F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add ( statusLabel );
            this.DoubleClick += new System.EventHandler ( JcwSplashScreenFrm_DoubleClick );
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "JcwSplashScreenFrm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "JcwSplashScreenFrm";
            this.ResumeLayout ( false );
        }

        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Timer splashScreenOpacityTimer;

        #endregion
    }
}