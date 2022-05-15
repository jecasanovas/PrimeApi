using BLL.Dtos;
using BLL.Models;
using Core.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.Repositories
{
    public interface IUserRepository
    {

        Task<UserInfo> InsertUser(UserInfoDto teacher);
        Task<UserInfo> UpdateUser(UserInfoDto teacher);

        Task DeleteUser(int id);


        Task<IEnumerable<UserInfo>> GetUsers(SearchParamUsers searchParamUsers);
        Task<int> GetTotalUsers(SearchParamUsers searchParamUsers);

        Task<UserInfo> PostFile(int id, Microsoft.AspNetCore.Http.IFormFile file);

   

    }
}
