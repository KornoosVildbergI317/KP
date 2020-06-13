using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooMail.Models
{
    class ModelSupplier
    {
        public int Id_Supplier { get; set; }
        public string Title { get; set; }
        public string Release_date { get; set; }
        public int Id_Species { get; set; }

        public ModelSupplier(int id_supplier, string title, string release_date, int id_species)
        {
            Id_Supplier = id_supplier;
            Title = title;
            Release_date = release_date;
            Id_Species = id_species;
        }
    }
}