namespace CryptographicallySecureJournal.Forms
{
    partial class JournalEditorForm
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
            this.txtBox = new System.Windows.Forms.TextBox();
            this.uploadBtn = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.uploadAndExitBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtBox
            // 
            this.txtBox.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.txtBox.Location = new System.Drawing.Point(0, 0);
            this.txtBox.Margin = new System.Windows.Forms.Padding(4);
            this.txtBox.Multiline = true;
            this.txtBox.Name = "txtBox";
            this.txtBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBox.Size = new System.Drawing.Size(550, 619);
            this.txtBox.TabIndex = 0;
            // 
            // uploadBtn
            // 
            this.uploadBtn.Location = new System.Drawing.Point(0, 626);
            this.uploadBtn.Margin = new System.Windows.Forms.Padding(4);
            this.uploadBtn.Name = "uploadBtn";
            this.uploadBtn.Size = new System.Drawing.Size(269, 26);
            this.uploadBtn.TabIndex = 1;
            this.uploadBtn.Text = "Upload";
            this.uploadBtn.UseVisualStyleBackColor = true;
            this.uploadBtn.Click += new System.EventHandler(this.UploadBtnClick);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(0, 660);
            this.progressBar.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(550, 26);
            this.progressBar.TabIndex = 2;
            // 
            // uploadAndExitBtn
            // 
            this.uploadAndExitBtn.Location = new System.Drawing.Point(276, 626);
            this.uploadAndExitBtn.Margin = new System.Windows.Forms.Padding(4);
            this.uploadAndExitBtn.Name = "uploadAndExitBtn";
            this.uploadAndExitBtn.Size = new System.Drawing.Size(276, 26);
            this.uploadAndExitBtn.TabIndex = 3;
            this.uploadAndExitBtn.Text = "Upload && Exit";
            this.uploadAndExitBtn.UseVisualStyleBackColor = true;
            this.uploadAndExitBtn.Click += new System.EventHandler(this.UploadAndExitBtnClick);
            // 
            // JournalEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 692);
            this.Controls.Add(this.uploadAndExitBtn);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.uploadBtn);
            this.Controls.Add(this.txtBox);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "JournalEditorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "JournalEditorForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.JournalEditorFormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBox;
        private System.Windows.Forms.Button uploadBtn;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button uploadAndExitBtn;
    }
}