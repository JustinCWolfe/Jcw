using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jcw.Common.Gui.WinForms.Forms
{
    public partial class JcwMessageBox : JcwBaseFixedStyleFrm
    {
        #region Constructors

        private JcwMessageBox ()
        {
            InitializeComponent ();
        }

        #endregion

        #region Static Methods

        private static JcwMessageBox ShowCommon ( string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon )
        {
            JcwMessageBox msgBox = new JcwMessageBox ();

            // set the size of the message box depending on the size of the text it needs to display
            using ( Graphics g = Graphics.FromHwnd ( msgBox.Handle ) )
            {
                SizeF layoutArea = new SizeF ( 600, float.PositiveInfinity );
                SizeF stringSize = g.MeasureString ( text, msgBox.MessageTextBox.Font, layoutArea );

                int padding = 20;

                // layout the message box
                int msgBoxWidth = Convert.ToInt32 ( stringSize.Width + 2 * padding );
                int msgBoxHeight = Convert.ToInt32 ( stringSize.Height + 4 * padding );
                msgBox.ClientSize = new Size ( msgBoxWidth, msgBoxHeight );

                msgBox.MessageTextBox.Text = text;
                msgBox.Text = caption;
                msgBox.CenterToParent ();

                // layout the message box buttons
                msgBox.tableLayoutPanel1.Controls.Add ( msgBox.MessageTextBox, 0, 0 );
                switch ( buttons )
                {
                    case MessageBoxButtons.OK:
                        msgBox.tableLayoutPanel1.Controls.Add ( msgBox.OkButton, 0, 1 );
                        msgBox.OkButton.Select ();
                        break;
                    case MessageBoxButtons.OKCancel:
                        msgBox.tableLayoutPanel1.ColumnCount = 2;
                        msgBox.tableLayoutPanel1.ColumnStyles.Add ( new ColumnStyle ( SizeType.Percent, 50F ) );
                        msgBox.tableLayoutPanel1.ColumnStyles[0].Width = 50F;
                        msgBox.tableLayoutPanel1.Controls.Add ( msgBox.OkButton, 0, 1 );
                        msgBox.tableLayoutPanel1.Controls.Add ( msgBox.CancelButton, 1, 1 );
                        msgBox.tableLayoutPanel1.SetColumnSpan ( msgBox.MessageTextBox, 2 );
                        msgBox.OkButton.Select ();
                        break;
                    case MessageBoxButtons.YesNo:
                        msgBox.tableLayoutPanel1.ColumnCount = 2;
                        msgBox.tableLayoutPanel1.ColumnStyles.Add ( new ColumnStyle ( SizeType.Percent, 50F ) );
                        msgBox.tableLayoutPanel1.ColumnStyles[0].Width = 50F;
                        msgBox.tableLayoutPanel1.Controls.Add ( msgBox.YesButton, 0, 1 );
                        msgBox.tableLayoutPanel1.Controls.Add ( msgBox.NoButton, 1, 1 );
                        msgBox.tableLayoutPanel1.SetColumnSpan ( msgBox.MessageTextBox, 2 );
                        msgBox.YesButton.Select ();
                        break;
                    case MessageBoxButtons.YesNoCancel:
                        msgBox.tableLayoutPanel1.ColumnCount = 3;
                        msgBox.tableLayoutPanel1.ColumnStyles.Add ( new ColumnStyle ( SizeType.Percent, 33.3F ) );
                        msgBox.tableLayoutPanel1.ColumnStyles.Add ( new ColumnStyle ( SizeType.Percent, 33.3F ) );
                        msgBox.tableLayoutPanel1.ColumnStyles[0].Width = 33.3F;
                        msgBox.tableLayoutPanel1.Controls.Add ( msgBox.YesButton, 0, 1 );
                        msgBox.tableLayoutPanel1.Controls.Add ( msgBox.NoButton, 1, 1 );
                        msgBox.tableLayoutPanel1.Controls.Add ( msgBox.CancelButton, 2, 1 );
                        msgBox.tableLayoutPanel1.SetColumnSpan ( msgBox.MessageTextBox, 3 );
                        msgBox.YesButton.Select ();
                        break;
                    default:
                        throw new Exception ( "Only Ok, Yes/No and Yes/No/Cancel message boxes are currently supported" );
                }
            }

            return msgBox;
        }

        public static DialogResult Show ( string text, string caption )
        {
            return ShowCommon ( text, caption, MessageBoxButtons.OK, MessageBoxIcon.None ).ShowDialog ();
        }

        public static DialogResult Show ( IWin32Window owner, string text, string caption )
        {
            return ShowCommon ( text, caption, MessageBoxButtons.OK, MessageBoxIcon.None ).ShowDialog ( owner );
        }

        public static DialogResult Show ( IWin32Window owner, string text, string caption, MessageBoxButtons buttons )
        {
            return ShowCommon ( text, caption, buttons, MessageBoxIcon.None ).ShowDialog ( owner );
        }

        public static DialogResult Show ( IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon )
        {
            return ShowCommon ( text, caption, buttons, icon ).ShowDialog ( owner );
        }

        #endregion

        #region Event Handlers

        private void OkButton_Click ( object sender, EventArgs e )
        {
            this.DialogResult = DialogResult.OK;
            this.Close ();
        }

        private void YesButton_Click ( object sender, EventArgs e )
        {
            this.DialogResult = DialogResult.Yes;
            this.Close ();
        }

        private void NoButton_Click ( object sender, EventArgs e )
        {
            this.DialogResult = DialogResult.No;
            this.Close ();
        }

        private void CancelButton_Click ( object sender, EventArgs e )
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close ();
        }

        #endregion
    }
}

