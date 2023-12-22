using System;
using System.Collections.Generic;
using Core.Entities;


namespace Core.Entities
{
    public class Course : BaseEntity
    {
        public string Name { get; set; }
        public int TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }
        public string Url { get; set; }
        public int TechnologyId { get; set; }
        public virtual Technology Technology { get; set; }
        public int TechnologyDetailsId { get; set; }
        public virtual TechnologyDetail TechnologyDetails { get; set; }
        public string Description { get; set; }
        public string SeachKeywords { get; set; }
        public string Photo { get; set; }
        public virtual List<CourseDetail> CourseDetail { get; set; }
    }
}
