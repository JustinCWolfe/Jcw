namespace Jcw.Common.Gui.WinForms.Forms
{
    partial class JcwMessageBox
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
            this.CancelButton = new Jcw.Common.Gui.WinForms.Controls.JcwSafeButton ();
            this.NoButton = new Jcw.Common.Gui.WinForms.Controls.JcwSafeButton ();
            this.OkButton = new Jcw.Common.Gui.WinForms.Controls.JcwSafeButton ();
            this.YesButton = new Jcw.Common.Gui.WinForms.Controls.JcwSafeButton ();
            this.MessageTextBox = new System.Windows.Forms.TextBox ();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel ();
            ( (System.ComponentModel.ISupportInitialize) ( this.dxErrorProvider ) ).BeginInit ();
            ( (System.ComponentModel.ISupportInitialize) ( this.errorProvider ) ).BeginInit ();
            this.SuspendLayout ();
            // 
            // CancelButton
            // 
            this.CancelButton.Appearance.Font = new System.Drawing.Font ( "Arial", 8F );
            this.CancelButton.Appearance.Options.UseBackColor = true;
            this.CancelButton.Appearance.Options.UseBorderColor = true;
            this.CancelButton.Appearance.Options.UseFont = true;
            this.CancelButton.Appearance.Options.UseForeColor = true;
            this.CancelButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CancelButton.Location = new System.Drawing.Point ( 0, 0 );
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size ( 75, 23 );
            this.CancelButton.TabIndex = 3;
            this.CancelButton.Text = "&Cancel";
            this.CancelButton.Click += new System.EventHandler ( this.CancelButton_Click );
            // 
            // NoButton
            // 
            this.NoButton.Appearance.Font = new System.Drawing.Font ( "Arial", 8F );
            this.NoButton.Appearance.Options.UseBackColor = true;
            this.NoButton.Appearance.Options.UseBorderColor = true;
            this.NoButton.Appearance.Options.UseFont = true;
            this.NoButton.Appearance.Options.UseForeColor = true;
            this.NoButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NoButton.Location = new System.Drawing.Point ( 0, 0 );
            this.NoButton.Name = "NoButton";
            this.NoButton.Size = new System.Drawing.Size ( 75, 23 );
            this.NoButton.TabIndex = 3;
            this.NoButton.Text = "&No";
            this.NoButton.Click += new System.EventHandler ( this.NoButton_Click );
            // 
            // OkButton
            // 
            this.OkButton.Appearance.Font = new System.Drawing.Font ( "Arial", 8F );
            this.OkButton.Appearance.Options.UseBackColor = true;
            this.OkButton.Appearance.Options.UseBorderColor = true;
            this.OkButton.Appearance.Options.UseFont = true;
            this.OkButton.Appearance.Options.UseForeColor = true;
            this.OkButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OkButton.Location = new System.Drawing.Point ( 0, 0 );
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size ( 75, 23 );
            this.OkButton.TabIndex = 3;
            this.OkButton.Text = "&Ok";
            this.OkButton.Click += new System.EventHandler ( this.OkButton_Click );
            // 
            // YesButton
            // 
            this.YesButton.Appearance.Font = new System.Drawing.Font ( "Arial", 8F );
            this.YesButton.Appearance.Options.UseBackColor = true;
            this.YesButton.Appearance.Options.UseBorderColor = true;
            this.YesButton.Appearance.Options.UseFont = true;
            this.YesButton.Appearance.Options.UseForeColor = true;
            this.YesButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.YesButton.Location = new System.Drawing.Point ( 0, 0 );
            this.YesButton.Name = "YesButton";
            this.YesButton.Size = new System.Drawing.Size ( 75, 23 );
            this.YesButton.TabIndex = 3;
            this.YesButton.Text = "&Yes";
            this.YesButton.Click += new System.EventHandler ( this.YesButton_Click );
            // 
            // MessageTextBox
            // 
            this.MessageTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.MessageTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MessageTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MessageTextBox.Location = new System.Drawing.Point ( 0, 0 );
            this.MessageTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.MessageTextBox.Multiline = true;
            this.MessageTextBox.Name = "MessageTextBox";
            this.MessageTextBox.ReadOnly = true;
            this.MessageTextBox.Size = new System.Drawing.Size ( 100, 20 );
            this.MessageTextBox.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add ( new System.Windows.Forms.ColumnStyle ( System.Windows.Forms.SizeType.Percent, 100F ) );
            this.tableLayoutPanel1.Controls.Add ( this.MessageTextBox, 0, 0 );
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding ( 10 );
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point ( 0, 0 );
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add ( new System.Windows.Forms.RowStyle ( System.Windows.Forms.SizeType.Percent, 100F ) );
            this.tableLayoutPanel1.RowStyles.Add ( new System.Windows.Forms.RowStyle ( System.Windows.Forms.SizeType.Absolute, 30F ) );
            this.tableLayoutPanel1.Size = new System.Drawing.Size ( 292, 266 );
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // JcwMessageBox
            // 
            this.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Appearance.Font = new System.Drawing.Font ( "Arial", 8F );
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseFont = true;
            this.ClientSize = new System.Drawing.Size ( 292, 266 );
            this.Controls.Add ( this.tableLayoutPanel1 );
            this.Name = "JcwMessageBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            ( (System.ComponentModel.ISupportInitialize) ( this.dxErrorProvider ) ).EndInit ();
            ( (System.ComponentModel.ISupportInitialize) ( this.errorProvider ) ).EndInit ();
            this.ResumeLayout ( false );
            this.PerformLayout ();

        }

        #endregion

        new private Jcw.Common.Gui.WinForms.Controls.JcwSafeButton CancelButton;
        private Jcw.Common.Gui.WinForms.Controls.JcwSafeButton NoButton;
        private Jcw.Common.Gui.WinForms.Controls.JcwSafeButton OkButton;
        private Jcw.Common.Gui.WinForms.Controls.JcwSafeButton YesButton;

        private System.Windows.Forms.TextBox MessageTextBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
