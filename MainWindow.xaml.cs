using System;
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

namespace ZooMail
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Session.mainWindow = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (new AuthorizationWindow()).ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            (new UseСonditionsWindow()).ShowDialog();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            (new HelpWindow()).ShowDialog();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            new Species.Species().ShowDialog();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            new EmployeeSpravochnik.EmployeeSpravochnik().ShowDialog();
        }

        

        
    }
}
