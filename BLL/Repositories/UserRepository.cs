using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces.Repositories;
using BLL.Interfaces.Services;
using BLL.Models;
using Core.Entities;
using Microsoft.AspNetCore.Http;

namespace BLL.Repositories
{
    public class UserRepository : IUserRepository

    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;


        public UserRepository(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }


        public async Task<UserInfo> PostFile(int id, IFormFile file)
        {

            return await _userService.PostFile(id, file);
        }

        public async Task<UserInfo> UpdateUser(UserInfoDto userInfoDto)
        {
            var user = _mapper.Map<UserInfo>(userInfoDto);
            return await _userService.UpdateUser(user);

        }

        public async Task DeleteUser(int id)
        {
            await _userService.DeleteUser(id);
        }

        public async Task<UserInfo> InsertUser(UserInfoDto userInfoDto)
        {
            var user = _mapper.Map<UserInfo>(userInfoDto);
            return await _userService.InsertUser(user);
        }

        public async Task<IEnumerable<UserInfo>> GetUsers(SearchParamUsers searchParameters)
        {
            return await _userService.GetUsers(searchParameters);
        }

        public async Task<int> GetTotalUsers(SearchParamUsers searchParameters)
        {
            return await _userService.GetTotalRowsAsysnc(searchParameters);
        }



    }

}
