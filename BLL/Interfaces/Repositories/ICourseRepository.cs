using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Dtos;
using BLL.Models;
using Core.Entities;
using Microsoft.AspNetCore.Http;

namespace BLL.Interfaces.Repositories
{
    public interface ICourseRepository
    {
        Task<CourseDto> InsertCourse(CourseDto courseDto);

        Task<CourseDto> UpdateCourse(CourseDto courseDto);

        Task<CourseDetailDto> InsertCourseDetail(CourseDetailDto courseDetailDto);

        Task<CourseDetailDto> UpdateCourseDetail(CourseDetailDto courseDetailDto);


        Task<CourseDto> PostFile(int id, IFormFile file);

        Task<CourseDto> DeleteCourse(int id);

        Task<DataResults<CourseDto>> GetCourses(SearchParamCourses searchParameters);

        Task<IEnumerable<CourseDetail>> GetCourseDetails(SearchParamCourses searchParameters);

  
        Task<int> GetTotalCourses(SearchParamCourses searchParameters);

        Task<int> GetTotalCoursesDetails(SearchParamCourses searchParameters);

        Task<IEnumerable<CourseDetailDto>> InsertCourseDetailsMasive(IEnumerable<CourseDetailDto> courseDetailDto);

    }
}


