using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class PaymentInfo:BaseEntity
    {
        public int UserInfoId { get; set; }
        public string Name { get; set; }

        public string CardHolderName { get; set; }

        public string CardNumber { get; set; }  

        public string MonthExp { get; set; }    

        public string YearExp { get; set; } 

        public String CCV { get; set; } 






    }
}
