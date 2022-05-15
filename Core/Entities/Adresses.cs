using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Adresses: BaseEntity
    {

        public string Direction { get; set; }
        public char TypeOfDirection { get; set; }

        public string Telephone { get; set; }

        public string Description { get; set; }

        public int UserInfoId { get; set; }

        public string CP { get; set; }
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
        public char TypeOfDocument { get; set; }

        public string Document { get; set; }   




    }
}
