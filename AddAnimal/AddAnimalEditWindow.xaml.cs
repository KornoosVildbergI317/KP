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

namespace ZooMail.AddAnimal
{
    /// <summary>
    /// Логика взаимодействия для AddAnimalEditWindow.xaml
    /// </summary>





    public partial class AddAnimalEditWindow : Window
    {


        private Models.ModelSupplier modelSupplier;

        List<Models.ModelSpecies> species;
        private AddAnimalEditWindow(Models.ModelSupplier modelSupplier)
        {
            InitializeComponent();
            this.modelSupplier = modelSupplier;

            species = (new DBManager()).getSpeciesList();
            ObservableCollection<string> listSpecies = new ObservableCollection<string>();
            foreach (var it in species)
            {
                listSpecies.Add(it.Title);
            }
            this.comboBoxID_Species.ItemsSource = listSpecies;

            if (this.modelSupplier != null)
            {
                int idlistSpecies = -1;
                for (idlistSpecies = 0; idlistSpecies < species.Count; idlistSpecies++)
                {
                    if (species[idlistSpecies].Id_species == this.modelSupplier.Id_Species)
                    {
                        break;
                    }
                }
                comboBoxID_Species.SelectedIndex = idlistSpecies;

                labelID_Model.Content = this.modelSupplier.Id_Supplier.ToString();
                textBoxReleaseDate.Text = this.modelSupplier.Release_date;
                textBoxTitle.Text = this.modelSupplier.Title;
            }
            else
            {
                labelID_Model.Content = string.Empty;
                textBoxReleaseDate.Text = string.Empty;
                textBoxTitle.Text = string.Empty;
            }
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxID_Species.SelectedIndex < 0 ||
                textBoxReleaseDate.Text == string.Empty ||
                textBoxTitle.Text == string.Empty)
            {
                MessageBox.Show("Error");
                return;
            }



            if (this.modelSupplier == null)
            {
                new DBManager().insertSupplier(new Models.ModelSupplier(
                -1,
                textBoxTitle.Text,
                textBoxReleaseDate.Text,
                species[comboBoxID_Species.SelectedIndex].Id_species
                ));
            }
            else
            {
                new DBManager().updateSupplier(new Models.ModelSupplier(
                this.modelSupplier.Id_Supplier,
                textBoxTitle.Text,
                textBoxReleaseDate.Text,
                species[comboBoxID_Species.SelectedIndex].Id_species
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
