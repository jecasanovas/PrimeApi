using System.Collections.Generic;
namespace BLL.Dtos
{
    public class DataResults<T>
    {
        public IEnumerable<T> Dto { get; set; }
        public int Results { get; set; }
    }
}
