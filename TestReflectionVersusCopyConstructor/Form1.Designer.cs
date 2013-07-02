namespace ReflectionVersusCopyConstructorTestApp
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
        protected override void Dispose (bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose ();
            }
            base.Dispose (disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel ();
            this.groupBox1 = new System.Windows.Forms.GroupBox ();
            this.ReflectionDurationTextBox = new System.Windows.Forms.TextBox ();
            this.label2 = new System.Windows.Forms.Label ();
            this.groupBox2 = new System.Windows.Forms.GroupBox ();
            this.CopyConstructorDurationTextBox = new System.Windows.Forms.TextBox ();
            this.label3 = new System.Windows.Forms.Label ();
            this.groupBox3 = new System.Windows.Forms.GroupBox ();
            this.label1 = new System.Windows.Forms.Label ();
            this.IterationsNumericUpDown = new System.Windows.Forms.NumericUpDown ();
            this.RunButton = new System.Windows.Forms.Button ();
            this.tableLayoutPanel1.SuspendLayout ();
            this.groupBox1.SuspendLayout ();
            this.groupBox2.SuspendLayout ();
            this.groupBox3.SuspendLayout ();
            ((System.ComponentModel.ISupportInitialize)(this.IterationsNumericUpDown)).BeginInit ();
            this.SuspendLayout ();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add (new System.Windows.Forms.ColumnStyle (System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add (new System.Windows.Forms.ColumnStyle (System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add (this.groupBox1, 1, 0);
            this.tableLayoutPanel1.Controls.Add (this.groupBox2, 1, 1);
            this.tableLayoutPanel1.Controls.Add (this.groupBox3, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point (0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add (new System.Windows.Forms.RowStyle (System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add (new System.Windows.Forms.RowStyle (System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add (new System.Windows.Forms.RowStyle (System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add (new System.Windows.Forms.RowStyle (System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size (456, 140);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add (this.ReflectionDurationTextBox);
            this.groupBox1.Controls.Add (this.label2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point (231, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size (222, 64);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Reflection";
            // 
            // ReflectionDurationTextBox
            // 
            this.ReflectionDurationTextBox.Location = new System.Drawing.Point (84, 27);
            this.ReflectionDurationTextBox.Name = "ReflectionDurationTextBox";
            this.ReflectionDurationTextBox.ReadOnly = true;
            this.ReflectionDurationTextBox.Size = new System.Drawing.Size (100, 20);
            this.ReflectionDurationTextBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point (31, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size (47, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Duration";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add (this.CopyConstructorDurationTextBox);
            this.groupBox2.Controls.Add (this.label3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point (231, 73);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size (222, 64);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Copy Constructor";
            // 
            // CopyConstructorDurationTextBox
            // 
            this.CopyConstructorDurationTextBox.Location = new System.Drawing.Point (84, 28);
            this.CopyConstructorDurationTextBox.Name = "CopyConstructorDurationTextBox";
            this.CopyConstructorDurationTextBox.ReadOnly = true;
            this.CopyConstructorDurationTextBox.Size = new System.Drawing.Size (100, 20);
            this.CopyConstructorDurationTextBox.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point (31, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size (47, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Duration";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add (this.label1);
            this.groupBox3.Controls.Add (this.IterationsNumericUpDown);
            this.groupBox3.Controls.Add (this.RunButton);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point (3, 3);
            this.groupBox3.Name = "groupBox3";
            this.tableLayoutPanel1.SetRowSpan (this.groupBox3, 2);
            this.groupBox3.Size = new System.Drawing.Size (222, 134);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Configure/Run";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point (54, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size (50, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Iterations";
            // 
            // IterationsNumericUpDown
            // 
            this.IterationsNumericUpDown.Increment = new decimal (new int[] {
            100000,
            0,
            0,
            0});
            this.IterationsNumericUpDown.Location = new System.Drawing.Point (57, 45);
            this.IterationsNumericUpDown.Maximum = new decimal (new int[] {
            100000000,
            0,
            0,
            0});
            this.IterationsNumericUpDown.Name = "IterationsNumericUpDown";
            this.IterationsNumericUpDown.Size = new System.Drawing.Size (100, 20);
            this.IterationsNumericUpDown.TabIndex = 0;
            this.IterationsNumericUpDown.ThousandsSeparator = true;
            this.IterationsNumericUpDown.Value = new decimal (new int[] {
            1000000,
            0,
            0,
            0});
            // 
            // RunButton
            // 
            this.RunButton.Location = new System.Drawing.Point (57, 96);
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size (100, 23);
            this.RunButton.TabIndex = 2;
            this.RunButton.Text = "&Run";
            this.RunButton.UseVisualStyleBackColor = true;
            this.RunButton.Click += new System.EventHandler (this.RunButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF (6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size (456, 140);
            this.Controls.Add (this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Object Creation: Reflection vs. Copy Constuctor Test App";
            this.tableLayoutPanel1.ResumeLayout (false);
            this.groupBox1.ResumeLayout (false);
            this.groupBox1.PerformLayout ();
            this.groupBox2.ResumeLayout (false);
            this.groupBox2.PerformLayout ();
            this.groupBox3.ResumeLayout (false);
            this.groupBox3.PerformLayout ();
            ((System.ComponentModel.ISupportInitialize)(this.IterationsNumericUpDown)).EndInit ();
            this.ResumeLayout (false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox CopyConstructorDurationTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ReflectionDurationTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown IterationsNumericUpDown;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button RunButton;
    }
}

