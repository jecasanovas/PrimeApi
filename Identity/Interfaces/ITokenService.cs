using Identity.Models;

namespace Identity.Interfaces
{
    public interface ITokenService
    {
         string CreateToken(AppUser user);
    }
}