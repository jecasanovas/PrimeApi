using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class SearchParamsPaymentInfo
    {

            public int? UserInfoId { get; set; }
            public int page { get; set; }

            public int pageSize { get; set; }

            public int onlOnlyCount { get; set; }

            public string order { get; set; }

            public string Name { get; set; }

            public string CardHolder { get; set; }


        }
    }




