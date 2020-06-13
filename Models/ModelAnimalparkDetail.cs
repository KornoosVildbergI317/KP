using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooMail.Models
{
    class ModelAnimalparkDetail
    {
        public int ID_animalpark { get; set; }
        public string Number { get; set; }


        public string CategoryTitle { get; set; }


        public string SpeciesSupplierTitle { get; set; }
        public string StatusAnimalTitle { get; set; }

        public ModelAnimalparkDetail(int iD_animalpark, string number, string categoryTitle, string speciesSupplierTitle, string statusAnimalTitle)
        {
            ID_animalpark = iD_animalpark;
            Number = number;
            CategoryTitle = categoryTitle;
            SpeciesSupplierTitle = speciesSupplierTitle;
            StatusAnimalTitle = statusAnimalTitle;
        }


    }
}
