using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BLL.Dtos;
using API.Helpers;
using AutoMapper;
using BLL.Interfaces.Services;
using BLL.SearchParams;
using MediatR;
using BLL.CQRS.Queries;
using BLL.CQRS.Commands;


namespace Courses.Api.Controllers
{
    public class UserInfoController : BaseApiController
    {

        public readonly IMapper _mapper;

        public readonly IMediator _mediator;

        public readonly IUserInfoService _userInfoService;

        public UserInfoController(IMapper mapper, IMediator mediator, IUserInfoService userInfoService)
        {
            _mediator = mediator;
            _mapper = mapper;
            _userInfoService = userInfoService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<Pagination<UserInfoDto>>> GetUsersInfo([FromQuery] SearchParamUsers searchParameters)
        {
            try
            {
                var result = await _mediator.Send(new GetUserInfoQuery()
                {
                    searchParams = searchParameters
                });
                return new Pagination<UserInfoDto>(searchParameters.page, searchParameters.pageSize, result.Results, (IReadOnlyList<UserInfoDto>)result.Dto);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<UserInfoDto>> UpdateUserInfo([FromBody] UserInfoDto UserInfoDto)
        {

            try
            {
                return Ok(await _mediator.Send(new UpdateUserInfoCommand()
                {
                    userInfoDto = UserInfoDto
                }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> InsertUserInfo([FromBody] UserInfoDto UserInfoDto)
        {

            try
            {
                return Ok(await _mediator.Send(new UpdateUserInfoCommand()
                {
                    userInfoDto = UserInfoDto
                }));
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
                return Ok(await _userInfoService.DeleteUserInfoAsync(id, CancellationToken.None));
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
                if (file != null && file.Count > 0)
                {
                    var teacherResponse = await _userInfoService.PostFileAsync(id, file[0], CancellationToken.None);
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
