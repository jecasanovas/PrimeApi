using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces;
using BLL.Interfaces.Repositories;
using BLL.Models;
using Core.DBContext;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Reposititories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ICourseService _courseService;
        private readonly IMapper _mapper;


        public CourseRepository(ICourseService courseService, IMapper mapper)
        {
            _courseService = courseService;
            _mapper = mapper;
        }

        public async Task<CourseDto> PostFile(int id, IFormFile file)
        {
            return _mapper.Map<CourseDto>(await _courseService.PostFile(id, file));
        }

        public async Task<CourseDto> InsertCourse(CourseDto courseDto)
        {
            var course =  await _courseService.InsertCourse(_mapper.Map<Course>(courseDto));
            return _mapper.Map <CourseDto>(course);
        }

        public async Task<CourseDto> UpdateCourse(CourseDto courseDto)
        {
            var course = await _courseService.UpdateCourse(_mapper.Map<Course>(courseDto));
            return  _mapper.Map<CourseDto>(course);
        }

   
        public async Task<CourseDetailDto> InsertCourseDetail(CourseDetailDto courseDetailDto)
        {
            var courseDetail =  await _courseService.InsertCourseDetail(_mapper.Map<CourseDetail>(courseDetailDto));
            return _mapper.Map<CourseDetailDto>(courseDetail);
        }

        public async Task<IEnumerable<CourseDetailDto>> InsertCourseDetailsMasive(IEnumerable<CourseDetailDto> courseDetailDto)
        {
            
            var courseDetail =  await _courseService.InsertCourseDetailsMasive(_mapper.Map<IEnumerable<CourseDetail>>(courseDetailDto));
            return _mapper.Map<IEnumerable<CourseDetailDto>>(courseDetail);
        }


        public async Task<CourseDetailDto> UpdateCourseDetail(CourseDetailDto courseDetailDto)
        {
            var courseDetail =  await _courseService.UpdateCourseDetails(_mapper.Map<CourseDetail>(courseDetailDto));
            return _mapper.Map<CourseDetailDto>(courseDetail);
        }


        public async Task<DataResults<CourseDto>> GetCourses(SearchParamCourses searchParameters)
        {
            var courses = await _courseService.GetCourses(searchParameters);
            //Count results without pagination active, for paging info
            var nrows = await _courseService.GetTotalRowsAsysnc(searchParameters);

            return new DataResults<CourseDto>()
            {
                Dto = _mapper.Map<IEnumerable<Course>, IEnumerable<CourseDto>>(courses),
                Results = nrows
           };
        }

        public async Task<IEnumerable<CourseDetail>> GetCourseDetails(SearchParamCourses searchParameters)
        {
            return await _courseService.GetCourseDetails(searchParameters);
        }

        public async Task<int> GetTotalCourses(SearchParamCourses searchParameters)
        {
            return await _courseService.GetTotalRowsAsysnc(searchParameters);
        }

        public async Task<int> GetTotalCoursesDetails(SearchParamCourses searchParameters)
        {
            return await _courseService.GetTotalDetailRowsAsysnc(searchParameters);
        }

        public async Task<CourseDto> DeleteCourse(int id)
        {
            var course =  await _courseService.DeleteCourse(id);
            return _mapper.Map<CourseDto>(course);
        }

       

    }
}
