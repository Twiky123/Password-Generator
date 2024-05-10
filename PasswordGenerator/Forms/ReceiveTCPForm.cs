using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace PasswordGenerator.Forms
{
    public partial class ReceiveTCPForm : Form
    {
        private int port; //Порт текущего подключения
        private bool methodLinked; //Флаг, обозначающий, что метод DoWOrk подключен
        private TcpListener server; //Активный слушатель TCP
        private ImagePassword receivedImage; //Полчуенное Пароль-Изображение
        private LoginPassword receivedLogin; //Полученный Логин-Пароль
        private PictureGenForm imageParent; //Пошлая форма в случае, если 
        private SavedPasswordsForm loginParent; //Пошлая форма в случае, если 
        private BackgroundWorker worker; // Фоновый процесс для ожидания получения новых пакетов
        private Logger logger; //Логгер текущего класса
        public ReceiveTCPForm(PictureGenForm parent)
        {
            logger = LogManager.GetCurrentClassLogger();
            methodLinked = false;
            imageParent = parent;
            worker = new BackgroundWorker();
            InitializeComponent();
        }

        public ReceiveTCPForm(SavedPasswordsForm parent)
        {
            logger = LogManager.GetCurrentClassLogger();
            methodLinked = false;
            loginParent = parent;
            worker = new BackgroundWorker();
            InitializeComponent();
            ReceiveBtn.BackColor = Color.FromArgb(50, 183, 108);
        }

        private void SwapPanels(Panel panel1, Panel panel2)
        {
            panel1.Visible = false;
            Point location = panel1.Location;
            panel1.Location = panel2.Location;
            panel2.Location = location;
            panel2.Visible = true;
        }

        private void ReceiveBtn_Click(object sender, EventArgs e)
        {
            logger.Trace("Запрошено начало ожидания получения");
            if (worker.IsBusy)
            {
                logger.Warn("Worker ещё не завершил исполняющий метод!");
                MessageBox.Show("Немного подождите. Завершаем предыдущую операцию.", "Ошибка");
                return;
            }

            if (!portTextBox.MaskCompleted)
            {
                logger.Warn("Неверно введён порт");
                MessageBox.Show("Неверно введён порт", "Ошибка");
                return;
            }

            if (!int.TryParse(portTextBox.Text, out port))
            {
                logger.Warn("Неверно введён порт");
                MessageBox.Show("Неверно введён порт", "Ошибка");
                return;
            }

            logger.Trace("Получение текущего IP");
            string ipLocal = GetLocalIPAddress();
            if (ipLocal.Equals("ERROR"))
            {
                logger.Warn("Нет сетевых адаптеров с адресом IPv4 в системе");
                MessageBox.Show("Нет сетевых адаптеров с адресом IPv4 в системе", "Ошибка");
                return;
            }
            logger.Trace($"Успешно: {ipLocal}");
            ipLabel.Text = $"IP: {ipLocal}\nПорт: {port}";

            if (!methodLinked)
            {
                worker.DoWork += (object doWorkObject, DoWorkEventArgs arg) =>
                {
                    server = new TcpListener(IPAddress.Any, port);
                    server.Start();
                    logger.Trace("Сервер TCP стартовал");
                    TcpClient client = null;
                    try
                    {
                        client = server.AcceptTcpClient();
                        logger.Trace("Клиент подтверждён");
                    }
                    catch
                    {
                        logger.Error("Ошибка подключения клиента, меняю панели обратно");
                        Action act = () =>
                        {
                            SwapPanels(waitPanel, askPortPanel);
                        };
                        this?.Invoke(act);
                        return;
                    }
                    NetworkStream ns = client.GetStream();
                    if (client.Connected)
                    {
                        logger.Trace("Клиент подключился");
                        byte[] msg = new byte[10485760];     // готовим место для принятия сообщения (10МБ)
                        string message = Encoding.UTF8.GetString(msg, 0, ns.Read(msg, 0, msg.Length));
                        try
                        {
                            logger.Trace("Проиводится попытка получения картинки-пароля");
                            receivedImage = ImagePassword.FromSendInfo(JsonConvert.DeserializeObject<SendPasswordImageInfo>(message));
                            logger.Info("Получен пароль-картинка");
                        }
                        catch
                        {
                            try
                            {
                                logger.Trace("Проиводится попытка получения логин-пароля");
                                receivedLogin = LoginPassword.FromSendInfo(JsonConvert.DeserializeObject<SendPasswordLoginInfo>(message));
                                logger.Info("Получен логин-пароль");
                            }
                            catch
                            {
                                logger.Error("Не удалось распознать полученный пакет");
                                MessageBox.Show("Не удалось распознать полученный пакет!", "Ошибка");
                            }
                        }
                        client.Close();
                        server.Stop();

                        Action act = () =>
                        {
                            if (receivedImage == null && receivedLogin == null)
                            {
                                SwapPanels(waitPanel, askPortPanel);
                            }
                            else if (receivedLogin != null)
                            {
                                SwapPanels(waitPanel, passwordReceivedPanel);
                                loginPassLabel.Text = $"Получен пароль с логином '{receivedLogin.Login}'!";
                            }
                            else
                            {
                                SwapPanels(waitPanel, foundPanel);
                                foundBox.Image = receivedImage.Image;
                            }
                        };
                        this?.Invoke(act);
                    }
                    server = null;
                    client = null;
                };
                methodLinked = true;
            }
            worker.RunWorkerAsync();
            SwapPanels(askPortPanel, waitPanel);
        }

        private string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return "ERROR";
        }

        private void cancelWaitBtn_Click(object sender, EventArgs e)
        {
            if (server != null)
            {
                server?.Stop();
                server = null;
            }
            else
            {
                SwapPanels(waitPanel, askPortPanel);
            }
        }

        private void cancelSaveBtn_Click(object sender, EventArgs e)
        {
            receivedImage = null;
            foundBox.Image = null;
            SwapPanels(foundPanel, askPortPanel);
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            receivedImage.Id = SqlConnector.AddToBase(receivedImage);
            if (loginParent != null)
            {
                loginParent.Parent.backBtn.PerformClick();
            }
            else
            {
                imageParent.AddPassword(receivedImage);
                imageParent.Parent.backBtn.PerformClick();
            }
        }

        private void cancelLoginPassBtn_Click(object sender, EventArgs e)
        {
            receivedLogin = null;
            SwapPanels(passwordReceivedPanel, askPortPanel);
        }

        private void saveLoginPassBtn_Click(object sender, EventArgs e)
        {
            List<LoginPassword> foundPassword = SqlConnector.FindPasswordWithLogin(receivedLogin.Login);
            if (foundPassword.Any(x => x.Decrypt().Equals(receivedLogin.Decrypt())))
            {
                logger.Warn($"Сохранение пароля отклонено. Такой пароль уже сохранён при логине {receivedLogin.Login}");
                MessageBox.Show("Такой пароль уже сохранён при этом логине!", "Ошибка");
                return;
            }
            receivedLogin.Id = SqlConnector.AddToBase(receivedLogin);
            if (loginParent == null)
            {
                imageParent.Parent.backBtn.PerformClick();
            }
            else
            {
                loginParent.OnSearchClick(null, null);
                loginParent.Parent.backBtn.PerformClick();
            }
        }

        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            server?.Stop();
        }
    }
}
