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

namespace ZooMail.Buy_animal
{
    /// <summary>
    /// Логика взаимодействия для Supplier.xaml
    /// </summary>
    public partial class Buy_animalWindow : Window
    {
        public Buy_animalWindow()
        {
            InitializeComponent();
        }
        List<Models.ModelAnimalparkDetail> listAnimal;
        private void Window_Activated(object sender, EventArgs e)
        {
            updateData();
        }

        public void updateData()
        {
            listAnimal = (new DBManager()).getAnimalparkListDetail();
            dataGridAnimal.ItemsSource = listAnimal;
            dataGridAnimal.Columns[0].Visibility = Visibility.Hidden;
            dataGridAnimal.Columns[1].Visibility = Visibility.Hidden;
            dataGridAnimal.Columns[2].Visibility = Visibility.Hidden;

            dataGridAnimal.Columns[3].Header = "Парк";
            dataGridAnimal.Columns[4].Visibility = Visibility.Hidden;

            
        }

        private void ButtonReserv_Click(object sender, RoutedEventArgs e)
        {
            var index = dataGridAnimal.SelectedIndex;
            if (index < 0)
            {
                MessageBox.Show("Необходимо выбрать животное");
                return;

            }

            if (listAnimal[index].StatusAnimalTitle != "Свободен")
            {
                MessageBox.Show("недоступно");
                return;
            }


            //Session.currentUser.ID_Authorization



            var startDate = dataPickerStart.SelectedDate;
            var endDate = dataPickerEnd.SelectedDate;

            if (startDate == null || endDate == null)
            {
                MessageBox.Show("Не выбрана дата покупки");
                return;
            }

            DateTime start = startDate.Value;
            DateTime end = endDate.Value;

            if (start >= end)
            {
                MessageBox.Show("Некорректные данные");
                return;
            }

            new DBManager().insertBuy_animal(new Models.ModelBuy_animal(
                -1,
                start.ToString(),
                end.ToString(),
                listAnimal[index].ID_animalpark,
                Session.currentUser.ID_Authorization)
                );

            var listAnimalpark = new DBManager().getAnimalparkList();
            Models.ModelAnimalpark model = null;
            foreach (var it in listAnimalpark)
            {
                if (it.ID_animalpark == listAnimal[index].ID_animalpark)
                {
                    model = it;
                    break;
                }
            }

            model.ID_Status_Animal = 1; //Занят
            new DBManager().updateAnimalpark(model);

            MessageBox.Show("Недоступно");
            updateData();
        }

        private void DataGridAnimal_Selected(object sender, RoutedEventArgs e)
        {
            var curit = dataGridAnimal.CurrentItem;
            var currentRowIndex = dataGridAnimal.Items.IndexOf(curit);

            txtCategory.Text = listAnimal[currentRowIndex].CategoryTitle;
            txtNumber.Text = listAnimal[currentRowIndex].Number;
            txtStatus.Text = listAnimal[currentRowIndex].StatusAnimalTitle;
            txtTitle.Text = listAnimal[currentRowIndex].SpeciesSupplierTitle;


        }
    }
}
