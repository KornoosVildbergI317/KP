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
using System.Windows.Shapes;

namespace ZooMail.BlockClient
{
    /// <summary>
    /// Логика взаимодействия для BlockClientWindow.xaml
    /// </summary>
    public partial class BlockClientWindow : Window
    {
        public BlockClientWindow()
        {
            InitializeComponent();
        }

        List<Models.ModelClientDetail> listClientDetail;


        private void updateData()
        {
            listClientDetail = (new DBManager()).getClientListDetail();

            dataGridClients.ItemsSource = listClientDetail;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            updateData();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonBlock_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = dataGridClients.SelectedIndex;
            if (selectedIndex < 0)
            {
                MessageBox.Show("Не выбран клиент");
                return;
            }

            var ap = new DBManager().getClientList();

            Models.ModelClient modelClient = null;
            foreach (var it in ap)
            {
                if (it.ID_Authorization == listClientDetail[selectedIndex].ID_Authorization)
                {
                    modelClient = it;
                    break;
                }
            }

            modelClient.ID_status_client = 3;
            new DBManager().updateClient(modelClient);

            MessageBox.Show("Операция выполнена");
            updateData();
        }

        private void ButtonUnBlock_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = dataGridClients.SelectedIndex;
            if (selectedIndex < 0)
            {
                MessageBox.Show("Не выбран клиент");
                return;
            }

            var ap = new DBManager().getClientList();

            Models.ModelClient modelClient = null;
            foreach (var it in ap)
            {
                if (it.ID_Authorization == listClientDetail[selectedIndex].ID_Authorization)
                {
                    modelClient = it;
                    break;
                }
            }

            modelClient.ID_status_client = 2;
            new DBManager().updateClient(modelClient);

            MessageBox.Show("Операция выполнена");
            updateData();
        }
    }
}
