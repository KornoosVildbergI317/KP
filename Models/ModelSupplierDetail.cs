using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooMail.Models
{
    class ModelSupplierDetail
    {
        public int Id_Supplier { get; set; }
        public string SpeciesSupplierTitle { get; set; }
        public string Release_date { get; set; }

        public ModelSupplierDetail(int id_Supplier, string speciesSupplierTitle, string release_date)
        {
            Id_Supplier = id_Supplier;
            SpeciesSupplierTitle = speciesSupplierTitle;
            Release_date = release_date;
        }
    }
}