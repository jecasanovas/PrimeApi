using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BLL.Dtos;
using BLL.Models;
using BLL.SearchParams;
using Core.Entities;
using Microsoft.AspNetCore.Http;

namespace BLL.Interfaces
{
    public interface ICourseService
    {
        Task<int> InsertCourseAsync(Course course, CancellationToken cancellationToken);
        Task<int> InsertCourseDetailAsync(CourseDetail courseDetail, CancellationToken cancellationToken);
        Task<int> UpdateCourseAsync(Course course, CancellationToken cancellationToken);
        Task<bool> DeleteCourseAsync(int id, CancellationToken cancellationToken);
        Task<int> UpdateCourseDetailsAsync(CourseDetail courseDetail, CancellationToken cancellationToken);
        Task<IEnumerable<CourseDetail>> GetCourseDetailsAsync(SearchParamCourses searchParam, CancellationToken cancellationToken);
        Task<int> PostFileAsync(int id, IFormFile file, CancellationToken cancellationToken);
        Task<IEnumerable<Course>> GetCoursesAsync(SearchParamCourses searchParameters, CancellationToken cancellationToken);
        Task<int> GetTotalRowsAsysnc(SearchParamCourses searchParams, CancellationToken cancellationToken);
        Task<int> GetTotalDetailRowsAsysnc(SearchParamCourses searchParameters, CancellationToken cancellationToken);
        Task<Course> GetCoursebyIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> InsertCourseDetailsMasiveAsync(IEnumerable<CourseDetail> courseDetails, CancellationToken cancellationToken);
    }
}


