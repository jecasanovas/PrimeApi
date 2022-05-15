using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class SearchParamUsers
    {
        public int ? id  { get; set; }

        public int page { get; set; }

        public int pageSize { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string? Order { get; set; }


    }
}
