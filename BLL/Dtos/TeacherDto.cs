using Microsoft.AspNetCore.Http;

namespace BLL.Dtos
{
    public class TeacherDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int CountryId { get; set; }

        public string CountryName { get; set; }
        public string UrlSite { get; set; }
        public string UrlSocial { get; set; }
        public string Photo { get; set; }

        public string description { get; set; }

    }
}