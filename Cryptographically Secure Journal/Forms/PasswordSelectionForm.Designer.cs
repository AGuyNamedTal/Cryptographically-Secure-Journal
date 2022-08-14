namespace CryptographicallySecureJournal.Forms
{
    partial class PasswordSelectionForm
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
            this.choosePassLbl = new System.Windows.Forms.Label();
            this.pass1TxtBox = new System.Windows.Forms.TextBox();
            this.pass2TxtBox = new System.Windows.Forms.TextBox();
            this.verPassLbl = new System.Windows.Forms.Label();
            this.okBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // choosePassLbl
            // 
            this.choosePassLbl.AutoSize = true;
            this.choosePassLbl.Location = new System.Drawing.Point(115, 9);
            this.choosePassLbl.Name = "choosePassLbl";
            this.choosePassLbl.Size = new System.Drawing.Size(127, 20);
            this.choosePassLbl.TabIndex = 0;
            this.choosePassLbl.Text = "Choose Password:";
            // 
            // pass1TxtBox
            // 
            this.pass1TxtBox.Location = new System.Drawing.Point(12, 39);
            this.pass1TxtBox.Name = "pass1TxtBox";
            this.pass1TxtBox.PasswordChar = '*';
            this.pass1TxtBox.Size = new System.Drawing.Size(333, 27);
            this.pass1TxtBox.TabIndex = 2;
            // 
            // pass2TxtBox
            // 
            this.pass2TxtBox.Location = new System.Drawing.Point(12, 111);
            this.pass2TxtBox.Name = "pass2TxtBox";
            this.pass2TxtBox.PasswordChar = '*';
            this.pass2TxtBox.Size = new System.Drawing.Size(333, 27);
            this.pass2TxtBox.TabIndex = 4;
            // 
            // verPassLbl
            // 
            this.verPassLbl.AutoSize = true;
            this.verPassLbl.Location = new System.Drawing.Point(119, 82);
            this.verPassLbl.Name = "verPassLbl";
            this.verPassLbl.Size = new System.Drawing.Size(116, 20);
            this.verPassLbl.TabIndex = 3;
            this.verPassLbl.Text = "Verify Password:";
            // 
            // okBtn
            // 
            this.okBtn.Location = new System.Drawing.Point(233, 154);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(65, 30);
            this.okBtn.TabIndex = 5;
            this.okBtn.Text = "OK";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.OkBtnClick);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(62, 154);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(69, 30);
            this.cancelBtn.TabIndex = 6;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.CancelBtnClick);
            // 
            // PasswordSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 196);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.pass2TxtBox);
            this.Controls.Add(this.verPassLbl);
            this.Controls.Add(this.pass1TxtBox);
            this.Controls.Add(this.choosePassLbl);
            this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "PasswordSelectionForm";
            this.Text = "Choose New Password";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PasswordSelectionFormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label choosePassLbl;
        private System.Windows.Forms.TextBox pass1TxtBox;
        private System.Windows.Forms.TextBox pass2TxtBox;
        private System.Windows.Forms.Label verPassLbl;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Button cancelBtn;
    }
}