namespace PasswordGenerator.Forms
{
    partial class CreateImagePasswordForm
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
            this.UploadImage = new System.Windows.Forms.Button();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.imageBox = new System.Windows.Forms.PictureBox();
            this.saveBtn = new System.Windows.Forms.Button();
            this.openPasswordBtn = new FontAwesome.Sharp.IconButton();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // UploadImage
            // 
            this.UploadImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.UploadImage.FlatAppearance.BorderSize = 0;
            this.UploadImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UploadImage.Location = new System.Drawing.Point(214, 51);
            this.UploadImage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.UploadImage.Name = "UploadImage";
            this.UploadImage.Size = new System.Drawing.Size(179, 34);
            this.UploadImage.TabIndex = 1;
            this.UploadImage.Text = "Загрузить картинку";
            this.UploadImage.UseVisualStyleBackColor = false;
            this.UploadImage.Click += new System.EventHandler(this.OnImageUploadClick);
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(18, 15);
            this.passwordLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(66, 21);
            this.passwordLabel.TabIndex = 2;
            this.passwordLabel.Text = "Пароль:";
            // 
            // passwordBox
            // 
            this.passwordBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.passwordBox.Location = new System.Drawing.Point(92, 12);
            this.passwordBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.passwordBox.MinimumSize = new System.Drawing.Size(265, 29);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.Size = new System.Drawing.Size(265, 29);
            this.passwordBox.TabIndex = 4;
            this.passwordBox.UseSystemPasswordChar = true;
            // 
            // imageBox
            // 
            this.imageBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageBox.Location = new System.Drawing.Point(12, 51);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(195, 195);
            this.imageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imageBox.TabIndex = 5;
            this.imageBox.TabStop = false;
            // 
            // saveBtn
            // 
            this.saveBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.saveBtn.FlatAppearance.BorderSize = 0;
            this.saveBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveBtn.Location = new System.Drawing.Point(214, 95);
            this.saveBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(179, 34);
            this.saveBtn.TabIndex = 6;
            this.saveBtn.Text = "Сохранить";
            this.saveBtn.UseVisualStyleBackColor = false;
            this.saveBtn.Click += new System.EventHandler(this.OnSaveClick);
            // 
            // openPasswordBtn
            // 
            this.openPasswordBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.openPasswordBtn.FlatAppearance.BorderSize = 0;
            this.openPasswordBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.openPasswordBtn.IconChar = FontAwesome.Sharp.IconChar.Eye;
            this.openPasswordBtn.IconColor = System.Drawing.Color.Black;
            this.openPasswordBtn.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.openPasswordBtn.IconSize = 30;
            this.openPasswordBtn.Location = new System.Drawing.Point(364, 15);
            this.openPasswordBtn.Name = "openPasswordBtn";
            this.openPasswordBtn.Size = new System.Drawing.Size(28, 24);
            this.openPasswordBtn.TabIndex = 22;
            this.openPasswordBtn.UseVisualStyleBackColor = true;
            this.openPasswordBtn.Click += new System.EventHandler(this.OnEyeClick);
            // 
            // CreateImagePasswordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 263);
            this.Controls.Add(this.openPasswordBtn);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.imageBox);
            this.Controls.Add(this.passwordBox);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.UploadImage);
            this.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "CreateImagePasswordForm";
            this.Text = "Создание картинки-пароля";
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button UploadImage;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox passwordBox;
        private System.Windows.Forms.PictureBox imageBox;
        private System.Windows.Forms.Button saveBtn;
        private FontAwesome.Sharp.IconButton openPasswordBtn;
    }
}