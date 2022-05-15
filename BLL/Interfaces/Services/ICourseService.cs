using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Dtos;
using BLL.Models;
using Core.Entities;
using Microsoft.AspNetCore.Http;

namespace BLL.Interfaces
{
    public interface ICourseService
    {
        Task<Course> InsertCourse(Course course);

        Task<CourseDetail> InsertCourseDetail(CourseDetail courseDetail);

        Task<Course> UpdateCourse(Course course);

        Task<Course> DeleteCourse(int id);
        Task<CourseDetail> UpdateCourseDetails(CourseDetail courseDetail);

        Task<IEnumerable<CourseDetail>> GetCourseDetails(SearchParamCourses searchParam);

        Task<Course> PostFile(int id, IFormFile file);

        Task<IEnumerable<Course>> GetCourses(SearchParamCourses searchParameters);

        Task<int> GetTotalRowsAsysnc(SearchParamCourses searchParams);


        Task<int> GetTotalDetailRowsAsysnc(SearchParamCourses searchParameters);

        Task<Course> GetCoursebyId(int id);

        Task<IEnumerable<CourseDetail>> InsertCourseDetailsMasive(IEnumerable<CourseDetail> courseDetails);




    }

}


