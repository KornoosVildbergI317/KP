using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooMail.Models
{
    class ModelBuy_animal
    {
        public int ID_Buy_animal { get; set; }
        public string Buy_animal_date_and_time { get; set; }
        public string Import_date_and_time { get; set; }
        public int ID_animalpark { get; set; }

        public int ID_Authorization { get; set; }

        public ModelBuy_animal(int iD_Buy_animal, string buy_animal_date_and_time, string import_date_and_time, int iD_animalpark, int iD_Authorization)
        {
            ID_Buy_animal = iD_Buy_animal;
            Buy_animal_date_and_time = buy_animal_date_and_time;
            Import_date_and_time = import_date_and_time;
            ID_animalpark = iD_animalpark;
            ID_Authorization = iD_Authorization;
        }
    }
}