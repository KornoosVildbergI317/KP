using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooMail
{
    class Configuration_class
    {
        public static string Empty = "Empty";

        public string DS = Empty,// Переменная Data Source
            IC = Empty;//Переменная Initial Catalog
        public SqlConnection connection = new SqlConnection();

        public bool isConnection
        {
            get
            {
                return DS != Empty && IC != Empty;
            }
        }
        public void SQL_Server_Configuration_Get()
        {
            //Создаёт каталог в одном из корней реестра ОС
            RegistryKey registry = Registry.CurrentUser;
            //Создаёт папку в выбраном коревом каталоге рееста ОС
            RegistryKey key = registry.CreateSubKey("Server_Configuration");
            try
            {
                //Пытаюсь получить значения из переменных в реестре
                DS = key.GetValue("DS").ToString();
                IC = key.GetValue("IC").ToString();
            }
            catch
            {
                DS = Empty;
                IC = Empty;
            }
            finally
            {
                //Обновление строки подкючения
                connection.ConnectionString = "Data Source = " + DS +
                    "; Initial Catalog = " + IC +
                    "; Integrated Security = true;";
            }
        }


    }
}