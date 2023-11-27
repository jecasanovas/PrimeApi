using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.SearchParams
{
    public class SearchParamUsers : SearchParam
    {
        public int? id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Order { get; set; }


    }
}
