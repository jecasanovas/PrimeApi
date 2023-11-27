using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.SearchParams;
using System.Threading;

namespace BLL.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserInfo> InsertUserAsync(UserInfo user, CancellationToken cancellationToken);
        Task<UserInfo> UpdateUserAsync(UserInfo user, CancellationToken cancellationToken);
        Task<int> GetTotalRowsAsync(SearchParamUsers searchParams, CancellationToken cancellationToken);
        Task<UserInfo> PostFileAsync(int id, Microsoft.AspNetCore.Http.IFormFile file, CancellationToken cancellationToken = default);
        Task<int> DeleteUserAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<UserInfo>> GetUsersAsync(SearchParamUsers searchParameters, CancellationToken cancellationToken);
    }
}


