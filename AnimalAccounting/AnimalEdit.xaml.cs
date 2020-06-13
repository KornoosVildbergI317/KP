using ZooMail.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ZooMail.AnimalAccounting
{
    /// <summary>
    /// Логика взаимодействия для AnimalEdit.xaml
    /// </summary>
    public partial class AnimalEdit : Window
    {

        private Models.ModelAnimalpark modelAnimalpark;

        List<Models.ModelCategory> categories;
        List<Models.ModelSupplierDetail> supplierDetails;
        List<Models.ModelStatusAnimal> statusAnimal;

        private AnimalEdit(Models.ModelAnimalpark modelAnimalpark)
        {

            InitializeComponent();

            this.modelAnimalpark = modelAnimalpark;

            categories = (new DBManager()).getCategoryList();
            ObservableCollection<string> listCategory = new ObservableCollection<string>();
            foreach (var it in categories)
            {
                listCategory.Add(it.Title);
            }
            this.comboBoxID_Category.ItemsSource = listCategory;

            supplierDetails = new DBManager().getSupplierListDetail();
            ObservableCollection<string> listSupplierDetail = new ObservableCollection<string>();
            foreach (var it in supplierDetails)
            {
                listSupplierDetail.Add(it.SpeciesSupplierTitle);
            }
            this.comboboxID_Model.ItemsSource = listSupplierDetail;

            statusAnimal = new DBManager().getStatusAnimalList();
            ObservableCollection<string> listStatusAnimal = new ObservableCollection<string>();
            foreach (var it in statusAnimal)
            {
                listStatusAnimal.Add(it.Title);
            }
            this.comboboxStatus.ItemsSource = listStatusAnimal;


            if (this.modelAnimalpark != null)
            {
                int idlistStatusAnimal = -1;
                for (idlistStatusAnimal = 0; idlistStatusAnimal < statusAnimal.Count; idlistStatusAnimal++)
                {
                    if (statusAnimal[idlistStatusAnimal].Id_Status_Animal == this.modelAnimalpark.ID_Status_Animal)
                    {
                        break;
                    }
                }
                comboboxStatus.SelectedIndex = idlistStatusAnimal;

                int idlistSupplier = -1;
                for (idlistSupplier = 0; idlistSupplier < supplierDetails.Count; idlistSupplier++)
                {
                    if (supplierDetails[idlistSupplier].Id_Supplier == this.modelAnimalpark.ID_Supplier)
                    {
                        break;
                    }
                }
                comboboxID_Model.SelectedIndex = idlistSupplier;

                int idlistCategories = -1;
                for (idlistCategories = 0; idlistCategories < categories.Count; idlistCategories++)
                {
                    if (categories[idlistCategories].Id_category == this.modelAnimalpark.ID_Category)
                    {
                        break;
                    }
                }
                comboBoxID_Category.SelectedIndex = idlistCategories;



                labelId.Content = this.modelAnimalpark.ID_animalpark.ToString();
                textboxNumber.Text = this.modelAnimalpark.Number;
            }
            else
            {
                labelId.Content = string.Empty;
                textboxNumber.Text = string.Empty;
            }

        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if (comboboxStatus.SelectedIndex < 0 || comboboxID_Model.SelectedIndex < 0 || comboBoxID_Category.SelectedIndex < 0)
            {
                MessageBox.Show("Error");
                return;
            }

            if (this.modelAnimalpark == null)
            {
                new DBManager().insertAnimalpark(new Models.ModelAnimalpark(
                -1,
                textboxNumber.Text,
                categories[comboBoxID_Category.SelectedIndex].Id_category,
                supplierDetails[comboboxID_Model.SelectedIndex].Id_Supplier,
                statusAnimal[comboboxStatus.SelectedIndex].Id_Status_Animal
                ));
            }
            else
            {
                new DBManager().updateAnimalpark(new Models.ModelAnimalpark(
                this.modelAnimalpark.ID_animalpark,
                textboxNumber.Text,
                categories[comboBoxID_Category.SelectedIndex].Id_category,
                supplierDetails[comboboxID_Model.SelectedIndex].Id_Supplier,
                statusAnimal[comboboxStatus.SelectedIndex].Id_Status_Animal
                ));
            }



            MessageBox.Show("Операция выполнена");
            Close();

        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
