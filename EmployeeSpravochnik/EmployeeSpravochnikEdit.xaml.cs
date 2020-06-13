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

namespace ZooMail.EmployeeSpravochnik
{
    /// <summary>
    /// Логика взаимодействия для EmployeeSpravochnikEdit.xaml
    /// </summary>
    /// 
    public partial class EmployeeSpravochnikEdit : Window
    {
        private Models.ModelEmployee modelEmployee;
        private List<Models.ModelRole> roles;
        private EmployeeSpravochnikEdit(Models.ModelEmployee modelEmployee)
        {
            InitializeComponent();

            roles = new DBManager().getRoleList();
            ObservableCollection<string> listPositions = new ObservableCollection<string>();
            foreach (var it in roles)
            {
                listPositions.Add(it.Title_Role);
            }
            this.comboBoxID_Position.ItemsSource = listPositions;

            this.modelEmployee = modelEmployee;

            if (this.modelEmployee != null)
            {
                labelId.Content = this.modelEmployee.ID_Authorization.ToString();
                textboxEmployeeSurname.Text = this.modelEmployee.Surname;
                textboxEmployeeName.Text = this.modelEmployee.Name;
                textboxEmployeeMiddle_Name.Text = this.modelEmployee.Middle_Name;

                comboBoxID_Position.IsEnabled = false;
                txtBoxLogin.Visibility = Visibility.Hidden;
                txtBoxPassword.Visibility = Visibility.Hidden;
                txtBoxPassword2.Visibility = Visibility.Hidden;
                labelPass.Visibility = Visibility.Hidden;
                labelPass2.Visibility = Visibility.Hidden;
                labelLogin.Visibility = Visibility.Hidden;

                int idlistRoles = -1;
                for (idlistRoles = 0; idlistRoles < roles.Count; idlistRoles++)
                {
                    Models.ModelAuthorization authorization = null;
                    foreach (var it in new DBManager().getAuthorizationList())
                    {
                        if (it.ID_Authorization == this.modelEmployee.ID_Authorization)
                        {
                            authorization = it;
                            break;
                        }
                    }
                    if (authorization != null)
                    {
                        if (roles[idlistRoles].Id_Role == authorization.ID_Role)
                        {
                            break;
                        }
                    }

                }
                comboBoxID_Position.SelectedIndex = idlistRoles;
            }
            else
            {
                labelId.Content = string.Empty;
                textboxEmployeeSurname.Text = string.Empty;
                textboxEmployeeName.Text = string.Empty;
                textboxEmployeeMiddle_Name.Text = string.Empty;
            }

        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if (textboxEmployeeMiddle_Name.Text == string.Empty)
            {
                MessageBox.Show("Error");
                return;
            }
            if (textboxEmployeeName.Text == string.Empty)
            {
                MessageBox.Show("Error");
                return;
            }
            if (textboxEmployeeSurname.Text == string.Empty)
            {
                MessageBox.Show("Error");
                return;
            }


            if (this.modelEmployee == null)
            {
                if (txtBoxLogin.Text == string.Empty)
                {
                    MessageBox.Show("Error");
                    return;
                }
                if (txtBoxPassword.Text == string.Empty)
                {
                    MessageBox.Show("Error");
                    return;
                }
                if (txtBoxPassword2.Text == string.Empty)
                {
                    MessageBox.Show("Error");
                    return;
                }
            }


            if (this.modelEmployee == null)
            {

                if (idAuth(txtBoxLogin.Text) >= 0)
                {
                    MessageBox.Show("Такой логин уже есть в базе");
                    return;
                }



                Models.ModelAuthorization newAuthorization = new Models.ModelAuthorization(
                    -1,
                    txtBoxLogin.Text,
                    txtBoxPassword.Text,
                    roles[comboBoxID_Position.SelectedIndex].Id_Role);
                new DBManager().insertAuthorization(newAuthorization);

                int id_Auth = idAuth(txtBoxLogin.Text);

                Models.ModelEmployee employee = new Models.ModelEmployee(
                    id_Auth,
                    textboxEmployeeSurname.Text,
                    textboxEmployeeName.Text,
                    textboxEmployeeMiddle_Name.Text
                    );


                new DBManager().insertEmployee(employee);

                MessageBox.Show("Регистрация прошла успешно");

            }
            else
            {
                new DBManager().updateEmployee(new Models.ModelEmployee(
                this.modelEmployee.ID_Authorization,
                 textboxEmployeeSurname.Text,
                textboxEmployeeName.Text,
                textboxEmployeeMiddle_Name.Text
                ));
            }



            MessageBox.Show("Операция выполнена");
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

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
