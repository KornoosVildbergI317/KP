using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooMail.Models
{
    class ModelClientDetail
    {
        public int ID_Authorization { get; set; }
        public string ClientName { get; set; }

        public string ClientStatus { get; set; }

        public ModelClientDetail(int iD_Authorization, string clientName, string clientStatus)
        {
            ID_Authorization = iD_Authorization;
            ClientName = clientName;
            ClientStatus = clientStatus;
        }
    }
}