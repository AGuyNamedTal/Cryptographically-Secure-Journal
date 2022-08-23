using CryptographicallySecureJournal.Utils;

namespace CryptographicallySecureJournal.Forms
{
    partial class NewJournalForm
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
            this.enterPassLbl = new System.Windows.Forms.Label();
            this.verPassLbl = new System.Windows.Forms.Label();
            this.passTxtBox = new System.Windows.Forms.TextBox();
            this.verPassTxtBox = new System.Windows.Forms.TextBox();
            this.secQuestionsCheckbox = new System.Windows.Forms.CheckBox();
            this.createNewJournalBtn = new System.Windows.Forms.Button();
            this.progressBar = new UpdateableProgressBar();
            this.infoLbl = new System.Windows.Forms.Label();
            this.backBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // enterPassLbl
            // 
            this.enterPassLbl.AutoSize = true;
            this.enterPassLbl.Location = new System.Drawing.Point(11, 42);
            this.enterPassLbl.Name = "enterPassLbl";
            this.enterPassLbl.Size = new System.Drawing.Size(105, 17);
            this.enterPassLbl.TabIndex = 0;
            this.enterPassLbl.Text = "Enter Password: ";
            // 
            // verPassLbl
            // 
            this.verPassLbl.AutoSize = true;
            this.verPassLbl.Location = new System.Drawing.Point(11, 102);
            this.verPassLbl.Name = "verPassLbl";
            this.verPassLbl.Size = new System.Drawing.Size(108, 17);
            this.verPassLbl.TabIndex = 1;
            this.verPassLbl.Text = "Verify Password: ";
            // 
            // passTxtBox
            // 
            this.passTxtBox.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.passTxtBox.Location = new System.Drawing.Point(14, 63);
            this.passTxtBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.passTxtBox.Name = "passTxtBox";
            this.passTxtBox.PasswordChar = '*';
            this.passTxtBox.Size = new System.Drawing.Size(410, 27);
            this.passTxtBox.TabIndex = 2;
            this.passTxtBox.Text = "password";
            // 
            // verPassTxtBox
            // 
            this.verPassTxtBox.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.verPassTxtBox.Location = new System.Drawing.Point(14, 123);
            this.verPassTxtBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.verPassTxtBox.Name = "verPassTxtBox";
            this.verPassTxtBox.PasswordChar = '*';
            this.verPassTxtBox.Size = new System.Drawing.Size(410, 27);
            this.verPassTxtBox.TabIndex = 3;
            this.verPassTxtBox.Text = "password";
            // 
            // secQuestionsCheckbox
            // 
            this.secQuestionsCheckbox.AutoSize = true;
            this.secQuestionsCheckbox.Location = new System.Drawing.Point(14, 169);
            this.secQuestionsCheckbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.secQuestionsCheckbox.Name = "secQuestionsCheckbox";
            this.secQuestionsCheckbox.Size = new System.Drawing.Size(341, 21);
            this.secQuestionsCheckbox.TabIndex = 4;
            this.secQuestionsCheckbox.Text = "Add Security Questions For Backup (RECOMMENDED)";
            this.secQuestionsCheckbox.UseVisualStyleBackColor = true;
            this.secQuestionsCheckbox.CheckedChanged += new System.EventHandler(this.SecQuestionsCheckedChanged);
            // 
            // createNewJournalBtn
            // 
            this.createNewJournalBtn.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.createNewJournalBtn.Location = new System.Drawing.Point(263, 212);
            this.createNewJournalBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.createNewJournalBtn.Name = "createNewJournalBtn";
            this.createNewJournalBtn.Size = new System.Drawing.Size(142, 54);
            this.createNewJournalBtn.TabIndex = 13;
            this.createNewJournalBtn.Text = "Create New Journal";
            this.createNewJournalBtn.UseVisualStyleBackColor = true;
            this.createNewJournalBtn.Click += new System.EventHandler(this.CreateNewJournalBtnClick);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(-1, 284);
            this.progressBar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(677, 19);
            this.progressBar.Step = 1;
            this.progressBar.TabIndex = 14;
            // 
            // infoLbl
            // 
            this.infoLbl.AutoSize = true;
            this.infoLbl.Font = new System.Drawing.Font("Segoe UI", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.infoLbl.Location = new System.Drawing.Point(231, 9);
            this.infoLbl.Name = "infoLbl";
            this.infoLbl.Size = new System.Drawing.Size(215, 17);
            this.infoLbl.TabIndex = 15;
            this.infoLbl.Text = "Security Options For New Journal";
            // 
            // backBtn
            // 
            this.backBtn.Location = new System.Drawing.Point(12, 238);
            this.backBtn.Name = "backBtn";
            this.backBtn.Size = new System.Drawing.Size(49, 28);
            this.backBtn.TabIndex = 16;
            this.backBtn.Text = "Back";
            this.backBtn.UseVisualStyleBackColor = true;
            this.backBtn.Click += new System.EventHandler(this.BackBtnClick);
            // 
            // NewJournalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 316);
            this.Controls.Add(this.backBtn);
            this.Controls.Add(this.infoLbl);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.createNewJournalBtn);
            this.Controls.Add(this.secQuestionsCheckbox);
            this.Controls.Add(this.verPassTxtBox);
            this.Controls.Add(this.passTxtBox);
            this.Controls.Add(this.verPassLbl);
            this.Controls.Add(this.enterPassLbl);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "NewJournalForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create New Journal";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label enterPassLbl;
        private System.Windows.Forms.Label verPassLbl;
        private System.Windows.Forms.TextBox passTxtBox;
        private System.Windows.Forms.TextBox verPassTxtBox;
        private System.Windows.Forms.CheckBox secQuestionsCheckbox;
        private System.Windows.Forms.Button createNewJournalBtn;
        private UpdateableProgressBar progressBar;
        private System.Windows.Forms.Label infoLbl;
        private System.Windows.Forms.Button backBtn;
    }
}