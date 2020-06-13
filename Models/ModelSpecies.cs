using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooMail.Models
{
    class ModelSpecies
    {
        public int Id_species { get; set; }
        public string Title { get; set; }

        public ModelSpecies(int id_species, string title)
        {
            Id_species = id_species;
            Title = title;
        }
    }
}