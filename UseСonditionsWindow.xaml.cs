using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ZooMail
{
    /// <summary>
    /// Логика взаимодействия для LaunchScreenWindow.xaml
    /// </summary>
    public partial class UseСonditionsWindow : Window
    {
        public UseСonditionsWindow()
        {
            InitializeComponent();

        }







        //private void Button_Click(object sender, RoutedEventArgs e)
        //{


        //    if (!(new DBManager().IsConnection))
        //    {
        //        MessageBox.Show("Ошибка подкл к БД");
        //    }
        //    else
        //    {
        //        new MainWindow().Show();
        //    }

        //    Close();
        //}

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (!(new DBManager().IsConnection))
            {
                MessageBox.Show("Ошибка подкл к БД");
            }
            else
            {
                new MainWindow().Show();
            }

            Close();
        }
    }
}
