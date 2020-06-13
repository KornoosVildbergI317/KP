using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooMail.Models
{
    class ModelStatusClient
    {
     
        
            public int Id_status_client { get; set; }
            public string Title { get; set; }

            public ModelStatusClient(int id_status_client, string title)
            {
                Id_status_client = id_status_client;
                Title = title;
            }
        }
    }