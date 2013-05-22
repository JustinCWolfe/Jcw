namespace Jcw.Common.Gui.WinForms.Forms
{
    partial class JcwChangeSkinFrm
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
            this.ApplyJcwSafeButton = new Jcw.Common.Gui.WinForms.Controls.JcwSafeButton ();
            this.CancelJcwSafeButton = new Jcw.Common.Gui.WinForms.Controls.JcwSafeButton ();
            this.label1 = new System.Windows.Forms.Label ();
            this.label2 = new System.Windows.Forms.Label ();
            this.jcwGroupBox1 = new Jcw.Common.Gui.WinForms.Controls.JcwGroupBox ();
            this.NewSkinComboBox = new System.Windows.Forms.ComboBox ();
            this.CurrentSkinJcwTextEdit = new Jcw.Common.Gui.WinForms.Controls.JcwTextEdit ();
            ( (System.ComponentModel.ISupportInitialize) ( this.dxErrorProvider ) ).BeginInit ();
            ( (System.ComponentModel.ISupportInitialize) ( this.errorProvider ) ).BeginInit ();
            this.jcwGroupBox1.SuspendLayout ();
            ( (System.ComponentModel.ISupportInitialize) ( this.CurrentSkinJcwTextEdit.Properties ) ).BeginInit ();
            this.SuspendLayout ();
            // 
            // ApplyJcwSafeButton
            // 
            this.ApplyJcwSafeButton.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ApplyJcwSafeButton.Font = new System.Drawing.Font ( "Arial", 8F );
            this.ApplyJcwSafeButton.Location = new System.Drawing.Point ( 23, 137 );
            this.ApplyJcwSafeButton.Name = "ApplyJcwSafeButton";
            this.ApplyJcwSafeButton.Size = new System.Drawing.Size ( 113, 25 );
            this.ApplyJcwSafeButton.TabIndex = 0;
            this.ApplyJcwSafeButton.Text = "&Apply";
            this.ApplyJcwSafeButton.Click += new System.EventHandler ( this.RenameJcwSafeButton_Click );
            // 
            // CancelJcwSafeButton
            // 
            this.CancelJcwSafeButton.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CancelJcwSafeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelJcwSafeButton.Font = new System.Drawing.Font ( "Arial", 8F );
            this.CancelJcwSafeButton.Location = new System.Drawing.Point ( 195, 137 );
            this.CancelJcwSafeButton.Name = "CancelJcwSafeButton";
            this.CancelJcwSafeButton.Size = new System.Drawing.Size ( 113, 25 );
            this.CancelJcwSafeButton.TabIndex = 1;
            this.CancelJcwSafeButton.Text = "&Cancel";
            this.CancelJcwSafeButton.Click += new System.EventHandler ( this.CancelJcwSafeButton_Click );
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point ( 17, 31 );
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size ( 69, 14 );
            this.label1.TabIndex = 4;
            this.label1.Text = "Current Skin:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point ( 17, 67 );
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size ( 56, 14 );
            this.label2.TabIndex = 5;
            this.label2.Text = "New Skin:";
            // 
            // jcwGroupBox1
            // 
            this.jcwGroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.jcwGroupBox1.Controls.Add ( this.NewSkinComboBox );
            this.jcwGroupBox1.Controls.Add ( this.CurrentSkinJcwTextEdit );
            this.jcwGroupBox1.Controls.Add ( this.label1 );
            this.jcwGroupBox1.Controls.Add ( this.label2 );
            this.jcwGroupBox1.Font = new System.Drawing.Font ( "Arial", 8F );
            this.jcwGroupBox1.ForeColor = System.Drawing.Color.FromArgb ( ( (int) ( ( (byte) ( 20 ) ) ) ), ( (int) ( ( (byte) ( 20 ) ) ) ), ( (int) ( ( (byte) ( 20 ) ) ) ) );
            this.jcwGroupBox1.Location = new System.Drawing.Point ( 23, 17 );
            this.jcwGroupBox1.Name = "jcwGroupBox1";
            this.jcwGroupBox1.Size = new System.Drawing.Size ( 285, 103 );
            this.jcwGroupBox1.TabIndex = 6;
            this.jcwGroupBox1.TabStop = false;
            this.jcwGroupBox1.Text = "Application Skin";
            // 
            // NewSkinComboBox
            // 
            this.NewSkinComboBox.FormattingEnabled = true;
            this.NewSkinComboBox.Location = new System.Drawing.Point ( 108, 67 );
            this.NewSkinComboBox.Name = "NewSkinComboBox";
            this.NewSkinComboBox.Size = new System.Drawing.Size ( 158, 22 );
            this.NewSkinComboBox.TabIndex = 7;
            // 
            // CurrentSkinJcwTextEdit
            // 
            this.CurrentSkinJcwTextEdit.Enabled = false;
            this.CurrentSkinJcwTextEdit.Location = new System.Drawing.Point ( 108, 28 );
            this.CurrentSkinJcwTextEdit.Name = "CurrentSkinJcwTextEdit";
            this.CurrentSkinJcwTextEdit.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.CurrentSkinJcwTextEdit.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.CurrentSkinJcwTextEdit.Properties.Appearance.Options.UseBackColor = true;
            this.CurrentSkinJcwTextEdit.Properties.Appearance.Options.UseForeColor = true;
            this.CurrentSkinJcwTextEdit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.CurrentSkinJcwTextEdit.Size = new System.Drawing.Size ( 158, 20 );
            this.CurrentSkinJcwTextEdit.TabIndex = 6;
            // 
            // JcwChangeSkinFrm
            // 
            this.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Appearance.Font = new System.Drawing.Font ( "Arial", 8F );
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF ( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelJcwSafeButton;
            this.ClientSize = new System.Drawing.Size ( 328, 175 );
            this.Controls.Add ( this.jcwGroupBox1 );
            this.Controls.Add ( this.CancelJcwSafeButton );
            this.Controls.Add ( this.ApplyJcwSafeButton );
            this.Name = "JcwChangeSkinFrm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Application Skin";
            ( (System.ComponentModel.ISupportInitialize) ( this.dxErrorProvider ) ).EndInit ();
            ( (System.ComponentModel.ISupportInitialize) ( this.errorProvider ) ).EndInit ();
            this.jcwGroupBox1.ResumeLayout ( false );
            this.jcwGroupBox1.PerformLayout ();
            ( (System.ComponentModel.ISupportInitialize) ( this.CurrentSkinJcwTextEdit.Properties ) ).EndInit ();
            this.ResumeLayout ( false );

        }

        #endregion

        private Jcw.Common.Gui.WinForms.Controls.JcwSafeButton ApplyJcwSafeButton;
        private Jcw.Common.Gui.WinForms.Controls.JcwSafeButton CancelJcwSafeButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Jcw.Common.Gui.WinForms.Controls.JcwGroupBox jcwGroupBox1;
        private Jcw.Common.Gui.WinForms.Controls.JcwTextEdit CurrentSkinJcwTextEdit;
        private System.Windows.Forms.ComboBox NewSkinComboBox;
    }
}
