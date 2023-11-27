using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.SearchParams
{
    public class SearchParamsPaymentInfo : SearchParam
    {

        public int? UserInfoId { get; set; }

        public int onlOnlyCount { get; set; }

        public string order { get; set; }

        public string Name { get; set; }

        public string CardHolder { get; set; }


    }
}




