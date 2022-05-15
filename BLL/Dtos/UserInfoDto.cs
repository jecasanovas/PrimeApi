using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos
{

        public class UserInfoDto 
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }

            public int CountryId { get; set; }
            public string Telephone { get; set; }
            public string Email { get; set; }
            public string Role { get; set; }
            public string Password { get; set; }
            public bool DoubleFactor { get; set; }
            public string Description { get; set; }

            public string Photo { get; set; }



        }
    }


