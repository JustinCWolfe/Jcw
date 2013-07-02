namespace DataTablePerformanceTester
{
    partial class DataTablePerformanceTesterFrm
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
            this.RunColumnOrdinalsTestButton = new DevExpress.XtraEditors.SimpleButton ();
            this.ColumnOrdinalsOutputListBox = new DevExpress.XtraEditors.ListBoxControl ();
            this.ColumnNamesOutputListBox = new DevExpress.XtraEditors.ListBoxControl ();
            this.RunColumnNamesTestButton = new DevExpress.XtraEditors.SimpleButton ();
            this.NumberOfIterationsTextEdit = new DevExpress.XtraEditors.TextEdit ();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl ();
            this.ColumnOrdinalMapperOutputListBox = new DevExpress.XtraEditors.ListBoxControl ();
            this.RunColumnOrdinalMapperTestButton = new DevExpress.XtraEditors.SimpleButton ();
            this.ColumnOrdinalMapperPropertiesOutputListBox = new DevExpress.XtraEditors.ListBoxControl ();
            this.RunColumnOrdinalMapperPropertiesTestButton = new DevExpress.XtraEditors.SimpleButton ();
            this.LinqOutputListBox = new DevExpress.XtraEditors.ListBoxControl ();
            this.RunLinqTestButton = new DevExpress.XtraEditors.SimpleButton ();
            ((System.ComponentModel.ISupportInitialize)(this.ColumnOrdinalsOutputListBox)).BeginInit ();
            ((System.ComponentModel.ISupportInitialize)(this.ColumnNamesOutputListBox)).BeginInit ();
            ((System.ComponentModel.ISupportInitialize)(this.NumberOfIterationsTextEdit.Properties)).BeginInit ();
            ((System.ComponentModel.ISupportInitialize)(this.ColumnOrdinalMapperOutputListBox)).BeginInit ();
            ((System.ComponentModel.ISupportInitialize)(this.ColumnOrdinalMapperPropertiesOutputListBox)).BeginInit ();
            ((System.ComponentModel.ISupportInitialize)(this.LinqOutputListBox)).BeginInit ();
            this.SuspendLayout ();
            // 
            // RunColumnOrdinalsTestButton
            // 
            this.RunColumnOrdinalsTestButton.Location = new System.Drawing.Point (10, 8);
            this.RunColumnOrdinalsTestButton.Name = "RunColumnOrdinalsTestButton";
            this.RunColumnOrdinalsTestButton.Size = new System.Drawing.Size (274, 23);
            this.RunColumnOrdinalsTestButton.TabIndex = 0;
            this.RunColumnOrdinalsTestButton.Text = "Run test using column ordinals";
            this.RunColumnOrdinalsTestButton.Click += new System.EventHandler (this.RunColumnOrdinalsTestButton_Click);
            // 
            // ColumnOrdinalsOutputListBox
            // 
            this.ColumnOrdinalsOutputListBox.Location = new System.Drawing.Point (10, 37);
            this.ColumnOrdinalsOutputListBox.Name = "ColumnOrdinalsOutputListBox";
            this.ColumnOrdinalsOutputListBox.Size = new System.Drawing.Size (609, 80);
            this.ColumnOrdinalsOutputListBox.TabIndex = 2;
            // 
            // ColumnNamesOutputListBox
            // 
            this.ColumnNamesOutputListBox.Location = new System.Drawing.Point (10, 542);
            this.ColumnNamesOutputListBox.Name = "ColumnNamesOutputListBox";
            this.ColumnNamesOutputListBox.Size = new System.Drawing.Size (609, 80);
            this.ColumnNamesOutputListBox.TabIndex = 4;
            // 
            // RunColumnNamesTestButton
            // 
            this.RunColumnNamesTestButton.Location = new System.Drawing.Point (10, 513);
            this.RunColumnNamesTestButton.Name = "RunColumnNamesTestButton";
            this.RunColumnNamesTestButton.Size = new System.Drawing.Size (274, 23);
            this.RunColumnNamesTestButton.TabIndex = 3;
            this.RunColumnNamesTestButton.Text = "Run test using column names";
            this.RunColumnNamesTestButton.Click += new System.EventHandler (this.RunColumnNamesTestButton_Click);
            // 
            // NumberOfIterationsTextEdit
            // 
            this.NumberOfIterationsTextEdit.EditValue = "10000";
            this.NumberOfIterationsTextEdit.Location = new System.Drawing.Point (519, 11);
            this.NumberOfIterationsTextEdit.Name = "NumberOfIterationsTextEdit";
            this.NumberOfIterationsTextEdit.Properties.Appearance.Options.UseTextOptions = true;
            this.NumberOfIterationsTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.NumberOfIterationsTextEdit.Properties.DisplayFormat.FormatString = "#";
            this.NumberOfIterationsTextEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.NumberOfIterationsTextEdit.Properties.EditFormat.FormatString = "#";
            this.NumberOfIterationsTextEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.NumberOfIterationsTextEdit.Size = new System.Drawing.Size (100, 20);
            this.NumberOfIterationsTextEdit.TabIndex = 5;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point (393, 14);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size (120, 13);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "Number of test iterations";
            // 
            // ColumnOrdinalMapperOutputListBox
            // 
            this.ColumnOrdinalMapperOutputListBox.Location = new System.Drawing.Point (10, 163);
            this.ColumnOrdinalMapperOutputListBox.Name = "ColumnOrdinalMapperOutputListBox";
            this.ColumnOrdinalMapperOutputListBox.Size = new System.Drawing.Size (609, 80);
            this.ColumnOrdinalMapperOutputListBox.TabIndex = 8;
            // 
            // RunColumnOrdinalMapperTestButton
            // 
            this.RunColumnOrdinalMapperTestButton.Location = new System.Drawing.Point (10, 134);
            this.RunColumnOrdinalMapperTestButton.Name = "RunColumnOrdinalMapperTestButton";
            this.RunColumnOrdinalMapperTestButton.Size = new System.Drawing.Size (274, 23);
            this.RunColumnOrdinalMapperTestButton.TabIndex = 7;
            this.RunColumnOrdinalMapperTestButton.Text = "Run test using column ordinal mapper fields";
            this.RunColumnOrdinalMapperTestButton.Click += new System.EventHandler (this.RunColumnOrdinalMapperTestButton_Click);
            // 
            // ColumnOrdinalMapperPropertiesOutputListBox
            // 
            this.ColumnOrdinalMapperPropertiesOutputListBox.Location = new System.Drawing.Point (10, 289);
            this.ColumnOrdinalMapperPropertiesOutputListBox.Name = "ColumnOrdinalMapperPropertiesOutputListBox";
            this.ColumnOrdinalMapperPropertiesOutputListBox.Size = new System.Drawing.Size (609, 80);
            this.ColumnOrdinalMapperPropertiesOutputListBox.TabIndex = 10;
            // 
            // RunColumnOrdinalMapperPropertiesTestButton
            // 
            this.RunColumnOrdinalMapperPropertiesTestButton.Location = new System.Drawing.Point (10, 260);
            this.RunColumnOrdinalMapperPropertiesTestButton.Name = "RunColumnOrdinalMapperPropertiesTestButton";
            this.RunColumnOrdinalMapperPropertiesTestButton.Size = new System.Drawing.Size (274, 23);
            this.RunColumnOrdinalMapperPropertiesTestButton.TabIndex = 9;
            this.RunColumnOrdinalMapperPropertiesTestButton.Text = "Run test using column ordinal mapper properties";
            this.RunColumnOrdinalMapperPropertiesTestButton.Click += new System.EventHandler (this.RunColumnOrdinalMapperPropertiesTestButton_Click);
            // 
            // LinqOutputListBox
            // 
            this.LinqOutputListBox.Location = new System.Drawing.Point (10, 416);
            this.LinqOutputListBox.Name = "LinqOutputListBox";
            this.LinqOutputListBox.Size = new System.Drawing.Size (609, 80);
            this.LinqOutputListBox.TabIndex = 12;
            // 
            // RunLinqTestButton
            // 
            this.RunLinqTestButton.Location = new System.Drawing.Point (10, 387);
            this.RunLinqTestButton.Name = "RunLinqTestButton";
            this.RunLinqTestButton.Size = new System.Drawing.Size (274, 23);
            this.RunLinqTestButton.TabIndex = 11;
            this.RunLinqTestButton.Text = "Run test using LINQ";
            this.RunLinqTestButton.Click += new System.EventHandler (this.RunLinqTestButton_Click);
            // 
            // DataTablePerformanceTesterFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF (6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size (631, 633);
            this.Controls.Add (this.LinqOutputListBox);
            this.Controls.Add (this.RunLinqTestButton);
            this.Controls.Add (this.ColumnOrdinalMapperPropertiesOutputListBox);
            this.Controls.Add (this.RunColumnOrdinalMapperPropertiesTestButton);
            this.Controls.Add (this.ColumnOrdinalMapperOutputListBox);
            this.Controls.Add (this.RunColumnOrdinalMapperTestButton);
            this.Controls.Add (this.labelControl1);
            this.Controls.Add (this.NumberOfIterationsTextEdit);
            this.Controls.Add (this.ColumnNamesOutputListBox);
            this.Controls.Add (this.RunColumnNamesTestButton);
            this.Controls.Add (this.ColumnOrdinalsOutputListBox);
            this.Controls.Add (this.RunColumnOrdinalsTestButton);
            this.Name = "DataTablePerformanceTesterFrm";
            this.Text = "DataTable Performance Tester";
            this.Load += new System.EventHandler (this.DataTablePerformanceTesterFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ColumnOrdinalsOutputListBox)).EndInit ();
            ((System.ComponentModel.ISupportInitialize)(this.ColumnNamesOutputListBox)).EndInit ();
            ((System.ComponentModel.ISupportInitialize)(this.NumberOfIterationsTextEdit.Properties)).EndInit ();
            ((System.ComponentModel.ISupportInitialize)(this.ColumnOrdinalMapperOutputListBox)).EndInit ();
            ((System.ComponentModel.ISupportInitialize)(this.ColumnOrdinalMapperPropertiesOutputListBox)).EndInit ();
            ((System.ComponentModel.ISupportInitialize)(this.LinqOutputListBox)).EndInit ();
            this.ResumeLayout (false);
            this.PerformLayout ();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton RunColumnOrdinalsTestButton;
        private DevExpress.XtraEditors.ListBoxControl ColumnOrdinalsOutputListBox;
        private DevExpress.XtraEditors.ListBoxControl ColumnNamesOutputListBox;
        private DevExpress.XtraEditors.SimpleButton RunColumnNamesTestButton;
        private DevExpress.XtraEditors.TextEdit NumberOfIterationsTextEdit;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ListBoxControl ColumnOrdinalMapperOutputListBox;
        private DevExpress.XtraEditors.SimpleButton RunColumnOrdinalMapperTestButton;
        private DevExpress.XtraEditors.ListBoxControl ColumnOrdinalMapperPropertiesOutputListBox;
        private DevExpress.XtraEditors.SimpleButton RunColumnOrdinalMapperPropertiesTestButton;
        private DevExpress.XtraEditors.ListBoxControl LinqOutputListBox;
        private DevExpress.XtraEditors.SimpleButton RunLinqTestButton;
    }
}

