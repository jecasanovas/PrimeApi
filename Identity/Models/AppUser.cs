using Microsoft.AspNetCore.Identity;

namespace Identity.Models
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public Address Address { get; set; }
    }
}