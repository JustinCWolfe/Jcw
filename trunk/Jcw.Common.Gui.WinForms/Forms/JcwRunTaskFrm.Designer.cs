namespace Jcw.Common.Gui.WinForms.Forms
{
    partial class JcwRunTaskFrm<T>
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ()
        {
            this.Button = new Jcw.Common.Gui.WinForms.Controls.JcwSafeButton ();
            this.MessageTextBox = new System.Windows.Forms.TextBox ();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel ();
            ( (System.ComponentModel.ISupportInitialize) ( this.dxErrorProvider ) ).BeginInit ();
            ( (System.ComponentModel.ISupportInitialize) ( this.errorProvider ) ).BeginInit ();
            this.tableLayoutPanel1.SuspendLayout ();
            this.SuspendLayout ();
            // 
            // Button
            // 
            this.Button.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Button.Font = new System.Drawing.Font ( "Arial", 8F );
            this.Button.Name = "Button";
            this.Button.TabIndex = 3;
            this.Button.Text = "&Cancel";
            this.Button.Click += new System.EventHandler ( this.Button_Click );
            // 
            // MessageTextBox
            // 
            this.MessageTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.MessageTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tableLayoutPanel1.SetColumnSpan ( this.MessageTextBox, 2 );
            this.MessageTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MessageTextBox.Multiline = true;
            this.MessageTextBox.Name = "MessageTextBox";
            this.MessageTextBox.ReadOnly = true;
            this.MessageTextBox.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add ( new System.Windows.Forms.ColumnStyle ( System.Windows.Forms.SizeType.Percent, 50F ) );
            this.tableLayoutPanel1.ColumnStyles.Add ( new System.Windows.Forms.ColumnStyle ( System.Windows.Forms.SizeType.Percent, 50F ) );
            this.tableLayoutPanel1.Controls.Add ( this.MessageTextBox, 0, 0 );
            this.tableLayoutPanel1.Controls.Add ( this.Button, 0, 1 );
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add ( new System.Windows.Forms.RowStyle ( System.Windows.Forms.SizeType.Percent, 100F ) );
            this.tableLayoutPanel1.RowStyles.Add ( new System.Windows.Forms.RowStyle ( System.Windows.Forms.SizeType.Absolute, 40F ) );
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // JcwRunTaskFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF ( 6F, 14F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add ( this.tableLayoutPanel1 );
            this.Name = "JcwRunTaskFrm";
            this.Padding = new System.Windows.Forms.Padding ( 15 );
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            ( (System.ComponentModel.ISupportInitialize) ( this.dxErrorProvider ) ).EndInit ();
            ( (System.ComponentModel.ISupportInitialize) ( this.errorProvider ) ).EndInit ();
            this.tableLayoutPanel1.ResumeLayout ( false );
            this.tableLayoutPanel1.PerformLayout ();
            this.ResumeLayout ( false );

        }

        #endregion

        protected Jcw.Common.Gui.WinForms.Controls.JcwSafeButton Button;
        protected System.Windows.Forms.TextBox MessageTextBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
