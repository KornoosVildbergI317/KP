using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooMail.Models
{
    class ModelStatusAnimal
    {
        public int Id_Status_Animal { get; set; }
        public string Title { get; set; }

        public ModelStatusAnimal(int id_Status_animal, string title)
        {
            Id_Status_Animal = id_Status_animal;
            Title = title;
        }
    }
}
