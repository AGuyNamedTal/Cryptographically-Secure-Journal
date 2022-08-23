using CryptographicallySecureJournal.Utils;

namespace CryptographicallySecureJournal.Forms
{
    partial class StartupForm
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
            this.connectToDriveBtn = new System.Windows.Forms.Button();
            this.passwordTxtBox = new System.Windows.Forms.TextBox();
            this.continueBtn = new System.Windows.Forms.Button();
            this.securitySettingsLbl = new System.Windows.Forms.Label();
            this.ProgressBar = new UpdateableProgressBar();
            this.connectAutomaticallyCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // connectToDriveBtn
            // 
            this.connectToDriveBtn.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.connectToDriveBtn.Location = new System.Drawing.Point(111, 14);
            this.connectToDriveBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.connectToDriveBtn.Name = "connectToDriveBtn";
            this.connectToDriveBtn.Size = new System.Drawing.Size(275, 63);
            this.connectToDriveBtn.TabIndex = 0;
            this.connectToDriveBtn.Text = "Connect To Drive";
            this.connectToDriveBtn.UseVisualStyleBackColor = true;
            this.connectToDriveBtn.Click += new System.EventHandler(this.ConnectToDriveBtnClick);
            // 
            // passwordTxtBox
            // 
            this.passwordTxtBox.Location = new System.Drawing.Point(83, 101);
            this.passwordTxtBox.Name = "passwordTxtBox";
            this.passwordTxtBox.PasswordChar = '*';
            this.passwordTxtBox.Size = new System.Drawing.Size(315, 29);
            this.passwordTxtBox.TabIndex = 2;
            this.passwordTxtBox.Text = "password";
            // 
            // continueBtn
            // 
            this.continueBtn.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.continueBtn.Location = new System.Drawing.Point(404, 86);
            this.continueBtn.Name = "continueBtn";
            this.continueBtn.Size = new System.Drawing.Size(58, 60);
            this.continueBtn.TabIndex = 3;
            this.continueBtn.Text = "→";
            this.continueBtn.UseVisualStyleBackColor = true;
            this.continueBtn.Click += new System.EventHandler(this.ContinueBtnClick);
            // 
            // securitySettingsLbl
            // 
            this.securitySettingsLbl.AutoSize = true;
            this.securitySettingsLbl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.securitySettingsLbl.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.securitySettingsLbl.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.securitySettingsLbl.ForeColor = System.Drawing.SystemColors.Highlight;
            this.securitySettingsLbl.Location = new System.Drawing.Point(172, 141);
            this.securitySettingsLbl.Name = "securitySettingsLbl";
            this.securitySettingsLbl.Size = new System.Drawing.Size(165, 30);
            this.securitySettingsLbl.TabIndex = 4;
            this.securitySettingsLbl.Text = "Security Options";
            this.securitySettingsLbl.Click += new System.EventHandler(this.SecuritySettingsLblClick);
            // 
            // ProgressBar
            // 
            this.ProgressBar.Location = new System.Drawing.Point(1, 240);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(494, 17);
            this.ProgressBar.TabIndex = 5;
            // 
            // connectAutomaticallyCheckBox
            // 
            this.connectAutomaticallyCheckBox.AutoSize = true;
            this.connectAutomaticallyCheckBox.Location = new System.Drawing.Point(8, 201);
            this.connectAutomaticallyCheckBox.Name = "connectAutomaticallyCheckBox";
            this.connectAutomaticallyCheckBox.Size = new System.Drawing.Size(315, 25);
            this.connectAutomaticallyCheckBox.TabIndex = 6;
            this.connectAutomaticallyCheckBox.Text = "Connect to drive automatically on startup";
            this.connectAutomaticallyCheckBox.UseVisualStyleBackColor = true;
            this.connectAutomaticallyCheckBox.CheckedChanged += new System.EventHandler(this.ConnectAutomaticallyCheckedChanged);
            // 
            // StartupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 256);
            this.Controls.Add(this.connectAutomaticallyCheckBox);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.securitySettingsLbl);
            this.Controls.Add(this.continueBtn);
            this.Controls.Add(this.passwordTxtBox);
            this.Controls.Add(this.connectToDriveBtn);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "StartupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Journal";
            this.Load += new System.EventHandler(this.StartupFormLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button connectToDriveBtn;
        private System.Windows.Forms.TextBox passwordTxtBox;
        private System.Windows.Forms.Button continueBtn;
        private System.Windows.Forms.Label securitySettingsLbl;
        public UpdateableProgressBar ProgressBar;
        private System.Windows.Forms.CheckBox connectAutomaticallyCheckBox;
    }
}

