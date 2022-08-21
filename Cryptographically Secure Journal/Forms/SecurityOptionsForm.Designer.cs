namespace CryptographicallySecureJournal.Forms
{
    partial class SecurityOptionsForm
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
            this.resetPassBtn = new System.Windows.Forms.Button();
            this.resetSecQuestionsBtn = new System.Windows.Forms.Button();
            this.backupJonLocallyBtn = new System.Windows.Forms.Button();
            this.backupJournalDrive = new System.Windows.Forms.Button();
            this.deleteJonBtn = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // resetPassBtn
            // 
            this.resetPassBtn.Location = new System.Drawing.Point(49, 13);
            this.resetPassBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.resetPassBtn.Name = "resetPassBtn";
            this.resetPassBtn.Size = new System.Drawing.Size(141, 30);
            this.resetPassBtn.TabIndex = 0;
            this.resetPassBtn.Text = "Reset Password";
            this.resetPassBtn.UseVisualStyleBackColor = true;
            this.resetPassBtn.Click += new System.EventHandler(this.ResetPassBtnClick);
            // 
            // resetSecQuestionsBtn
            // 
            this.resetSecQuestionsBtn.Location = new System.Drawing.Point(49, 50);
            this.resetSecQuestionsBtn.Name = "resetSecQuestionsBtn";
            this.resetSecQuestionsBtn.Size = new System.Drawing.Size(141, 47);
            this.resetSecQuestionsBtn.TabIndex = 1;
            this.resetSecQuestionsBtn.Text = "Reset Security Questions";
            this.resetSecQuestionsBtn.UseVisualStyleBackColor = true;
            this.resetSecQuestionsBtn.Click += new System.EventHandler(this.ResetSecQuestionsBtnClick);
            // 
            // backupJonLocallyBtn
            // 
            this.backupJonLocallyBtn.Location = new System.Drawing.Point(49, 103);
            this.backupJonLocallyBtn.Name = "backupJonLocallyBtn";
            this.backupJonLocallyBtn.Size = new System.Drawing.Size(141, 46);
            this.backupJonLocallyBtn.TabIndex = 2;
            this.backupJonLocallyBtn.Text = "Backup Journal Locally";
            this.backupJonLocallyBtn.UseVisualStyleBackColor = true;
            this.backupJonLocallyBtn.Click += new System.EventHandler(this.BackupJonLocallyBtnClick);
            // 
            // backupJournalDrive
            // 
            this.backupJournalDrive.Location = new System.Drawing.Point(49, 155);
            this.backupJournalDrive.Name = "backupJournalDrive";
            this.backupJournalDrive.Size = new System.Drawing.Size(141, 46);
            this.backupJournalDrive.TabIndex = 3;
            this.backupJournalDrive.Text = "Backup Journal on Drive";
            this.backupJournalDrive.UseVisualStyleBackColor = true;
            this.backupJournalDrive.Click += new System.EventHandler(this.BackupJournalDriveClick);
            // 
            // deleteJonBtn
            // 
            this.deleteJonBtn.ForeColor = System.Drawing.Color.Red;
            this.deleteJonBtn.Location = new System.Drawing.Point(49, 216);
            this.deleteJonBtn.Name = "deleteJonBtn";
            this.deleteJonBtn.Size = new System.Drawing.Size(141, 46);
            this.deleteJonBtn.TabIndex = 4;
            this.deleteJonBtn.Text = "Delete Journal From Drive";
            this.deleteJonBtn.UseVisualStyleBackColor = true;
            this.deleteJonBtn.Click += new System.EventHandler(this.DeleteJonBtnClick);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(-1, 282);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(246, 14);
            this.progressBar.TabIndex = 5;
            // 
            // SecurityOptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(244, 295);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.deleteJonBtn);
            this.Controls.Add(this.backupJournalDrive);
            this.Controls.Add(this.backupJonLocallyBtn);
            this.Controls.Add(this.resetSecQuestionsBtn);
            this.Controls.Add(this.resetPassBtn);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "SecurityOptionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Security Options";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button resetPassBtn;
        private System.Windows.Forms.Button resetSecQuestionsBtn;
        private System.Windows.Forms.Button backupJonLocallyBtn;
        private System.Windows.Forms.Button backupJournalDrive;
        private System.Windows.Forms.Button deleteJonBtn;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}