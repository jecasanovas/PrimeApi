using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;


namespace Core.Entities
{
    public partial class Teacher : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public int CountryId { get; set; }
        public virtual Country Country { get; set; }

        public string UrlSite { get; set; }
        public string UrlSocial { get; set; }
        public string Photo { get; set; }


        public string description { get; set; }

    }
}
