namespace PasswordGenerator.Forms
{
    partial class AskLoginForm
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
            this.loginLabel = new System.Windows.Forms.Label();
            this.cancelBtn = new FontAwesome.Sharp.IconButton();
            this.acceptBtn = new FontAwesome.Sharp.IconButton();
            this.loginBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // loginLabel
            // 
            this.loginLabel.AutoSize = true;
            this.loginLabel.Location = new System.Drawing.Point(12, 9);
            this.loginLabel.Name = "loginLabel";
            this.loginLabel.Size = new System.Drawing.Size(116, 21);
            this.loginLabel.TabIndex = 0;
            this.loginLabel.Text = "Введите логин:";
            // 
            // cancelBtn
            // 
            this.cancelBtn.BackColor = System.Drawing.Color.Crimson;
            this.cancelBtn.FlatAppearance.BorderSize = 0;
            this.cancelBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelBtn.IconChar = FontAwesome.Sharp.IconChar.Plus;
            this.cancelBtn.IconColor = System.Drawing.Color.Black;
            this.cancelBtn.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.cancelBtn.IconSize = 39;
            this.cancelBtn.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cancelBtn.Location = new System.Drawing.Point(12, 41);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Rotation = 45D;
            this.cancelBtn.Size = new System.Drawing.Size(183, 54);
            this.cancelBtn.TabIndex = 2;
            this.cancelBtn.Text = "Отмена";
            this.cancelBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cancelBtn.UseVisualStyleBackColor = false;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // acceptBtn
            // 
            this.acceptBtn.BackColor = System.Drawing.Color.Green;
            this.acceptBtn.FlatAppearance.BorderSize = 0;
            this.acceptBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.acceptBtn.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.acceptBtn.IconColor = System.Drawing.Color.Black;
            this.acceptBtn.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.acceptBtn.IconSize = 39;
            this.acceptBtn.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.acceptBtn.Location = new System.Drawing.Point(201, 41);
            this.acceptBtn.Name = "acceptBtn";
            this.acceptBtn.Size = new System.Drawing.Size(183, 54);
            this.acceptBtn.TabIndex = 2;
            this.acceptBtn.Text = "Сохранить";
            this.acceptBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.acceptBtn.UseVisualStyleBackColor = false;
            this.acceptBtn.Click += new System.EventHandler(this.acceptBtn_Click);
            // 
            // loginBox
            // 
            this.loginBox.Location = new System.Drawing.Point(134, 6);
            this.loginBox.Name = "loginBox";
            this.loginBox.Size = new System.Drawing.Size(250, 29);
            this.loginBox.TabIndex = 0;
            // 
            // AskLoginForm
            // 
            this.ClientSize = new System.Drawing.Size(395, 104);
            this.Controls.Add(this.loginBox);
            this.Controls.Add(this.acceptBtn);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.loginLabel);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximumSize = new System.Drawing.Size(411, 143);
            this.MinimumSize = new System.Drawing.Size(411, 143);
            this.Name = "AskLoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Сохранение пароля";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label loginLabel;
        private FontAwesome.Sharp.IconButton cancelBtn;
        private FontAwesome.Sharp.IconButton acceptBtn;
        private System.Windows.Forms.TextBox loginBox;
    }
}