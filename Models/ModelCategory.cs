using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooMail.Models
{
    class ModelCategory
    {
        public int Id_category { get; set; }
        public string Title { get; set; }

        public ModelCategory(int id_category, string title)
        {
            Id_category = id_category;
            Title = title;
        }
    }
}

