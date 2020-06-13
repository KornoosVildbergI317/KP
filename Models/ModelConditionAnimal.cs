using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooMail.Models
{
    class ModelConditionAnimal
    {
        public int Id_condition_Animal { get; set; }
        public string Title { get; set; }

        public ModelConditionAnimal(int id_condition_Animal, string title)
        {
            Id_condition_Animal = id_condition_Animal;
            Title = title;
        }
    }
}