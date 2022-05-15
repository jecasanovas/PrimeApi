using Core.Entities;
using BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces.Services
{
        public interface IUserService
        {

            Task<UserInfo> InsertUser(UserInfo user);
            Task<UserInfo> UpdateUser(UserInfo user);
            Task<int> GetTotalRowsAsysnc(SearchParamUsers searchParams);
            Task<UserInfo> PostFile(int id, Microsoft.AspNetCore.Http.IFormFile file);
            Task<int> DeleteUser(int id);
            Task<IEnumerable<UserInfo>> GetUsers(SearchParamUsers searchParameters);

        }
    }


