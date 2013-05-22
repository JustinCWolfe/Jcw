namespace WiFlyTestApp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ()
        {
            this.label1 = new System.Windows.Forms.Label ();
            this.CommandToRunTextBox = new System.Windows.Forms.TextBox ();
            this.RunCommandButton = new System.Windows.Forms.Button ();
            this.ResultsTextBox = new System.Windows.Forms.TextBox ();
            this.groupBox1 = new System.Windows.Forms.GroupBox ();
            this.groupBox2 = new System.Windows.Forms.GroupBox ();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel ();
            this.groupBox3 = new System.Windows.Forms.GroupBox ();
            this.DisconnectDeviceButton = new System.Windows.Forms.Button ();
            this.label3 = new System.Windows.Forms.Label ();
            this.ConnectToDeviceButton = new System.Windows.Forms.Button ();
            this.ConnectStatusTextBox = new System.Windows.Forms.TextBox ();
            this.groupBox4 = new System.Windows.Forms.GroupBox ();
            this.AdHocCheckBox = new System.Windows.Forms.CheckBox ();
            this.DisconnectWifiButton = new System.Windows.Forms.Button ();
            this.label4 = new System.Windows.Forms.Label ();
            this.JoinStatusTextBox = new System.Windows.Forms.TextBox ();
            this.label2 = new System.Windows.Forms.Label ();
            this.SSIDToJoinTextBox = new System.Windows.Forms.TextBox ();
            this.JoinSSIDButton = new System.Windows.Forms.Button ();
            this.groupBox5 = new System.Windows.Forms.GroupBox ();
            this.SendDataButton = new System.Windows.Forms.Button ();
            this.DataToSendTextBox = new System.Windows.Forms.TextBox ();
            this.label5 = new System.Windows.Forms.Label ();
            this.groupBox1.SuspendLayout ();
            this.groupBox2.SuspendLayout ();
            this.tableLayoutPanel1.SuspendLayout ();
            this.groupBox3.SuspendLayout ();
            this.groupBox4.SuspendLayout ();
            this.groupBox5.SuspendLayout ();
            this.SuspendLayout ();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point (19, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size (93, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Command To Run";
            // 
            // CommandToRunTextBox
            // 
            this.CommandToRunTextBox.Location = new System.Drawing.Point (22, 35);
            this.CommandToRunTextBox.Name = "CommandToRunTextBox";
            this.CommandToRunTextBox.Size = new System.Drawing.Size (369, 20);
            this.CommandToRunTextBox.TabIndex = 0;
            // 
            // RunCommandButton
            // 
            this.RunCommandButton.Location = new System.Drawing.Point (432, 33);
            this.RunCommandButton.Name = "RunCommandButton";
            this.RunCommandButton.Size = new System.Drawing.Size (132, 23);
            this.RunCommandButton.TabIndex = 1;
            this.RunCommandButton.Text = "&Run Command";
            this.RunCommandButton.UseVisualStyleBackColor = true;
            this.RunCommandButton.Click += new System.EventHandler (this.RunCommandButton_Click);
            // 
            // ResultsTextBox
            // 
            this.ResultsTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResultsTextBox.Location = new System.Drawing.Point (3, 16);
            this.ResultsTextBox.Multiline = true;
            this.ResultsTextBox.Name = "ResultsTextBox";
            this.ResultsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ResultsTextBox.Size = new System.Drawing.Size (574, 321);
            this.ResultsTextBox.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add (this.label1);
            this.groupBox1.Controls.Add (this.CommandToRunTextBox);
            this.groupBox1.Controls.Add (this.RunCommandButton);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point (3, 235);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size (580, 65);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Command Mode";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add (this.ResultsTextBox);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point (3, 306);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size (580, 340);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Output";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add (new System.Windows.Forms.ColumnStyle (System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add (this.groupBox1, 0, 3);
            this.tableLayoutPanel1.Controls.Add (this.groupBox2, 0, 4);
            this.tableLayoutPanel1.Controls.Add (this.groupBox3, 0, 1);
            this.tableLayoutPanel1.Controls.Add (this.groupBox4, 0, 0);
            this.tableLayoutPanel1.Controls.Add (this.groupBox5, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point (0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add (new System.Windows.Forms.RowStyle (System.Windows.Forms.SizeType.Percent, 14F));
            this.tableLayoutPanel1.RowStyles.Add (new System.Windows.Forms.RowStyle (System.Windows.Forms.SizeType.Percent, 11F));
            this.tableLayoutPanel1.RowStyles.Add (new System.Windows.Forms.RowStyle (System.Windows.Forms.SizeType.Percent, 11F));
            this.tableLayoutPanel1.RowStyles.Add (new System.Windows.Forms.RowStyle (System.Windows.Forms.SizeType.Percent, 11F));
            this.tableLayoutPanel1.RowStyles.Add (new System.Windows.Forms.RowStyle (System.Windows.Forms.SizeType.Percent, 53F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size (586, 649);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add (this.DisconnectDeviceButton);
            this.groupBox3.Controls.Add (this.label3);
            this.groupBox3.Controls.Add (this.ConnectToDeviceButton);
            this.groupBox3.Controls.Add (this.ConnectStatusTextBox);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point (3, 93);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size (580, 65);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Connect";
            // 
            // DisconnectDeviceButton
            // 
            this.DisconnectDeviceButton.Location = new System.Drawing.Point (160, 28);
            this.DisconnectDeviceButton.Name = "DisconnectDeviceButton";
            this.DisconnectDeviceButton.Size = new System.Drawing.Size (111, 23);
            this.DisconnectDeviceButton.TabIndex = 1;
            this.DisconnectDeviceButton.Text = "&Disconnect";
            this.DisconnectDeviceButton.UseVisualStyleBackColor = true;
            this.DisconnectDeviceButton.Click += new System.EventHandler (this.DisconnectDeviceButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point (426, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size (80, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Connect Status";
            // 
            // ConnectToDeviceButton
            // 
            this.ConnectToDeviceButton.Location = new System.Drawing.Point (22, 28);
            this.ConnectToDeviceButton.Name = "ConnectToDeviceButton";
            this.ConnectToDeviceButton.Size = new System.Drawing.Size (111, 23);
            this.ConnectToDeviceButton.TabIndex = 0;
            this.ConnectToDeviceButton.Text = "&Connect To WiFly";
            this.ConnectToDeviceButton.UseVisualStyleBackColor = true;
            this.ConnectToDeviceButton.Click += new System.EventHandler (this.ConnectToDeviceButton_Click);
            // 
            // ConnectStatusTextBox
            // 
            this.ConnectStatusTextBox.Location = new System.Drawing.Point (429, 32);
            this.ConnectStatusTextBox.Name = "ConnectStatusTextBox";
            this.ConnectStatusTextBox.ReadOnly = true;
            this.ConnectStatusTextBox.Size = new System.Drawing.Size (135, 20);
            this.ConnectStatusTextBox.TabIndex = 4;
            this.ConnectStatusTextBox.Text = "Not Connected";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add (this.AdHocCheckBox);
            this.groupBox4.Controls.Add (this.DisconnectWifiButton);
            this.groupBox4.Controls.Add (this.label4);
            this.groupBox4.Controls.Add (this.JoinStatusTextBox);
            this.groupBox4.Controls.Add (this.label2);
            this.groupBox4.Controls.Add (this.SSIDToJoinTextBox);
            this.groupBox4.Controls.Add (this.JoinSSIDButton);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point (3, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size (580, 84);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Discover/Join SSID";
            // 
            // AdHocCheckBox
            // 
            this.AdHocCheckBox.AutoSize = true;
            this.AdHocCheckBox.Location = new System.Drawing.Point (22, 61);
            this.AdHocCheckBox.Name = "AdHocCheckBox";
            this.AdHocCheckBox.Size = new System.Drawing.Size (122, 17);
            this.AdHocCheckBox.TabIndex = 1;
            this.AdHocCheckBox.Text = "Adhoc Access Point";
            this.AdHocCheckBox.UseVisualStyleBackColor = true;
            // 
            // DisconnectWifiButton
            // 
            this.DisconnectWifiButton.Location = new System.Drawing.Point (289, 32);
            this.DisconnectWifiButton.Name = "DisconnectWifiButton";
            this.DisconnectWifiButton.Size = new System.Drawing.Size (102, 23);
            this.DisconnectWifiButton.TabIndex = 3;
            this.DisconnectWifiButton.Text = "Disconnect Wifi";
            this.DisconnectWifiButton.UseVisualStyleBackColor = true;
            this.DisconnectWifiButton.Click += new System.EventHandler (this.DisconnectWifiButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point (429, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size (59, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Join Status";
            // 
            // JoinStatusTextBox
            // 
            this.JoinStatusTextBox.Location = new System.Drawing.Point (429, 35);
            this.JoinStatusTextBox.Name = "JoinStatusTextBox";
            this.JoinStatusTextBox.ReadOnly = true;
            this.JoinStatusTextBox.Size = new System.Drawing.Size (135, 20);
            this.JoinStatusTextBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point (19, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size (70, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "SSID To Join";
            // 
            // SSIDToJoinTextBox
            // 
            this.SSIDToJoinTextBox.Location = new System.Drawing.Point (22, 35);
            this.SSIDToJoinTextBox.Name = "SSIDToJoinTextBox";
            this.SSIDToJoinTextBox.Size = new System.Drawing.Size (111, 20);
            this.SSIDToJoinTextBox.TabIndex = 0;
            // 
            // JoinSSIDButton
            // 
            this.JoinSSIDButton.Location = new System.Drawing.Point (160, 32);
            this.JoinSSIDButton.Name = "JoinSSIDButton";
            this.JoinSSIDButton.Size = new System.Drawing.Size (111, 23);
            this.JoinSSIDButton.TabIndex = 2;
            this.JoinSSIDButton.Text = "Join SSID";
            this.JoinSSIDButton.UseVisualStyleBackColor = true;
            this.JoinSSIDButton.Click += new System.EventHandler (this.JoinSSIDButton_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add (this.SendDataButton);
            this.groupBox5.Controls.Add (this.DataToSendTextBox);
            this.groupBox5.Controls.Add (this.label5);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point (3, 164);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size (580, 65);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Data Mode";
            // 
            // SendDataButton
            // 
            this.SendDataButton.Location = new System.Drawing.Point (432, 33);
            this.SendDataButton.Name = "SendDataButton";
            this.SendDataButton.Size = new System.Drawing.Size (132, 23);
            this.SendDataButton.TabIndex = 1;
            this.SendDataButton.Text = "&Send Data";
            this.SendDataButton.UseVisualStyleBackColor = true;
            this.SendDataButton.Click += new System.EventHandler (this.SendDataButton_Click);
            // 
            // DataToSendTextBox
            // 
            this.DataToSendTextBox.Location = new System.Drawing.Point (22, 35);
            this.DataToSendTextBox.Name = "DataToSendTextBox";
            this.DataToSendTextBox.Size = new System.Drawing.Size (369, 20);
            this.DataToSendTextBox.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point (19, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size (74, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Data To Send";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF (6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size (586, 649);
            this.Controls.Add (this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "WiFly Test App";
            this.groupBox1.ResumeLayout (false);
            this.groupBox1.PerformLayout ();
            this.groupBox2.ResumeLayout (false);
            this.groupBox2.PerformLayout ();
            this.tableLayoutPanel1.ResumeLayout (false);
            this.groupBox3.ResumeLayout (false);
            this.groupBox3.PerformLayout ();
            this.groupBox4.ResumeLayout (false);
            this.groupBox4.PerformLayout ();
            this.groupBox5.ResumeLayout (false);
            this.groupBox5.PerformLayout ();
            this.ResumeLayout (false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox CommandToRunTextBox;
        private System.Windows.Forms.Button RunCommandButton;
        private System.Windows.Forms.TextBox ResultsTextBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button ConnectToDeviceButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ConnectStatusTextBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button DisconnectDeviceButton;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox JoinStatusTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox SSIDToJoinTextBox;
        private System.Windows.Forms.Button JoinSSIDButton;
        private System.Windows.Forms.Button DisconnectWifiButton;
        private System.Windows.Forms.CheckBox AdHocCheckBox;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button SendDataButton;
        private System.Windows.Forms.TextBox DataToSendTextBox;
        private System.Windows.Forms.Label label5;
    }
}

