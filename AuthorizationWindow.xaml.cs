using ZooMail.Models;
using System;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZooMail.PersonalAccounting;

namespace ZooMail
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private void Button_Test_Click(object sender, RoutedEventArgs e)
        {
            Session.currentUser = null;

            var users = new DBManager().getAuthorizationList();

            foreach (var user in users)
            {
                if (user.Login == txtBoxLog.Text && user.Password == txtBoxPas.Password)
                {
                    Session.currentUser = user;
                    PersonalAccountWindow clientPersonal = new PersonalAccountWindow();
                    clientPersonal.Show();
                    if (Session.mainWindow != null)
                    {
                        Session.mainWindow.Close();
                    }
                    Close();

                    return;
                }
            }

            MessageBox.Show("Ошибка в логине или пароле!");
        }

        private void Button_Registarion_Click(object sender, RoutedEventArgs e)
        {
            new RegistrationWindow(null).ShowDialog();
        }

    }
}
