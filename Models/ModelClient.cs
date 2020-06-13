using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooMail.Models
{
    class ModelClient
    {
        public int ID_Authorization { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Middle_Name { get; set; }
        public string Email_address { get; set; }
        public int ID_status_client { get; set; }
        public string Passport_number_and_series { get; set; }
        public int ID_Category { get; set; }

        public ModelClient(int iD_Authorization, string surname, string name, string middle_Name, string email_address, int iD_status_client, string passport_number_and_series, int iD_Category)
        {
            ID_Authorization = iD_Authorization;
            Surname = surname;
            Name = name;
            Middle_Name = middle_Name;
            Email_address = email_address;
            ID_status_client = iD_status_client;
            Passport_number_and_series = passport_number_and_series;
            ID_Category = iD_Category;
        }
    }
}
