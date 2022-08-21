namespace CryptographicallySecureJournal.Forms
{
    partial class MethodOfRecoveryForm
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
            this.cancelBtn = new System.Windows.Forms.Button();
            this.lbl = new System.Windows.Forms.Label();
            this.passwordRecBtn = new System.Windows.Forms.Button();
            this.bySecQuestionBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(126, 112);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(69, 30);
            this.cancelBtn.TabIndex = 12;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.CancelBtnClick);
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Font = new System.Drawing.Font("Segoe UI", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lbl.Location = new System.Drawing.Point(57, 9);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(213, 20);
            this.lbl.TabIndex = 7;
            this.lbl.Text = "Choose Method Of Recovery:";
            // 
            // passwordRecBtn
            // 
            this.passwordRecBtn.Location = new System.Drawing.Point(32, 40);
            this.passwordRecBtn.Name = "passwordRecBtn";
            this.passwordRecBtn.Size = new System.Drawing.Size(99, 50);
            this.passwordRecBtn.TabIndex = 13;
            this.passwordRecBtn.Text = "By Password";
            this.passwordRecBtn.UseVisualStyleBackColor = true;
            this.passwordRecBtn.Click += new System.EventHandler(this.PasswordRecBtnClick);
            // 
            // bySecQuestionBtn
            // 
            this.bySecQuestionBtn.Location = new System.Drawing.Point(198, 40);
            this.bySecQuestionBtn.Name = "bySecQuestionBtn";
            this.bySecQuestionBtn.Size = new System.Drawing.Size(99, 50);
            this.bySecQuestionBtn.TabIndex = 14;
            this.bySecQuestionBtn.Text = "By Security Questions";
            this.bySecQuestionBtn.UseVisualStyleBackColor = true;
            this.bySecQuestionBtn.Click += new System.EventHandler(this.BySecQuestionBtnClick);
            // 
            // MethodOfRecoveryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 153);
            this.Controls.Add(this.bySecQuestionBtn);
            this.Controls.Add(this.passwordRecBtn);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.lbl);
            this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MethodOfRecoveryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Method of Recovery";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MethodOfRecoveryFormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.Button passwordRecBtn;
        private System.Windows.Forms.Button bySecQuestionBtn;
    }
}