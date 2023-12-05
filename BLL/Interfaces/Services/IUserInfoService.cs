using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.SearchParams;
using System.Threading;

namespace BLL.Interfaces.Services
{
    public interface IUserInfoService
    {
        Task<int> InsertUserInfoAsync(UserInfo user, CancellationToken cancellationToken);
        Task<int> UpdateUserInfoAsync(UserInfo user, CancellationToken cancellationToken);
        Task<int> GetTotalRowsAsync(SearchParamUsers searchParams, CancellationToken cancellationToken);
        Task<int> PostFileAsync(int id, Microsoft.AspNetCore.Http.IFormFile file, CancellationToken cancellationToken = default);
        Task<bool> DeleteUserInfoAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<UserInfo>> GetUsersInfoAsync(SearchParamUsers searchParameters, CancellationToken cancellationToken);
    }
}


