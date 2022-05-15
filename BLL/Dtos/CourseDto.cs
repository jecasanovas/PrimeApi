using System;

namespace BLL.Dtos
{
    public class CourseDto
    {

        public int Id { get; set; }

        public int TeacherId { get; set; }
        public string TeacherName { get; set; }

        public string CountryName { get; set; }


        public string Name { get; set; }
        public string Url { get; set; }
        public int TechnologyId { get; set; }

        public string TechnologyName { get; set; }

        public string TechnologyDetailsName { get; set; }   

        public string TeacherPhoto { get; set; }

        public string TeacherDescription { get; set; }

        public int TechnologyDetailsId { get; set; }
        public string Description { get; set; }
        public string SeachKeywords { get; set; }
        public string Photo { get; set; }

        public string CourseDetails { get; set; }
   }
}