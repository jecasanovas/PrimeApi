using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BLL.Dtos;
using API.Helpers;
using BLL.Models;
using BLL.Interfaces;
using MediatR;
using BLL.CQRS.Queries;
using BLL.CQRS.Commands;
using AutoMapper;
using BLL.SearchParams;
using Core.Entities;

namespace Courses.Api.Controllers
{
    public class TeacherController : BaseApiController
    {
        private readonly ITeacherService _teacherService;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public TeacherController(IMapper mapper, IMediator mediator, ITeacherService teacherService)
        {
            _teacherService = teacherService;
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<Paginator<TeacherDto>>> Teacher([FromQuery] SearchParamTeachers searchParameters)
        {
            try
            {
                var result = await _mediator.Send(new GetTeacherQuery()
                {
                    searchParams = searchParameters
                });

                return new Paginator<TeacherDto>(searchParameters.page, searchParameters.pageSize, result.Results, (IReadOnlyList<TeacherDto>)result.Dto);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<int>> UpdateTeacher([FromBody] TeacherDto teacherDto)
        {
            try
            {
                return Ok(await _mediator.Send(new UpdateTeacherCommand()
                {
                    Teacher = teacherDto
                }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<int>> CreateTeacher([FromBody] TeacherDto teacherDto)
        {
            try
            {
                return Ok(await _mediator.Send(new InsertTeacherCommand()
                {
                    Teacher = teacherDto
                }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [Authorize]
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteTeacher([FromQuery] int id)
        {
            try
            {
                return Ok(await _mediator.Send(new DeleteTeacherCommand()
                {
                    TeacherId = id
                }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [Authorize]
        [HttpPost]
        [Route("File")]
        public async Task<ActionResult<Teacher>> PostFile(int id)
        {
            try
            {
                var file = Request.Form.Files[0];
                if (file == null || file.Length == 0)
                {
                    return NoContent();
                }
                var result = await _teacherService.PostFileAsync(id, Request.Form.Files[0], CancellationToken.None);
                return Ok(_mapper.Map<TeacherDto>(result));

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}

