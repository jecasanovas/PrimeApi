using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces;
using BLL.Interfaces.Repositories;
using BLL.Models;
using Core.Entities;
using Microsoft.AspNetCore.Http;

namespace BLL.Repositories
{
    public class TeacherRepository : ITeacherRepository

    {
        private readonly ITeacherService _teacherService;
        private readonly IMapper _mapper;  


        public TeacherRepository(ITeacherService teacherService, IMapper mapper)
        {
            _teacherService = teacherService;
            _mapper = mapper;
        }


        public async Task<Teacher> PostFile(int id,  IFormFile file)
        {

            return await _teacherService.PostFile(id, file);
        }

        public async Task<Teacher> UpdateTeacher(TeacherDto teacherDto)
        {
            var teacher =  _mapper.Map<Teacher>(teacherDto);
            return await _teacherService.UpdateTeacher(teacher);

        }

        public async Task DeleteTeacher(int id)
        {
            await _teacherService.DeleteTeacher(id);
        }

        public async Task<Teacher> InsertTeacher(TeacherDto teacherDto)
        {
            var teacher = _mapper.Map<Teacher>(teacherDto);
            return await _teacherService.InsertTeacher(teacher);
        }

        public async Task<IEnumerable<Teacher>> GetTeachers(SearchParamTeachers searchParameters)
        {
            return await _teacherService.GetTeachers(searchParameters);
        }

        public async Task<int> GetTotalTeachers(SearchParamTeachers searchParameters)
        {
            return await _teacherService.GetTotalRowsAsysnc(searchParameters);
        }
    }
    
}
