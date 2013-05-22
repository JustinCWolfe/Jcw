using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Jcw.Common.Gui.WinForms.Controls
{
    public partial class JcwStatusStrip : StatusStrip
    {
        #region Constructors

        public JcwStatusStrip ()
            : base ()
        {
            // 
            // JcwStatusStrip
            // 
            this.AutoSize = false;
            this.CanOverflow = true;
            this.Font = Jcw.Common.JcwStyle.JcwStyleFont;
            this.Layout += new System.Windows.Forms.LayoutEventHandler (JcwStatusStrip_Layout);
            this.OverflowButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.OverflowButton.Margin = new System.Windows.Forms.Padding (15);
            this.OverflowButton.Font = Jcw.Common.JcwStyle.JcwStyleFont;
            this.OverflowButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
            this.OverflowButton.Enabled = true;
            this.OverflowButton.Overflow = System.Windows.Forms.ToolStripItemOverflow.AsNeeded;
            this.OverflowButton.Text = "More>>>";
            this.OverflowButton.Visible = true;
            this.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.StackWithOverflow;
            this.Renderer = new Jcw.Common.Gui.WinForms.Controls.JcwToolstripProfessionalRenderer ();
        }

        #endregion

        #region Event Handlers

        private void JcwStatusStrip_Layout (object sender, LayoutEventArgs e)
        {
            using (Graphics g = this.CreateGraphics ())
            {
                foreach (ToolStripItem item in this.Items)
                {
                    if (string.IsNullOrEmpty (item.Text))
                        continue;

                    Form parentForm = this.FindForm ();
                    if (parentForm == null)
                        return;

                    // check the text of each item and if it is longer than the width of the form, add line terminators to wrap the text
                    SizeF statusBarItemTextSize = g.MeasureString (item.Text, item.Font);
                    if (statusBarItemTextSize.Width > parentForm.Width)
                    {
                        int numberOfLineTerminators = Convert.ToInt32 (statusBarItemTextSize.Width / parentForm.Width);
                        for (int index = 1 ; index <= numberOfLineTerminators ; index++)
                            item.Text = item.Text.Insert (item.Text.Length * index / numberOfLineTerminators, "\n");
                    }

                    // if the text of any of the items contains a line terminator, it should be marked as overflow always
                    // because we only display a single line status strip 
                    if (item.Text.Contains ("\n") || item.Text.Contains ("\r") || item.Text.Contains ("\r\n"))
                        item.Overflow = ToolStripItemOverflow.Always;
                    else
                        item.Overflow = ToolStripItemOverflow.Never;
                }
            }
        }

        #endregion
    }
}
