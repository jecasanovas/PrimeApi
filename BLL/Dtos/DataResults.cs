using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos
{
    public class DataResults<T>
    {
        public IEnumerable<T> Dto { get; set; }
        public int Results { get; set; }

    }
}
