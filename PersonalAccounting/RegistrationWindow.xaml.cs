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

namespace ZooMail.PersonalAccounting
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
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

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }


        List<Models.ModelCategory> categories;
        //List<Models.ModelCategory> models;
        List<Models.ModelStatusClient> statuses;

        bool isManager { get; set; }
        private ModelClient modelClient;

        private RegistrationWindow(ModelClient modelClient, bool isManager = false)

        {
            InitializeComponent();

            this.modelClient = modelClient;

            this.isManager = isManager;

            labelID_status_client.Visibility = isManager ? Visibility.Visible : Visibility.Hidden;
            comboBoxID_status_client.Visibility = isManager ? Visibility.Visible : Visibility.Hidden;

            categories = new DBManager().getCategoryList();
            ObservableCollection<string> listCategory = new ObservableCollection<string>();
            foreach (var category in categories)
            {
                listCategory.Add(category.Title);
            }
            this.comboBoxID_Category.ItemsSource = listCategory;

            statuses = new DBManager().getStatusClientList();
            ObservableCollection<string> listStatuses = new ObservableCollection<string>();
            foreach (var status in statuses)
            {
                listStatuses.Add(status.Title);
            }
            this.comboBoxID_status_client.ItemsSource = listStatuses;

            if (this.modelClient != null)
            {
                int idlistStatus = -1;
                for (idlistStatus = 0; idlistStatus < statuses.Count; idlistStatus++)
                {
                    if (statuses[idlistStatus].Id_status_client == this.modelClient.ID_status_client)
                    {
                        break;
                    }
                }
                comboBoxID_status_client.SelectedIndex = idlistStatus;

                int idlistCategory = -1;
                for (idlistCategory = 0; idlistCategory < categories.Count; idlistCategory++)
                {
                    if (categories[idlistCategory].Id_category == this.modelClient.ID_Category)
                    {
                        break;
                    }
                }
                comboBoxID_Category.SelectedIndex = idlistCategory;

                txtBoxEmail_address.Text = this.modelClient.Email_address;
                txtBoxLogin.Text = Login(modelClient.ID_Authorization);
                txtBoxMiddle_Name.Text = modelClient.Middle_Name;
                txtBoxName.Text = modelClient.Name;
                txtBoxPassport_number_and_series.Text = modelClient.Passport_number_and_series;
                txtBoxPassword.Visibility = Visibility.Hidden;
                txtBoxSurname.Text = modelClient.Surname;
            }
            else
            {
                txtBoxEmail_address.Text = string.Empty;
                txtBoxLogin.Text = string.Empty;
                txtBoxMiddle_Name.Text = string.Empty;
                txtBoxName.Text = string.Empty;
                txtBoxPassport_number_and_series.Text = string.Empty;
                txtBoxSurname.Text = string.Empty;
            }

            if (isManager)
            {
                txtBoxEmail_address.IsReadOnly = true;
                txtBoxLogin.IsReadOnly = true;
                txtBoxMiddle_Name.IsReadOnly = true;
                txtBoxName.IsReadOnly = true;
                txtBoxPassport_number_and_series.IsReadOnly = true;
                labelPass.Visibility = labelPass2.Visibility = Visibility.Hidden;
                txtBoxPassword.Visibility = Visibility.Hidden;
                txtBoxPassword2.Visibility = Visibility.Hidden;
                txtBoxSurname.IsReadOnly = true;
                comboBoxID_Category.IsReadOnly = true;
                comboBoxID_status_client.IsReadOnly = true;
                buttonRegistration.Content = "Close";
            }
        }

        private async void Button_Click_Registration(object sender, RoutedEventArgs e)
        {

            if (isManager)
            {
                Close();
                return;
            }

            if (!checkBoxAgree.IsChecked.Value)
            {
                MessageBox.Show("Необходимо согласиться с условиями");
                return;
            }

            if (comboBoxID_Category.SelectedIndex < 0)
            {
                MessageBox.Show("Не выбрана категория");
                return;
            }

            if (isManager && comboBoxID_status_client.SelectedIndex < 0)
            {
                MessageBox.Show("Не выбран статус клиента");
                return;
            }

            bool znach2 = await Symb(txtBoxSurname.Text);

            if (txtBoxSurname.Text == String.Empty || znach2)
            {
                MessageBox.Show("Есть ошибка в указании Фамилии");
                return;
            }

            bool znach1 = await Symb(txtBoxName.Text);

            if (txtBoxName.Text == String.Empty || znach1)
            {
                MessageBox.Show("Есть ошибка в указании Имени");
                return;
            }

            bool znach3 = await Symb(txtBoxMiddle_Name.Text);

            if (txtBoxMiddle_Name.Text == String.Empty || znach3)
            {
                MessageBox.Show("Есть ошибка в указании Отчества");
                return;
            }

            if (!IsValidEmail(txtBoxEmail_address.Text))
            {
                MessageBox.Show("Не верно указан Email");
                return;
            }


            if (txtBoxPassport_number_and_series.Text == String.Empty)
            {
                MessageBox.Show("Не указаны номер и серия паспорта");
                return;
            }

           


            if (txtBoxLogin.Text == String.Empty)
            {
                MessageBox.Show("Не указан логин");
                return;
            }


            if (txtBoxPassword.Text != txtBoxPassword2.Text)
            {
                MessageBox.Show("Введенные пароли не совпадают");
                return;
            }

            int ID_Category = categories[comboBoxID_Category.SelectedIndex].Id_category;



            if (idAuth(txtBoxLogin.Text) >= 0)
            {
                MessageBox.Show("Такой логин уже есть в базе");
                return;
            }



            Models.ModelAuthorization newAuthorization = new Models.ModelAuthorization(-1, txtBoxLogin.Text, txtBoxPassword.Text, 1);
            new DBManager().insertAuthorization(newAuthorization);

            int id_Auth = idAuth(txtBoxLogin.Text);

            Models.ModelClient client = new Models.ModelClient(
                id_Auth,
                txtBoxSurname.Text,
                txtBoxName.Text,
                txtBoxMiddle_Name.Text,
                txtBoxEmail_address.Text,
                2,
                txtBoxPassport_number_and_series.Text,
                ID_Category
                );


            new DBManager().insertClient(client);

            MessageBox.Show("Регистрация прошла успешно");
            Close();

        }

        int idAuth(string Login)
        {
            var dbmanager = new DBManager();
            var auths = dbmanager.getAuthorizationList();
            foreach (var a in auths)
            {
                if (a.Login == txtBoxLogin.Text)
                {
                    return a.ID_Authorization;
                }
            }
            return -1;
        }

        string Login(int ID_Authorization)
        {
            var dbmanager = new DBManager();
            var auths = dbmanager.getAuthorizationList();
            foreach (var a in auths)
            {
                if (a.ID_Authorization == ID_Authorization)
                {
                    return a.Login;
                }
            }
            return string.Empty;
        }

        private void ButtonConditon_Click(object sender, RoutedEventArgs e)
        {
            (new UseСonditionsWindow()).ShowDialog();
        }
    }
}
