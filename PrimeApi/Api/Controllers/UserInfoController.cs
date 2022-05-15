using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Core.DBContext;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using BLL.Dtos;
using API.Helpers;
using BLL.Interfaces.Repositories;
using AutoMapper;
using BLL.Models;

namespace Courses.Api.Controllers
{
    public class UserInfoController : BaseApiController
    {
        private readonly IUserRepository _user;
        public readonly IMapper _mapper;

        public UserInfoController(IMapper mapper, IUserRepository userRepository)
        {
            _user = userRepository;
            _mapper = mapper;
        }



        [HttpGet]
        [Authorize]
        public async Task<ActionResult<Pagination<UserInfoDto>>> UserInfo([FromQuery] SearchParamUsers searchParameters)
        {
            try
            {
                var result = await _user.GetUsers(searchParameters);
                IEnumerable<UserInfoDto> data = _mapper.Map<IEnumerable<UserInfo>, IEnumerable<UserInfoDto>>(result);
                var rows = await _user.GetTotalUsers(searchParameters);

                return new Pagination<UserInfoDto>(searchParameters.page, searchParameters.pageSize, rows, (IReadOnlyList<UserInfoDto>)data);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }


        }

        [Authorize]
        [HttpPut]


        public async Task<ActionResult<UserInfoDto>> UpdateUser([FromBody] UserInfoDto UserInfoDto)
        {

            try
            {
                var teacherResponse = await _user.UpdateUser(UserInfoDto);
                return Ok(_mapper.Map<UserInfoDto>(teacherResponse));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] UserInfoDto UserInfoDto)
        {

            try
            {
                var teacherResponse = await _user.UpdateUser(UserInfoDto);
                return Ok(_mapper.Map<UserInfoDto>(teacherResponse));


            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }

        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> DeleteUser([FromQuery] int id)
        {

            try
            {
                await _user.DeleteUser(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }



        [Authorize]
        [HttpPost]
        [Route("File")]
        public async Task<ActionResult> PostFile(int id)
        {

            try
            {
                var file = Request.Form?.Files;
                if (file != null && file.Count > 0 )
                {
                    var teacherResponse = await _user.PostFile(id, file[0]);
                    return Ok(_mapper.Map<UserInfoDto>(teacherResponse));
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        
        }


    }
}
