using FontAwesome.Sharp;
using NLog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PasswordGenerator.Forms
{
    public partial class PictureGenForm : Form
    {
        private Logger logger;
        private const int margin = 5;
        private int lastCountInLine;
        public new MainForm Parent { get; set; }
        private List<ImagePassword> passwords;
        private Color formColor;
        public PictureGenForm(MainForm parent)
        {
            logger = LogManager.GetCurrentClassLogger();
            Parent = parent;
            InitializeComponent();
            formColor = ColorTranslator.FromHtml((string)Tag);
            logger.Trace("Начата загрузка картинок-паролей...");
            passwords = new List<ImagePassword>();
            List<ImagePassword> loadedPasswords = SqlConnector.LoadImagesFromBase();
            loadedPasswords.ForEach(x => AddPassword(x));
            logger.Trace($"Загрузка картинок-паролей завершена! Всего загружено: {passwords.Count}");
        }

        private void OnBorderDraw(object sender, PaintEventArgs e)
        {
            Control senderControl = sender as Control;
            if (senderControl == null)
            {
                return;
            }
            ControlPaint.DrawBorder(e.Graphics, senderControl.ClientRectangle, formColor, ButtonBorderStyle.Solid);
        }

        private void CreatePasswordPanel(Point location, ImagePassword password)
        {
            #region Создание плитки
            Panel passPicPanel = new Panel();
            password.Panel = passPicPanel;
            passPicPanel.BackColor = SystemColors.Control;
            passPicPanel.BorderStyle = BorderStyle.FixedSingle;
            #region Надпись
            Label passLabel = new Label()
            {
                Dock = DockStyle.Top,
                Location = new Point(0, 176),
                Margin = new Padding(4, 0, 4, 0),
                Size = new Size(198, 47),
                BackColor = formColor,
                Text = password.ToString(),
                TextAlign = ContentAlignment.MiddleCenter
            };
            #endregion
            #region Кнопки
            Panel btnPanel = new Panel();
            btnPanel.BackColor = Algorythms.ChangeColorBrightness(formColor, -0.3F);
            #region Показать/Скрыть
            Button openBtn = new Button()
            {
                Dock = DockStyle.Fill,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(0, 0),
                Size = new Size(90, 33),
                TabIndex = 0,
                Tag = true,
                Text = "Показать",
                UseVisualStyleBackColor = true
            };
            openBtn.Click += (object senderObject, EventArgs arg) =>
            {
                bool flag = (bool)openBtn.Tag;
                if (flag)
                {
                    passLabel.Text = password.Decrypt();
                    openBtn.Text = "Скрыть";
                    openBtn.Tag = false;
                }
                else
                {
                    StringBuilder passBuilder = new StringBuilder();
                    for (int i = 0; i < password.Password.Length; i++)
                    {
                        passBuilder.Append('*');
                    }
                    openBtn.Text = "Показать";
                    passLabel.Text = password.ToString();
                    openBtn.Tag = true;
                }
            };
            openBtn.FlatAppearance.BorderSize = 0;
            btnPanel.Controls.Add(openBtn);
            #endregion
            #region Копировать
            Button copyBtn = new Button()
            {
                Dock = DockStyle.Right,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(0, 0),
                Size = new Size(110, 33),
                TabIndex = 0,
                Text = "Копировать",
                Tag = password,
                UseVisualStyleBackColor = true
            };
            copyBtn.FlatAppearance.BorderSize = 0;
            copyBtn.Click += (object senderObject, EventArgs arg) =>
            {
                if (password != null && password.Password.Length > 0)
                {
                    Clipboard.SetText(password.Password);
                }
            };
            #endregion
            btnPanel.Controls.Add(copyBtn);
            btnPanel.Dock = DockStyle.Fill;
            btnPanel.Location = new Point(0, 223);
            btnPanel.Size = new Size(200, 33);
            #endregion
            #region Картинка
            PictureBox passImageBox = new PictureBox()
            {
                Dock = DockStyle.Top,
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = password.Image,
                Location = new Point(0, 0),
                Margin = new Padding(4, 5, 4, 5),
                Size = new Size(198, 176),
                TabStop = false
            };
            #endregion
            #region Кнопка удаления
            IconButton deleteBtn = new IconButton();
            deleteBtn.FlatAppearance.BorderSize = 0;
            deleteBtn.FlatStyle = FlatStyle.Flat;
            deleteBtn.IconChar = IconChar.Plus;
            deleteBtn.Rotation = 45;
            deleteBtn.IconSize = 20;
            deleteBtn.Size = new Size(25, 25);
            deleteBtn.BackColor = Color.Crimson;
            deleteBtn.Location = new Point(passImageBox.Width - 25, 0);
            deleteBtn.Click += (object senderObject, EventArgs arg) =>
            {
                if (MessageBox.Show("Вы действительно хотите удалить этот пароль?", "Удаление", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
                if (password == null)
                {
                    return;
                }
                RemovePassword(password);
                SqlConnector.DeleteFromBase(password);

            };
            #endregion
            #region Кнопка отправки
            IconButton sendBtn = new IconButton();
            sendBtn.FlatAppearance.BorderSize = 0;
            sendBtn.FlatStyle = FlatStyle.Flat;
            sendBtn.IconChar = IconChar.Share;
            sendBtn.IconSize = 20;
            sendBtn.Size = new Size(25, 25);
            sendBtn.BackColor = formColor;
            sendBtn.Location = new Point(0, 0);
            sendBtn.Click += (object senderObject, EventArgs arg) =>
            {
                Parent.SetNextForm(sendBtn, new SendTCPForm(password, Parent));
            };
            #endregion
            #region Добавление
            passPicPanel.Controls.Add(btnPanel);
            passPicPanel.Controls.Add(passLabel);
            passPicPanel.Controls.Add(passImageBox);
            passImageBox.Controls.Add(deleteBtn);
            passImageBox.Controls.Add(sendBtn);
            #endregion
            passPicPanel.Location = location;
            passPicPanel.Margin = new Padding(4, 5, 4, 5);
            passPicPanel.Size = new Size(200, 258);
            workPanel.Controls.Add(passPicPanel);
            #endregion
        }

        public void AddPassword(ImagePassword password)
        {
            if (passwords.Count(x=>x.Panel != null) == 0)
            {
                CreatePasswordPanel(new Point(margin, margin), password);
            }
            else
            {
                ImagePassword lastPassword = passwords.Where(x => x.Panel != null).Last();
                int freeSpace = workPanel.Width - margin * 3 - lastPassword.Panel.Location.X - lastPassword.Panel.Width;
                if (freeSpace >= lastPassword.Panel.Width)
                {
                    CreatePasswordPanel(new Point(lastPassword.Panel.Location.X + lastPassword.Panel.Width + margin, lastPassword.Panel.Location.Y), password);
                }
                else
                {
                    CreatePasswordPanel(new Point(margin, lastPassword.Panel.Location.Y + lastPassword.Panel.Height + margin), password);
                }
            }
            passwords.Add(password);
            logger.Trace($"Пароль-картинка(ID:{password.Id}) успешно добавлен!");
        }

        public void RemovePassword(ImagePassword password)
        {
            passwords.Remove(password);
            workPanel.Controls.Remove(password.Panel);
            password.Image.Dispose();
            RelocatePanels();
            logger.Trace($"Пароль-картинка(ID:{password.Id}) успешно убран!");
        }

        public void RelocatePanels()
        {
            logger.Trace("Запущено переопределение позиций плиток!");
            if (passwords.Count == 0)
            {
                return;
            }
            int amountFit = (workPanel.Width - margin) / (200 + margin);
            float lineAmount = (float)passwords.Count / amountFit;
            if (lineAmount % 1 > 0)
            {
                lineAmount++;
            }
            int intLineAmount = (int)lineAmount;
            for (int y = 0; y < intLineAmount; y++)
            {
                for (int x = 0; x < amountFit; x++)
                {
                    int index = amountFit * y + x;
                    if (index >= passwords.Count)
                    {
                        break;
                    }
                    passwords[amountFit * y + x].Panel.Location = new Point(margin + (200 + margin) * x, 2 * margin + (258 + margin) * y);
                }
            }
            logger.Trace("Переопределение позиций плиток завершено!");
        }

        public void OnWorkPanelResized(object sender, EventArgs e)
        {
            if (passwords == null || passwords.Count == 0)
            {
                return;
            }
            int amountFit = (workPanel.Width - margin) / (200 + margin);

            if (amountFit == 0)
            {
                return;
            }
            if (lastCountInLine == amountFit)
            {
                return;
            }
            float lineAmount = (float)passwords.Count / amountFit;
            if (lineAmount % 1 > 0)
            {
                lineAmount++;
            }
            int intLineAmount = (int)lineAmount;
            for (int y = 0; y < intLineAmount; y++)
            {
                for (int x = 0; x < amountFit; x++)
                {
                    int index = amountFit * y + x;
                    if (index >= passwords.Count)
                    {
                        break;
                    }
                    passwords[amountFit * y + x].Panel.Location = new Point(margin + (200 + margin) * x, 2 * margin + (258 + margin) * y);
                }
            }
            lastCountInLine = amountFit;
        }

        private void OnAddClick(object sender, EventArgs e)
        {
            logger.Trace("Нажата кнопка \"Добавить\"");
            Parent.SetNextForm(addBtn, new CreateImagePasswordForm(this));
        }

        private void PictureGenForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            passwords.ForEach(x =>
            {
                x.Panel?.Dispose();
                x.Panel = null;
            });
        }

        private void OnReceiveClick(object sender, EventArgs e)
        {
            Parent.SetNextForm(receiveBtn, new ReceiveTCPForm(this));
        }
    }
}
