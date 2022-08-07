namespace CryptographicallySecureJournal
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
            this.SuspendLayout();
            // 
            // connectToDriveBtn
            // 
            this.connectToDriveBtn.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.connectToDriveBtn.Location = new System.Drawing.Point(108, 23);
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
            this.passwordTxtBox.Location = new System.Drawing.Point(83, 117);
            this.passwordTxtBox.Name = "passwordTxtBox";
            this.passwordTxtBox.PasswordChar = '*';
            this.passwordTxtBox.Size = new System.Drawing.Size(315, 29);
            this.passwordTxtBox.TabIndex = 2;
            // 
            // continueBtn
            // 
            this.continueBtn.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.continueBtn.Location = new System.Drawing.Point(404, 102);
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
            this.securitySettingsLbl.Location = new System.Drawing.Point(167, 189);
            this.securitySettingsLbl.Name = "securitySettingsLbl";
            this.securitySettingsLbl.Size = new System.Drawing.Size(166, 30);
            this.securitySettingsLbl.TabIndex = 4;
            this.securitySettingsLbl.Text = "Security Settings";
            this.securitySettingsLbl.Click += new System.EventHandler(this.SecuritySettingsLblClick);
            // 
            // StartupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 256);
            this.Controls.Add(this.securitySettingsLbl);
            this.Controls.Add(this.continueBtn);
            this.Controls.Add(this.passwordTxtBox);
            this.Controls.Add(this.connectToDriveBtn);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "StartupForm";
            this.Text = "Journal";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button connectToDriveBtn;
        private System.Windows.Forms.TextBox passwordTxtBox;
        private System.Windows.Forms.Button continueBtn;
        private System.Windows.Forms.Label securitySettingsLbl;
    }
}

