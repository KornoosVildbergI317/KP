using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooMail.Models
{
    class ModelEmployee
    {
        public int ID_Authorization { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Middle_Name { get; set; }

        public ModelEmployee(int iD_Authorization, string surname, string name, string middle_Name)
        {
            ID_Authorization = iD_Authorization;
            Surname = surname;
            Name = name;
            Middle_Name = middle_Name;

        }
    }
}
