using ZooMail.Models;
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

namespace ZooMail.AddAnimal
{
    /// <summary>
    /// Логика взаимодействия для AddAnimalsWindow.xaml
    /// </summary>
    public partial class AddAnimalWindow : Window
    {
        public AddAnimalWindow()
        {
            InitializeComponent();

        }

        List<Models.ModelSupplierDetail> listSupplier;

        private void updateData()
        {
            listSupplier = (new DBManager()).getSupplierListDetail();
            dataGridAddAnimals.ItemsSource = listSupplier;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            updateData();
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = dataGridAddAnimals.SelectedIndex;
            if (selectedIndex < 0)
            {
                MessageBox.Show("Не выбрано животное для редактирования");
                return;
            }



            var ap = new DBManager().getSupplierList();

            Models.ModelSupplier modelSupplier = null;
            foreach (var it in ap)
            {
                if (it.Id_Supplier == listSupplier[selectedIndex].Id_Supplier)
                {
                    modelSupplier = it;
                    break;
                }
            }

            var ce = new AddAnimalEditWindow(modelSupplier);
            ce.ShowDialog();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            var ce = new AddAnimalEditWindow(null);
            ce.ShowDialog();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = dataGridAddAnimals.SelectedIndex;
            if (selectedIndex < 0)
            {
                MessageBox.Show("Не выбрано животное для редактирования");
                return;
            }


            if (MessageBox.Show("Вы уверены?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                return;
            }


            new DBManager().deleteSupplier(listSupplier[selectedIndex].Id_Supplier);
            MessageBox.Show("Операция выполнена");
            updateData();
        }
    }
}
