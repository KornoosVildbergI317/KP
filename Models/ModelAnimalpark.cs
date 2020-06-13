using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooMail.Models
{
    class ModelAnimalpark
    {
        public int ID_animalpark { get; set; }
        public string Number { get; set; }
        public int ID_Category { get; set; }
        public int ID_Supplier { get; set; }
        public int ID_Status_Animal { get; set; }





        public ModelAnimalpark(int id_animalpark, string number, int id_category, int id_supplier, int id_status_animal)
        {
            ID_animalpark = id_animalpark;
            Number = number;
            ID_Category = id_category;
            ID_Supplier = id_supplier;
            ID_Status_Animal = id_status_animal;
        }
    }
}
