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
    public class TeacherController : BaseApiController
    {
        private readonly ITeacherRepository _teacher;
        public readonly IMapper _mapper;

        public TeacherController(GestionCursosContext context, IMapper mapper, ITeacherRepository teacher)
        {
            _teacher = teacher;
            _mapper = mapper;
        }



        [HttpGet]
        [Authorize]
        public async Task<ActionResult<Pagination<TeacherDto>>> Teacher([FromQuery] SearchParamTeachers searchParameters)
        {
            try
            {
                var result = await _teacher.GetTeachers(searchParameters);
               IEnumerable<TeacherDto> data = _mapper.Map<IEnumerable<Teacher>, IEnumerable<TeacherDto>>(result);
                var rows = await _teacher.GetTotalTeachers(searchParameters);

                return new Pagination<TeacherDto>(searchParameters.page, searchParameters.pageSize, rows, (IReadOnlyList<TeacherDto>)data);

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
                var teacherResponse =  await _teacher.UpdateTeacher(teacherDto);
                return Ok(_mapper.Map<TeacherDto>(teacherResponse));
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
                var teacherResponse = await _teacher.UpdateTeacher(teacherDto);
                return Ok(_mapper.Map<TeacherDto>(teacherResponse));

             
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }

        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> DeleteTeacher([FromQuery]int id)
        {

            try
            {
                await _teacher.DeleteTeacher(id);
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
                var teacherResponse = await _teacher.PostFile(id, Request.Form.Files[0]);
                return Ok(_mapper.Map<TeacherDto>(teacherResponse));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }


    }
}

