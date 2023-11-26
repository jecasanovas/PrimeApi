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
        public async Task<ActionResult<Pagination<TeacherDto>>> Teacher([FromQuery] SearchParamTeachers searchParameters)
        {
            try
            {
                var result = await _mediator.Send(new GetTeacherQuery()
                {
                    searchParams = searchParameters
                });

                return new Pagination<TeacherDto>(searchParameters.page, searchParameters.pageSize, result.Results, (IReadOnlyList<TeacherDto>)result.Dto);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<TeacherDto>> UpdateTeacher([FromBody] TeacherDto teacherDto)
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
        public async Task<ActionResult> CreateTeacher([FromBody] TeacherDto teacherDto)
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
        public async Task<ActionResult> DeleteTeacher([FromQuery] int id)
        {

            try
            {
                await _mediator.Send(new DeleteTeacherCommand()
                {
                    TeacherId = id
                });
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
                var file = Request.Form.Files[0];
                if (file == null || file.Length == 0)
                {
                    return BadRequest();
                }
                var teacherResponse = await _teacherService.PostFile(id, Request.Form.Files[0]);
                return Ok(_mapper.Map<TeacherDto>(teacherResponse));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }


    }
}

