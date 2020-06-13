using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooMail.Models
{
    class ModelPosition
    {
        public int Id_Position { get; set; }
        public string Title_position { get; set; }

        public ModelPosition(int id_Position, string title_position)
        {
            Id_Position = id_Position;
            Title_position = title_position;
        }
    }
}
