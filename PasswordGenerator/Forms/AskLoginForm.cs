using System;
using System.Windows.Forms;

namespace PasswordGenerator.Forms
{
    public partial class AskLoginForm : Form
    {
        public string Result { get; set; }
        public AskLoginForm()
        {
            InitializeComponent();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Result = null;
            Close();
        }

        private void acceptBtn_Click(object sender, EventArgs e)
        {
            if (loginBox.Text.Length == 0)
            {
                MessageBox.Show("Для сохранения заполните поле логина!","Ошибка");
                return;
            }
            
            Result = loginBox.Text;
           
            Close();
        }

        
    }
}
