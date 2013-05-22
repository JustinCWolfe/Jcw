namespace Jcw.Common.Controls
{
    partial class JcwStatusStrip
    {
        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ()
        {
            // 
            // JcwStatusStrip
            // 
            this.AutoSize = false;
            this.CanOverflow = true;
            this.Font = JcwStyle.JcwStyleFont;
            this.Layout += new System.Windows.Forms.LayoutEventHandler (JcwStatusStrip_Layout);
            this.OverflowButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.OverflowButton.Margin = new System.Windows.Forms.Padding (15);
            this.OverflowButton.Font = JcwStyle.JcwStyleFont;
            this.OverflowButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
            this.OverflowButton.Enabled = true;
            this.OverflowButton.Overflow = System.Windows.Forms.ToolStripItemOverflow.AsNeeded;
            this.OverflowButton.Text = "More>>>";
            this.OverflowButton.Visible = true;
            this.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.StackWithOverflow;
            this.Renderer = new Jcw.Common.Controls.JcwToolstripProfessionalRenderer ();
        }

        #endregion
    }
}
