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

namespace ZooMail.Species
{
    /// <summary>
    /// Логика взаимодействия для SpeciesEdit.xaml
    /// </summary>
    /// 

    public partial class SpeciesEdit : Window
    {

        async Task<bool> Symb(string str)
        {
            bool znach = false;
            await Task.Run(() =>
            {
                if (
                str.Contains("?") || str.Contains("!") || str.Contains("@") ||
                str.Contains("#") || str.Contains("№") || str.Contains("~") ||
                str.Contains(";") || str.Contains("%") || str.Contains("$") ||
                str.Contains("^") || str.Contains("&") || str.Contains(":") ||
                str.Contains("*") || str.Contains("(") || str.Contains(")") ||
                str.Contains("_") || str.Contains("=") || str.Contains("+") ||
                str.Contains("/") || str.Contains("|") || str.Contains("[") ||
                str.Contains("]") || str.Contains("{") || str.Contains("}") ||
                str.Contains("<") || str.Contains(">") || str.Contains("-") ||
                str.Contains(",") || str.Contains("`") || str.Contains("."))
                    znach = true;
            });
            return znach;
        }


        private Models.ModelSpecies modelSpecies;
        private SpeciesEdit(Models.ModelSpecies modelSpecies)
        {
            InitializeComponent();
            this.modelSpecies = modelSpecies;

            if (this.modelSpecies != null)
            {
                labelId.Content = this.modelSpecies.Id_species.ToString();
                textboxSpeciesTitle.Text = this.modelSpecies.Title;
            }
            else
            {
                labelId.Content = string.Empty;
                textboxSpeciesTitle.Text = string.Empty;
            }

        }


        private async void ButtonOk_ClickAsync(object sender, RoutedEventArgs e)
        {
            bool znach = await Symb(textboxSpeciesTitle.Text);
            if (textboxSpeciesTitle.Text == string.Empty || znach)
            {
                MessageBox.Show("Ошибка");
                return;
            }

            if (this.modelSpecies == null)
            {
                new DBManager().insertSpecies(new Models.ModelSpecies(
                -1,
                textboxSpeciesTitle.Text
                ));
            }
            else
            {
                new DBManager().updateSpecies(new Models.ModelSpecies(
                this.modelSpecies.Id_species,
                textboxSpeciesTitle.Text
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
