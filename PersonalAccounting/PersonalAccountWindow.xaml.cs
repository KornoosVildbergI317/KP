//using ZooMail.ClientList;
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
using ZooMail.Buy_animal;

namespace ZooMail.PersonalAccounting
{
    /// <summary>
    /// Логика взаимодействия для PersonalAccountWindow.xaml
    /// </summary>
    public partial class PersonalAccountWindow : Window
    {




        private List<Button> buttons = new List<Button>();
        public PersonalAccountWindow()
        {
            InitializeComponent();

            var roleList = new DBManager().getRoleList(Session.currentUser.ID_Role);

            //buttonsRole = new ObservableCollection<ButtonRole>();
            if (roleList.Count == 1)
            {

                buttons.Clear();



                var role = roleList[0];

                this.Title = role.Title_Role;



                if (role.Accounting_animals == 1)
                {
                    buttons.Add(createButton("Учет животных", Button_Hanle_AnimalAccountingWindow));
                    //buttonsRole.Add(new ButtonRole() { Caption = "Учет животных", HandleName = "Button_Hanle" });
                }
                if (role.Add_animal == 1)
                {
                    buttons.Add(createButton("Добавление животных", Button_Hanle_AddingAnimalWindow));
                    //buttonsRole.Add(new ButtonRole() { Caption = "Добавление животных", HandleName = "Button_Hanle" });
                }
                if (role.Lock_client == 1)
                {
                    buttons.Add(createButton("Блокировка клиента", Button_Hanle_ClientLockWindow));
                   // buttons.Add(createButton("Штрафы", Button_Hanle_Fines));
                    //buttonsRole.Add(new ButtonRole() { Caption = "Блокировка клиента", HandleName = "Button_Hanle" });
                }
                if (role.Buy_animal == 1)
                {
                    buttons.Add(createButton("Покупка животного", Button_Hanle_Buy_animalWindow));
                    //buttonsRole.Add(new ButtonRole() { Caption = "покупка", HandleName = "Button_Hanle" });
                }
                if (role.Add_client == 1)
                {
                    buttons.Add(createButton("Список клиентов", Button_Hanle_AddCustomerWindow));
                    //buttonsRole.Add(new ButtonRole() { Caption = "Добавление клиента", HandleName = "Button_Hanle" });
                }


                for (int i = 0; i < buttons.Count; i++)
                {

                    sp.Children.Add(buttons[i]);
                }

            }
            //ButtonsListBox.ItemsSource = buttonsRole;
        }

        private Button createButton(string content, RoutedEventHandler handler)
        {
            var b = new Button();
            b.Content = content;
            b.Click += handler;
            return b;
        }

        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            Session.currentUser = null;
            (new MainWindow()).Show();
            Close();
        }

        private void Button_Hanle_AnimalAccountingWindow(object sender, RoutedEventArgs e)
        {
            var window = new AnimalAccounting.AnimalAccountingWindow();
            window.ShowDialog();
        }

        private void Button_Hanle_AddingAnimalWindow(object sender, RoutedEventArgs e)
        {
            var window = new AddAnimal.AddAnimalWindow();
            window.ShowDialog();
        }

        private void Button_Hanle_ClientLockWindow(object sender, RoutedEventArgs e)
        {
            var window = new BlockClient.BlockClientWindow();
            window.ShowDialog();
        }

        private void Button_Hanle_Buy_animalWindow(object sender, RoutedEventArgs e)
        {
            var window = new Buy_animalWindow();
            window.ShowDialog();
        }

        private void Button_Hanle_AddCustomerWindow(object sender, RoutedEventArgs e)
        {
            ClientListWindow w = new ClientListWindow();
            w.ShowDialog();
        }

        //private void Button_Hanle_Fines(object sender, RoutedEventArgs e)
        //{
        //    Fines.FinesWindow fines = new Fines.FinesWindow();
        //    fines.ShowDialog();
        //}
    }


    //public class ButtonRole
    //{
    //    public string Caption { get; set; }
    //    public string HandleName { get; set; }
    //}
}
