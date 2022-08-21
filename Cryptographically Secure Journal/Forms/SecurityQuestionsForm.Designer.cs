namespace CryptographicallySecureJournal.Forms
{
    partial class SecurityQuestionsForm
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
            this.okBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.secAns4 = new System.Windows.Forms.TextBox();
            this.secQues4 = new System.Windows.Forms.ComboBox();
            this.secAns3 = new System.Windows.Forms.TextBox();
            this.secQues3 = new System.Windows.Forms.ComboBox();
            this.secAns2 = new System.Windows.Forms.TextBox();
            this.secQues2 = new System.Windows.Forms.ComboBox();
            this.secAns1 = new System.Windows.Forms.TextBox();
            this.secQues1 = new System.Windows.Forms.ComboBox();
            this.secQuestionsCheckbox = new System.Windows.Forms.CheckBox();
            this.titleLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // okBtn
            // 
            this.okBtn.Location = new System.Drawing.Point(299, 544);
            this.okBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(87, 30);
            this.okBtn.TabIndex = 0;
            this.okBtn.Text = "OK";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.OkBtnClick);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(202, 544);
            this.cancelBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(87, 30);
            this.cancelBtn.TabIndex = 1;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.CancelBtnClick);
            // 
            // secAns4
            // 
            this.secAns4.Location = new System.Drawing.Point(45, 473);
            this.secAns4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.secAns4.Name = "secAns4";
            this.secAns4.Size = new System.Drawing.Size(519, 25);
            this.secAns4.TabIndex = 21;
            // 
            // secQues4
            // 
            this.secQues4.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.secQues4.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.secQues4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.secQues4.FormattingEnabled = true;
            this.secQues4.Items.AddRange(new object[] {
            "Question1",
            "Question2"});
            this.secQues4.Location = new System.Drawing.Point(45, 426);
            this.secQues4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.secQues4.Name = "secQues4";
            this.secQues4.Size = new System.Drawing.Size(519, 25);
            this.secQues4.TabIndex = 20;
            // 
            // secAns3
            // 
            this.secAns3.Location = new System.Drawing.Point(45, 364);
            this.secAns3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.secAns3.Name = "secAns3";
            this.secAns3.Size = new System.Drawing.Size(519, 25);
            this.secAns3.TabIndex = 19;
            // 
            // secQues3
            // 
            this.secQues3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.secQues3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.secQues3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.secQues3.FormattingEnabled = true;
            this.secQues3.Items.AddRange(new object[] {
            "Question1",
            "Question2"});
            this.secQues3.Location = new System.Drawing.Point(45, 316);
            this.secQues3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.secQues3.Name = "secQues3";
            this.secQues3.Size = new System.Drawing.Size(519, 25);
            this.secQues3.TabIndex = 18;
            // 
            // secAns2
            // 
            this.secAns2.Location = new System.Drawing.Point(45, 252);
            this.secAns2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.secAns2.Name = "secAns2";
            this.secAns2.Size = new System.Drawing.Size(519, 25);
            this.secAns2.TabIndex = 17;
            // 
            // secQues2
            // 
            this.secQues2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.secQues2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.secQues2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.secQues2.FormattingEnabled = true;
            this.secQues2.Items.AddRange(new object[] {
            "Question1",
            "Question2"});
            this.secQues2.Location = new System.Drawing.Point(45, 205);
            this.secQues2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.secQues2.Name = "secQues2";
            this.secQues2.Size = new System.Drawing.Size(519, 25);
            this.secQues2.TabIndex = 16;
            // 
            // secAns1
            // 
            this.secAns1.Location = new System.Drawing.Point(45, 144);
            this.secAns1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.secAns1.Name = "secAns1";
            this.secAns1.Size = new System.Drawing.Size(519, 25);
            this.secAns1.TabIndex = 15;
            // 
            // secQues1
            // 
            this.secQues1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.secQues1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.secQues1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.secQues1.FormattingEnabled = true;
            this.secQues1.Items.AddRange(new object[] {
            "Question1",
            "Question2"});
            this.secQues1.Location = new System.Drawing.Point(45, 97);
            this.secQues1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.secQues1.Name = "secQues1";
            this.secQues1.Size = new System.Drawing.Size(519, 25);
            this.secQues1.TabIndex = 14;
            // 
            // secQuestionsCheckbox
            // 
            this.secQuestionsCheckbox.AutoSize = true;
            this.secQuestionsCheckbox.Checked = true;
            this.secQuestionsCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.secQuestionsCheckbox.Location = new System.Drawing.Point(45, 56);
            this.secQuestionsCheckbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.secQuestionsCheckbox.Name = "secQuestionsCheckbox";
            this.secQuestionsCheckbox.Size = new System.Drawing.Size(341, 21);
            this.secQuestionsCheckbox.TabIndex = 13;
            this.secQuestionsCheckbox.Text = "Add Security Questions For Backup (RECOMMENDED)";
            this.secQuestionsCheckbox.UseVisualStyleBackColor = true;
            this.secQuestionsCheckbox.CheckedChanged += new System.EventHandler(this.SecQuestionsCheckedChanged);
            // 
            // titleLbl
            // 
            this.titleLbl.AutoSize = true;
            this.titleLbl.Font = new System.Drawing.Font("Segoe UI", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.titleLbl.Location = new System.Drawing.Point(198, 22);
            this.titleLbl.Name = "titleLbl";
            this.titleLbl.Size = new System.Drawing.Size(194, 20);
            this.titleLbl.TabIndex = 22;
            this.titleLbl.Text = "Choose Security Questions";
            // 
            // SecurityQuestionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 596);
            this.Controls.Add(this.titleLbl);
            this.Controls.Add(this.secAns4);
            this.Controls.Add(this.secQues4);
            this.Controls.Add(this.secAns3);
            this.Controls.Add(this.secQues3);
            this.Controls.Add(this.secAns2);
            this.Controls.Add(this.secQues2);
            this.Controls.Add(this.secAns1);
            this.Controls.Add(this.secQues1);
            this.Controls.Add(this.secQuestionsCheckbox);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.okBtn);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "SecurityQuestionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Security Questions";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SecurityQuestionsFormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.TextBox secAns4;
        private System.Windows.Forms.ComboBox secQues4;
        private System.Windows.Forms.TextBox secAns3;
        private System.Windows.Forms.ComboBox secQues3;
        private System.Windows.Forms.TextBox secAns2;
        private System.Windows.Forms.ComboBox secQues2;
        private System.Windows.Forms.TextBox secAns1;
        private System.Windows.Forms.ComboBox secQues1;
        private System.Windows.Forms.CheckBox secQuestionsCheckbox;
        private System.Windows.Forms.Label titleLbl;
    }
}