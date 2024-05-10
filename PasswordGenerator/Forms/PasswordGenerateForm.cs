using NLog;
using PasswordGenerator.Forms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PasswordGenerator
{
    public partial class PasswordGenerateForm : Form
    {
        private static Logger logger; //Обект логгера NLog
        private PasswordGenerator generator;
        private MainForm parent;
        public PasswordGenerateForm(MainForm parent, PasswordGenerator generator)
        {
            logger = LogManager.GetCurrentClassLogger();
            InitializeComponent();
            this.parent = parent;
            this.generator = generator;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            lengthUpDown.Value = generator.PasswordLength;
            upperCheckBox.Checked = generator.UseUpperCase;
            lowerCheckBox.Checked = generator.UseLowerCase;
            numberCheckBox.Checked = generator.UseNumbers;
            alphanumCheckBox.Checked = generator.UseNonAlphanumeric;
            similarCheckBox.Checked = generator.ExcludeSimilar;
            ambiguousCheckBox.Checked = generator.ExcludeAmbiguous;
            logger.Trace("Загружены параметры генератора");
        }

        #region Copy, Eye and Save Buttons
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

        private void OnCopyClick(object sender, EventArgs e)
        {
            if (passwordBox.Text.Length > 0)
            {
                copyBtn.IconChar = FontAwesome.Sharp.IconChar.Check;
                copyBtn.IconColor = Color.Green;
                Clipboard.SetText(passwordBox.Text);
                logger.Trace("Пароль скопирован");
                copyLabel.Visible = true;
                copyLabelShowTimer.Start();
            }
        }

        private void OnSaveClick(object sender, EventArgs e)
        {
            if (passwordBox.Text.Length == 0)
            {
                return;
            }
            using (AskLoginForm loginForm = new AskLoginForm())
            {
                loginForm.ShowDialog();
                if (loginForm.Result == null)
                {
                    return;
                }
                List<LoginPassword> foundPassword = SqlConnector.FindPasswordWithLogin(loginForm.Result);
                if (foundPassword.Any(x => x.Decrypt().Equals(passwordBox.Text)))
                {
                    logger.Warn($"Сохранение пароля отклонено. Такой пароль уже сохранён при логине {loginForm.Result}");
                    MessageBox.Show("Такой пароль уже сохранён при этом логине!", "Ошибка");
                    return;
                }
                string encodedPassword = Algorythms.EncryptString(passwordBox.Text, loginForm.Result);
                LoginPassword savePassword = new LoginPassword(0, loginForm.Result, encodedPassword);
                savePassword.Id = SqlConnector.AddToBase(savePassword);
                logger.Trace($"Пароль сохранён при логине {loginForm.Result}");
            }
        }

        private void OnCopyTimerElapsed(object sender, EventArgs e)
        {
            copyBtn.IconChar = FontAwesome.Sharp.IconChar.Copy;
            copyBtn.IconColor = Color.Black;
            copyLabel.Visible = false;
            copyLabelShowTimer.Stop();
        }
        #endregion

        #region CheckBoxes
        private void OnUpperCheckBoxChanged(object sender, EventArgs e)
        {
            if (!(upperCheckBox.Checked || lowerCheckBox.Checked || alphanumCheckBox.Checked || numberCheckBox.Checked))
            {
                upperCheckBox.Checked = !upperCheckBox.Checked;
                MessageBox.Show("Выберите хотя бы одну опцию для генерации", "Ошибка");
                return;
            }
            generator.UseUpperCase = upperCheckBox.Checked;
        }

        private void OnLowerCheckBoxChanged(object sender, EventArgs e)
        {
            if (!(upperCheckBox.Checked || lowerCheckBox.Checked || alphanumCheckBox.Checked || numberCheckBox.Checked))
            {
                lowerCheckBox.Checked = !lowerCheckBox.Checked;
                MessageBox.Show("Выберите хотя бы одну опцию для генерации", "Ошибка");
                return;
            }
            generator.UseLowerCase = lowerCheckBox.Checked;
        }

        private void OnAlphanumCheckBoxChanged(object sender, EventArgs e)
        {
            if (!(upperCheckBox.Checked || lowerCheckBox.Checked || alphanumCheckBox.Checked || numberCheckBox.Checked))
            {
                alphanumCheckBox.Checked = !alphanumCheckBox.Checked;
                MessageBox.Show("Выберите хотя бы одну опцию для генерации", "Ошибка");
                return;
            }
            generator.UseNonAlphanumeric = alphanumCheckBox.Checked;
        }

        private void OnNumberCheckBoxChanged(object sender, EventArgs e)
        {
            if (!(upperCheckBox.Checked || lowerCheckBox.Checked || alphanumCheckBox.Checked || numberCheckBox.Checked))
            {
                numberCheckBox.Checked = !numberCheckBox.Checked;
                MessageBox.Show("Выберите хотя бы одну опцию для генерации", "Ошибка");
                return;
            }
            generator.UseNumbers = numberCheckBox.Checked;
        }

        private void OnSimilarCheckBoxChanged(object sender, EventArgs e)
            => generator.ExcludeSimilar = similarCheckBox.Checked;

        private void OnAmbiguousCheckBoxChanged(object sender, EventArgs e)
            => generator.ExcludeAmbiguous = ambiguousCheckBox.Checked;
        #endregion

        private void OnGenerateClick(object sender, EventArgs e)
        {
            passwordBox.Text = generator.Generate();
            logger.Trace("Пароль сгенерирован");
            generator.SaveJson();
            logger.Trace("Параметры генератора сохранены");
        }

        private void OnLengthUpDownChanged(object sender, EventArgs e)
            => generator.PasswordLength = (int)lengthUpDown.Value;

        private void OnOpenSavedClick(object sender, EventArgs e)
            => parent.OnPicPasswordsClick(null, null);
    }
}
