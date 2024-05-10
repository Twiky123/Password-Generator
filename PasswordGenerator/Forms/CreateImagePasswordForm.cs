using NLog;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PasswordGenerator.Forms
{
    public partial class CreateImagePasswordForm : Form
    {
        private Logger logger;
        private PictureGenForm parent;
        private Image loadedImage;
        public CreateImagePasswordForm(PictureGenForm parent)
        {
            logger = LogManager.GetCurrentClassLogger();
            this.parent = parent;
            InitializeComponent();
        }

        private void OnImageUploadClick(object sender, EventArgs e)
        {
            logger.Trace("Начато добавление картинки");
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Bitmap fromFile = null;
                    try
                    {
                        fromFile = Image.FromFile(dialog.FileName) as Bitmap;
                    }
                    catch (Exception exception)
                    {
                        logger.Error($"Ошибка загрузки картинки: {exception}");
                        MessageBox.Show("Ошибка загрузки", "Ошибка");
                        return;
                    }
                    Image imageToDispose = imageBox.Image;
                    imageBox.Image = fromFile;
                    loadedImage = fromFile;
                    imageToDispose?.Dispose();

                    logger.Trace("Картинка добавлена");
                }
                else
                {
                    logger.Trace("Добавление картинки отклонено");
                }
            }
        }

        private void OnEyeClick(object sender, EventArgs e)
        {
            if (openPasswordBtn.IconChar == FontAwesome.Sharp.IconChar.EyeSlash)
            {
                openPasswordBtn.IconChar = FontAwesome.Sharp.IconChar.Eye;
                passwordBox.UseSystemPasswordChar = true;
            }
            else
            {
                openPasswordBtn.IconChar = FontAwesome.Sharp.IconChar.EyeSlash;
                passwordBox.UseSystemPasswordChar = false;
            }
        }

        private void OnSaveClick(object sender, EventArgs e)
        {
            if (passwordBox.Text.Length == 0)
            {
                logger.Warn("Сохранение картинки-пароля отклонено. Не указан пароль");
                MessageBox.Show("Укажите пароль!", "Ошибка");
                return;
            }
            if (imageBox.Image == null)
            {
                logger.Warn("Сохранение картинки-пароля отклонено. Не указана картинка");
                MessageBox.Show("Загрузите картинку!", "Ошибка");
                return;
            }
            ImagePassword imagePassword = new ImagePassword(0, passwordBox.Text, loadedImage);
            imagePassword.Id = SqlConnector.AddToBase(imagePassword);
            parent.AddPassword(imagePassword);
            parent.Parent.backBtn.PerformClick();
            logger.Trace("Сохранение картинки-пароля выполнено!");
        }
    }
}
