using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooMail.Models
{
    class ModelRole
    {
        public int Id_Role { get; set; }
        public string Title_Role { get; set; }
        public int Add_client { get; set; }
        public int Add_animal { get; set; }
        public int Buy_animal { get; set; }
        public int Lock_client { get; set; }
        public int Accounting_animals { get; set; }

        public ModelRole(int id_Role, string title_Role, int add_client, int add_animal, int buy_animal, int lock_client, int accounting_animals)
        {
            Id_Role = id_Role;
            Title_Role = title_Role;
            Add_client = add_client;
            Add_animal = add_animal;
            Buy_animal = buy_animal;
            Lock_client = lock_client;
            Accounting_animals = accounting_animals;
        }
    }
}
