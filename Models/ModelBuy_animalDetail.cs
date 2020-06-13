using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooMail.Models
{
    class ModelBuy_animalDetail
    {
        public int ID_Buy_animal { get; set; }
        public String AnimalTitle { get; set; }
        public string Buy_animal_date_and_time { get; set; }
        public string Import_date_and_time { get; set; }

        public ModelBuy_animalDetail(int iD_Buy_animal, string carTitle, string buy_animal_date_and_time, string import_date_and_time)
        {
            ID_Buy_animal = iD_Buy_animal;
            AnimalTitle = carTitle;
            Buy_animal_date_and_time = buy_animal_date_and_time;
            Import_date_and_time = import_date_and_time;
        }
    }
}
