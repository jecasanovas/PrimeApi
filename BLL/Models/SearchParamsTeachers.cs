using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class SearchParamTeachers
    {
        public int page { get; set; }

        public int pageSize { get; set; }
        
        public string TeacherName { get; set; }


    }
}
