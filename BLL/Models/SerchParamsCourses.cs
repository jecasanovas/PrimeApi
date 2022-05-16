using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class SearchParamCourses
    {

        public int? id  { get; set; }
        public int? lessonID { get; set; }

        public int page { get; set; }

        public int pageSize { get; set; }

        public string description { get; set; }

        public int CourseId { get; set; }
        public string name { get; set; }
        public object Name { get; internal set; }
        public string order { get; set; }
        
        public string idteacher { get; set; }

        public bool onlycount { get; set; }

        public string idtec { get; set; }

        public string idtechdet { get; set; }


    }
}
