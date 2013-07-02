namespace GenerateSql
{
    partial class Form1
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
            this.label2 = new System.Windows.Forms.Label ();
            this.DatabaseNameTextBox = new System.Windows.Forms.TextBox ();
            this.label3 = new System.Windows.Forms.Label ();
            this.DatabaseServerTextBox = new System.Windows.Forms.TextBox ();
            this.label4 = new System.Windows.Forms.Label ();
            this.SqlQueryRichTextBox = new System.Windows.Forms.RichTextBox ();
            this.ResultSetRichTextBox = new System.Windows.Forms.RichTextBox ();
            this.label5 = new System.Windows.Forms.Label ();
            this.RunButton = new System.Windows.Forms.Button ();
            this.label1 = new System.Windows.Forms.Label ();
            this.label6 = new System.Windows.Forms.Label ();
            this.InsertTableNameTextBox = new System.Windows.Forms.TextBox ();
            this.KeyStartValueTextBox = new System.Windows.Forms.TextBox ();
            this.KeyColumnTextBox = new System.Windows.Forms.TextBox ();
            this.label7 = new System.Windows.Forms.Label ();
            this.SuspendLayout ();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point ( 20, 49 );
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size ( 84, 13 );
            this.label2.TabIndex = 3;
            this.label2.Text = "Database Name";
            // 
            // DatabaseNameTextBox
            // 
            this.DatabaseNameTextBox.Location = new System.Drawing.Point ( 122, 45 );
            this.DatabaseNameTextBox.Name = "DatabaseNameTextBox";
            this.DatabaseNameTextBox.Size = new System.Drawing.Size ( 161, 20 );
            this.DatabaseNameTextBox.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point ( 20, 23 );
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size ( 87, 13 );
            this.label3.TabIndex = 5;
            this.label3.Text = "Database Server";
            // 
            // DatabaseServerTextBox
            // 
            this.DatabaseServerTextBox.Location = new System.Drawing.Point ( 122, 19 );
            this.DatabaseServerTextBox.Name = "DatabaseServerTextBox";
            this.DatabaseServerTextBox.Size = new System.Drawing.Size ( 161, 20 );
            this.DatabaseServerTextBox.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point ( 20, 80 );
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size ( 53, 13 );
            this.label4.TabIndex = 6;
            this.label4.Text = "Sql Query";
            // 
            // SqlQueryRichTextBox
            // 
            this.SqlQueryRichTextBox.Anchor = ( (System.Windows.Forms.AnchorStyles) ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.SqlQueryRichTextBox.Location = new System.Drawing.Point ( 23, 96 );
            this.SqlQueryRichTextBox.Name = "SqlQueryRichTextBox";
            this.SqlQueryRichTextBox.Size = new System.Drawing.Size ( 1117, 150 );
            this.SqlQueryRichTextBox.TabIndex = 6;
            this.SqlQueryRichTextBox.Text = "";
            // 
            // ResultSetRichTextBox
            // 
            this.ResultSetRichTextBox.Anchor = ( (System.Windows.Forms.AnchorStyles) ( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.ResultSetRichTextBox.Location = new System.Drawing.Point ( 23, 274 );
            this.ResultSetRichTextBox.Name = "ResultSetRichTextBox";
            this.ResultSetRichTextBox.Size = new System.Drawing.Size ( 1117, 364 );
            this.ResultSetRichTextBox.TabIndex = 7;
            this.ResultSetRichTextBox.Text = "";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point ( 20, 258 );
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size ( 56, 13 );
            this.label5.TabIndex = 8;
            this.label5.Text = "Result Set";
            // 
            // RunButton
            // 
            this.RunButton.Anchor = ( (System.Windows.Forms.AnchorStyles) ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.RunButton.Location = new System.Drawing.Point ( 1038, 18 );
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size ( 102, 47 );
            this.RunButton.TabIndex = 8;
            this.RunButton.Text = "&Run";
            this.RunButton.UseVisualStyleBackColor = true;
            this.RunButton.Click += new System.EventHandler ( this.RunButton_Click );
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point ( 315, 49 );
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size ( 80, 13 );
            this.label1.TabIndex = 10;
            this.label1.Text = "Key Start Value";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point ( 315, 23 );
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size ( 94, 13 );
            this.label6.TabIndex = 13;
            this.label6.Text = "Insert Table Name";
            // 
            // InsertTableNameTextBox
            // 
            this.InsertTableNameTextBox.Location = new System.Drawing.Point ( 417, 19 );
            this.InsertTableNameTextBox.Name = "InsertTableNameTextBox";
            this.InsertTableNameTextBox.Size = new System.Drawing.Size ( 161, 20 );
            this.InsertTableNameTextBox.TabIndex = 2;
            // 
            // KeyStartValueTextBox
            // 
            this.KeyStartValueTextBox.Location = new System.Drawing.Point ( 417, 45 );
            this.KeyStartValueTextBox.Name = "KeyStartValueTextBox";
            this.KeyStartValueTextBox.Size = new System.Drawing.Size ( 161, 20 );
            this.KeyStartValueTextBox.TabIndex = 4;
            // 
            // KeyColumnTextBox
            // 
            this.KeyColumnTextBox.Location = new System.Drawing.Point ( 683, 45 );
            this.KeyColumnTextBox.Name = "KeyColumnTextBox";
            this.KeyColumnTextBox.Size = new System.Drawing.Size ( 161, 20 );
            this.KeyColumnTextBox.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point ( 605, 48 );
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size ( 63, 13 );
            this.label7.TabIndex = 15;
            this.label7.Text = "Key Column";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF ( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size ( 1166, 663 );
            this.Controls.Add ( this.KeyColumnTextBox );
            this.Controls.Add ( this.label7 );
            this.Controls.Add ( this.KeyStartValueTextBox );
            this.Controls.Add ( this.label6 );
            this.Controls.Add ( this.InsertTableNameTextBox );
            this.Controls.Add ( this.label1 );
            this.Controls.Add ( this.RunButton );
            this.Controls.Add ( this.ResultSetRichTextBox );
            this.Controls.Add ( this.label5 );
            this.Controls.Add ( this.SqlQueryRichTextBox );
            this.Controls.Add ( this.label4 );
            this.Controls.Add ( this.label3 );
            this.Controls.Add ( this.DatabaseServerTextBox );
            this.Controls.Add ( this.label2 );
            this.Controls.Add ( this.DatabaseNameTextBox );
            this.Name = "Form1";
            this.Text = "Generate Reference Data Sql";
            this.ResumeLayout ( false );
            this.PerformLayout ();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox DatabaseNameTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox DatabaseServerTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox SqlQueryRichTextBox;
        private System.Windows.Forms.RichTextBox ResultSetRichTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button RunButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox InsertTableNameTextBox;
        private System.Windows.Forms.TextBox KeyStartValueTextBox;
        private System.Windows.Forms.TextBox KeyColumnTextBox;
        private System.Windows.Forms.Label label7;
    }
}

