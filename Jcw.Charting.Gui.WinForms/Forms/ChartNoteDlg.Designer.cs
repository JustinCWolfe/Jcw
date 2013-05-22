namespace Jcw.Charting.Gui.WinForms.Forms
{
    partial class ChartNoteDlg
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
            this.AddNoteButton = new Jcw.Common.Gui.WinForms.Controls.JcwSafeButton ();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel ();
            this.jcwMemoEdit1 = new Jcw.Common.Gui.WinForms.Controls.JcwMemoEdit ();
            this.jcwLabel1 = new Jcw.Common.Gui.WinForms.Controls.JcwLabel ();
            this.JcwCancelButton = new Jcw.Common.Gui.WinForms.Controls.JcwSafeButton ();
            ( (System.ComponentModel.ISupportInitialize) ( this.dxErrorProvider ) ).BeginInit ();
            this.tableLayoutPanel1.SuspendLayout ();
            ( (System.ComponentModel.ISupportInitialize) ( this.jcwMemoEdit1.Properties ) ).BeginInit ();
            this.SuspendLayout ();
            // 
            // AddNoteButton
            // 
            this.AddNoteButton.Anchor = ( (System.Windows.Forms.AnchorStyles) ( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.AddNoteButton.BackColor = System.Drawing.Color.WhiteSmoke;
            this.AddNoteButton.Font = new System.Drawing.Font ( "Arial", 8F );
            this.AddNoteButton.Location = new System.Drawing.Point ( 3, 121 );
            this.AddNoteButton.Name = "AddNoteButton";
            this.AddNoteButton.Size = new System.Drawing.Size ( 167, 25 );
            this.AddNoteButton.TabIndex = 2;
            this.AddNoteButton.Text = "&Add Note";
            this.AddNoteButton.Click += new System.EventHandler ( this.AddNoteButton_Click );
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add ( new System.Windows.Forms.ColumnStyle ( System.Windows.Forms.SizeType.Percent, 50F ) );
            this.tableLayoutPanel1.ColumnStyles.Add ( new System.Windows.Forms.ColumnStyle ( System.Windows.Forms.SizeType.Percent, 50F ) );
            this.tableLayoutPanel1.ColumnStyles.Add ( new System.Windows.Forms.ColumnStyle ( System.Windows.Forms.SizeType.Absolute, 20F ) );
            this.tableLayoutPanel1.Controls.Add ( this.JcwCancelButton, 0, 2 );
            this.tableLayoutPanel1.Controls.Add ( this.AddNoteButton, 0, 1 );
            this.tableLayoutPanel1.Controls.Add ( this.jcwMemoEdit1, 1, 0 );
            this.tableLayoutPanel1.Controls.Add ( this.jcwLabel1, 0, 0 );
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point ( 0, 0 );
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add ( new System.Windows.Forms.RowStyle ( System.Windows.Forms.SizeType.Percent, 20F ) );
            this.tableLayoutPanel1.RowStyles.Add ( new System.Windows.Forms.RowStyle ( System.Windows.Forms.SizeType.Percent, 60F ) );
            this.tableLayoutPanel1.RowStyles.Add ( new System.Windows.Forms.RowStyle ( System.Windows.Forms.SizeType.Percent, 20F ) );
            this.tableLayoutPanel1.Size = new System.Drawing.Size ( 347, 149 );
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // jcwMemoEdit1
            // 
            this.jcwMemoEdit1.Anchor = ( (System.Windows.Forms.AnchorStyles) ( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.tableLayoutPanel1.SetColumnSpan ( this.jcwMemoEdit1, 2 );
            this.jcwMemoEdit1.Location = new System.Drawing.Point ( 3, 32 );
            this.jcwMemoEdit1.Name = "jcwMemoEdit1";
            this.jcwMemoEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.jcwMemoEdit1.Size = new System.Drawing.Size ( 341, 83 );
            this.jcwMemoEdit1.TabIndex = 4;
            // 
            // jcwLabel1
            // 
            this.jcwLabel1.Anchor = ( (System.Windows.Forms.AnchorStyles) ( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.jcwLabel1.AutoSize = true;
            this.jcwLabel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.SetColumnSpan ( this.jcwLabel1, 2 );
            this.jcwLabel1.Location = new System.Drawing.Point ( 3, 0 );
            this.jcwLabel1.Name = "jcwLabel1";
            this.jcwLabel1.Size = new System.Drawing.Size ( 341, 29 );
            this.jcwLabel1.TabIndex = 5;
            this.jcwLabel1.Text = "Chart Note Text";
            this.jcwLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // JcwCancelButton
            // 
            this.JcwCancelButton.Anchor = ( (System.Windows.Forms.AnchorStyles) ( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.JcwCancelButton.BackColor = System.Drawing.Color.WhiteSmoke;
            this.JcwCancelButton.Font = new System.Drawing.Font ( "Arial", 8F );
            this.JcwCancelButton.Location = new System.Drawing.Point ( 176, 121 );
            this.JcwCancelButton.Name = "JcwCancelButton";
            this.JcwCancelButton.Size = new System.Drawing.Size ( 168, 25 );
            this.JcwCancelButton.TabIndex = 6;
            this.JcwCancelButton.Text = "Cancel";
            this.JcwCancelButton.Click += new System.EventHandler ( this.JcwCancelButton_Click );
            // 
            // ChartNoteDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF ( 6F, 14F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size ( 347, 149 );
            this.Controls.Add ( this.tableLayoutPanel1 );
            this.Load += new System.EventHandler ( ChartNoteDlg_Load );
            this.Name = "ChartNoteDlg";
            this.Text = "Create Chart Note";
            ( (System.ComponentModel.ISupportInitialize) ( this.dxErrorProvider ) ).EndInit ();
            this.tableLayoutPanel1.ResumeLayout ( false );
            this.tableLayoutPanel1.PerformLayout ();
            ( (System.ComponentModel.ISupportInitialize) ( this.jcwMemoEdit1.Properties ) ).EndInit ();
            this.ResumeLayout ( false );

        }

        #endregion

        private Jcw.Common.Gui.WinForms.Controls.JcwSafeButton AddNoteButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Jcw.Common.Gui.WinForms.Controls.JcwMemoEdit jcwMemoEdit1;
        private Jcw.Common.Gui.WinForms.Controls.JcwLabel jcwLabel1;
        private Jcw.Common.Gui.WinForms.Controls.JcwSafeButton JcwCancelButton;
    }
}
