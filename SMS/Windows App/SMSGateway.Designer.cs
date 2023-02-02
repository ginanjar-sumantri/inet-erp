namespace Windows_App
{
    partial class SMSGateway
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SMSGateway));
            this.PortComboBox = new System.Windows.Forms.ComboBox();
            this.StartButton = new System.Windows.Forms.Button();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.OrganizationTextBox = new System.Windows.Forms.TextBox();
            this.UserNameTextBox = new System.Windows.Forms.TextBox();
            this.PortLabel = new System.Windows.Forms.Label();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.OrganizationLabel = new System.Windows.Forms.Label();
            this.UserNameLabel = new System.Windows.Forms.Label();
            this.SignalProgressBar = new System.Windows.Forms.ProgressBar();
            this.SignalStrengthLabel = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.RememberSettingCheckBox = new System.Windows.Forms.CheckBox();
            this.IntervalLabel = new System.Windows.Forms.Label();
            this.IntervalTextBox = new System.Windows.Forms.TextBox();
            this.MiliSecondLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // PortComboBox
            // 
            this.PortComboBox.FormattingEnabled = true;
            this.PortComboBox.Location = new System.Drawing.Point(138, 90);
            this.PortComboBox.Name = "PortComboBox";
            this.PortComboBox.Size = new System.Drawing.Size(71, 21);
            this.PortComboBox.TabIndex = 3;
            // 
            // StartButton
            // 
            this.StartButton.BackColor = System.Drawing.Color.LightGreen;
            this.StartButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.StartButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartButton.ForeColor = System.Drawing.Color.DarkGreen;
            this.StartButton.Location = new System.Drawing.Point(215, 90);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(75, 23);
            this.StartButton.TabIndex = 5;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = false;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Location = new System.Drawing.Point(138, 60);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.PasswordChar = '*';
            this.PasswordTextBox.Size = new System.Drawing.Size(152, 20);
            this.PasswordTextBox.TabIndex = 2;
            // 
            // OrganizationTextBox
            // 
            this.OrganizationTextBox.Location = new System.Drawing.Point(138, 8);
            this.OrganizationTextBox.Name = "OrganizationTextBox";
            this.OrganizationTextBox.Size = new System.Drawing.Size(152, 20);
            this.OrganizationTextBox.TabIndex = 0;
            this.OrganizationTextBox.Leave += new System.EventHandler(this.OrganizationTextBox_Leave);
            // 
            // UserNameTextBox
            // 
            this.UserNameTextBox.Location = new System.Drawing.Point(138, 34);
            this.UserNameTextBox.Name = "UserNameTextBox";
            this.UserNameTextBox.Size = new System.Drawing.Size(152, 20);
            this.UserNameTextBox.TabIndex = 1;
            // 
            // PortLabel
            // 
            this.PortLabel.AutoSize = true;
            this.PortLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PortLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.PortLabel.Location = new System.Drawing.Point(35, 91);
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.Size = new System.Drawing.Size(97, 16);
            this.PortLabel.TabIndex = 7;
            this.PortLabel.Text = "Gateway Port";
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PasswordLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.PasswordLabel.Location = new System.Drawing.Point(61, 58);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(71, 16);
            this.PasswordLabel.TabIndex = 6;
            this.PasswordLabel.Text = "Password";
            // 
            // OrganizationLabel
            // 
            this.OrganizationLabel.AutoSize = true;
            this.OrganizationLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OrganizationLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.OrganizationLabel.Location = new System.Drawing.Point(42, 9);
            this.OrganizationLabel.Name = "OrganizationLabel";
            this.OrganizationLabel.Size = new System.Drawing.Size(90, 16);
            this.OrganizationLabel.TabIndex = 9;
            this.OrganizationLabel.Text = "Organization";
            // 
            // UserNameLabel
            // 
            this.UserNameLabel.AutoSize = true;
            this.UserNameLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserNameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.UserNameLabel.Location = new System.Drawing.Point(60, 34);
            this.UserNameLabel.Name = "UserNameLabel";
            this.UserNameLabel.Size = new System.Drawing.Size(72, 16);
            this.UserNameLabel.TabIndex = 8;
            this.UserNameLabel.Text = "UserName";
            // 
            // SignalProgressBar
            // 
            this.SignalProgressBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.SignalProgressBar.ForeColor = System.Drawing.Color.DeepPink;
            this.SignalProgressBar.Location = new System.Drawing.Point(16, 144);
            this.SignalProgressBar.Maximum = 30;
            this.SignalProgressBar.Name = "SignalProgressBar";
            this.SignalProgressBar.Size = new System.Drawing.Size(274, 33);
            this.SignalProgressBar.TabIndex = 16;
            // 
            // SignalStrengthLabel
            // 
            this.SignalStrengthLabel.AutoSize = true;
            this.SignalStrengthLabel.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SignalStrengthLabel.ForeColor = System.Drawing.Color.DarkMagenta;
            this.SignalStrengthLabel.Location = new System.Drawing.Point(16, 180);
            this.SignalStrengthLabel.Name = "SignalStrengthLabel";
            this.SignalStrengthLabel.Size = new System.Drawing.Size(96, 18);
            this.SignalStrengthLabel.TabIndex = 15;
            this.SignalStrengthLabel.Text = "Signal Strength";
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "SMSGateway";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // RememberSettingCheckBox
            // 
            this.RememberSettingCheckBox.AutoSize = true;
            this.RememberSettingCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.RememberSettingCheckBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RememberSettingCheckBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.RememberSettingCheckBox.Location = new System.Drawing.Point(156, 180);
            this.RememberSettingCheckBox.Name = "RememberSettingCheckBox";
            this.RememberSettingCheckBox.Size = new System.Drawing.Size(134, 18);
            this.RememberSettingCheckBox.TabIndex = 7;
            this.RememberSettingCheckBox.Text = "Remember Settings";
            this.RememberSettingCheckBox.UseVisualStyleBackColor = true;
            // 
            // IntervalLabel
            // 
            this.IntervalLabel.AutoSize = true;
            this.IntervalLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IntervalLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.IntervalLabel.Location = new System.Drawing.Point(33, 119);
            this.IntervalLabel.Name = "IntervalLabel";
            this.IntervalLabel.Size = new System.Drawing.Size(99, 16);
            this.IntervalLabel.TabIndex = 6;
            this.IntervalLabel.Text = "Timer Interval";
            // 
            // IntervalTextBox
            // 
            this.IntervalTextBox.Location = new System.Drawing.Point(138, 119);
            this.IntervalTextBox.Name = "IntervalTextBox";
            this.IntervalTextBox.Size = new System.Drawing.Size(71, 20);
            this.IntervalTextBox.TabIndex = 4;
            this.IntervalTextBox.Text = "5000";
            // 
            // MiliSecondLabel
            // 
            this.MiliSecondLabel.AutoSize = true;
            this.MiliSecondLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MiliSecondLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.MiliSecondLabel.Location = new System.Drawing.Point(214, 119);
            this.MiliSecondLabel.Name = "MiliSecondLabel";
            this.MiliSecondLabel.Size = new System.Drawing.Size(81, 16);
            this.MiliSecondLabel.TabIndex = 6;
            this.MiliSecondLabel.Text = "milli second";
            // 
            // SMSGateway
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Linen;
            this.ClientSize = new System.Drawing.Size(304, 212);
            this.Controls.Add(this.RememberSettingCheckBox);
            this.Controls.Add(this.SignalProgressBar);
            this.Controls.Add(this.SignalStrengthLabel);
            this.Controls.Add(this.PortComboBox);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.IntervalTextBox);
            this.Controls.Add(this.PasswordTextBox);
            this.Controls.Add(this.OrganizationTextBox);
            this.Controls.Add(this.UserNameTextBox);
            this.Controls.Add(this.MiliSecondLabel);
            this.Controls.Add(this.IntervalLabel);
            this.Controls.Add(this.PortLabel);
            this.Controls.Add(this.PasswordLabel);
            this.Controls.Add(this.OrganizationLabel);
            this.Controls.Add(this.UserNameLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(312, 239);
            this.MinimumSize = new System.Drawing.Size(312, 239);
            this.Name = "SMSGateway";
            this.Text = "SMSGateway";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SMSGateway_FormClosed);
            this.Resize += new System.EventHandler(this.SMSGateway_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox PortComboBox;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.TextBox OrganizationTextBox;
        private System.Windows.Forms.TextBox UserNameTextBox;
        private System.Windows.Forms.Label PortLabel;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.Label OrganizationLabel;
        private System.Windows.Forms.Label UserNameLabel;
        private System.Windows.Forms.ProgressBar SignalProgressBar;
        private System.Windows.Forms.Label SignalStrengthLabel;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.CheckBox RememberSettingCheckBox;
        private System.Windows.Forms.Label IntervalLabel;
        private System.Windows.Forms.TextBox IntervalTextBox;
        private System.Windows.Forms.Label MiliSecondLabel;
    }
}