using BLL.Interfaces;
using BLL.Interfaces.Repositories;
using BLL.Parameters;
using BLL.SearchParams;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CoursesService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Course> _courseRepository;
        private readonly IGenericRepository<CourseDetail> _courseDetailRepository;
        private readonly IPhotoService _photoService;
        public CoursesService(IUnitOfWork unitOfWork, IGenericRepository<Course> courseRepository,
            IGenericRepository<CourseDetail> courseDetailRepository, IPhotoService photoService)
        {
            _unitOfWork = unitOfWork;
            _courseRepository = courseRepository;
            _courseDetailRepository = courseDetailRepository;
            _photoService = photoService;
        }
        public async Task<Course> GetCoursebyIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _courseRepository.GetByIdAsync(id);
        }
        public async Task<Course> PostFileAsync(int id, IFormFile file, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _photoService.AddPhotoAsync(file);
                await _unitOfWork.BeginTransactionAsync(cancellationToken);
                var course = await _courseRepository.GetByIdAsync(id);
                Uri url = result.SecureUrl;
                course.Photo = url.AbsoluteUri;
                _unitOfWork.Repository<Course>().Update(course);
                await _unitOfWork.CompleteAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
                return course;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<IEnumerable<Course>> GetCoursesAsync(SearchParamCourses searchParamsCourses, CancellationToken cancellationToken)
        {
            var criteria = new CoursesParams(searchParamsCourses);
            return await _courseRepository.ListAsync(criteria, cancellationToken);
        }
        public async Task<IEnumerable<CourseDetail>> GetCourseDetailsAsync(SearchParamCourses searchParamCourses, CancellationToken cancellationToken)
        {
            var criteria = new CourseDetailsParam(searchParamCourses);
            return await _courseDetailRepository.ListAsync(criteria, cancellationToken);
        }
        public async Task<int> GetTotalRowsAsysnc(SearchParamCourses searchParams, CancellationToken cancellationToken)
        {
            var criteria = new CoursesParams(searchParams);
            return await _courseRepository.CountAsync(criteria, cancellationToken);
        }
        public async Task<int> GetTotalDetailRowsAsysnc(SearchParamCourses searchParameters, CancellationToken cancellationToken)
        {
            var criteria = new CourseDetailsParam(searchParameters, true);
            return await _courseDetailRepository.CountAsync(criteria, cancellationToken);
        }
        public async Task<Course> UpdateCourseAsync(Course course, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            if (course.Id == 0)
                _unitOfWork.Repository<Course>().Add(course);
            else
                _unitOfWork.Repository<Course>().Update(course);
            await _unitOfWork.CompleteAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);
            return course;
        }
        public async Task<Course> InsertCourseAsync(Course course, CancellationToken cancellationToken)
        {
            return await UpdateCourseAsync(course, cancellationToken);
        }
        public async Task<IEnumerable<CourseDetail>> InsertCourseDetailsMasiveAsync(IEnumerable<CourseDetail> courseDetails, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            _unitOfWork.Repository<CourseDetail>().AddRange(courseDetails);
            await _unitOfWork.CompleteAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);
            return courseDetails;
        }

        public async Task<CourseDetail> InsertCourseDetailAsync(CourseDetail courseDetail, CancellationToken cancellationToken)
        {
            return await UpdateCourseDetailsAsync(courseDetail, cancellationToken);
        }
        public async Task<CourseDetail> UpdateCourseDetailsAsync(CourseDetail courseDetail, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            if (courseDetail.Id == 0)
                _unitOfWork.Repository<CourseDetail>().Add(courseDetail);
            else
                _unitOfWork.Repository<CourseDetail>().Update(courseDetail);
            await _unitOfWork.CompleteAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);
            return courseDetail;
        }
        public async Task<Course> DeleteCourseAsync(int id, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            var course = await _courseRepository.GetByIdAsync(id);
            _unitOfWork.Repository<Course>().Delete(course);
            await _unitOfWork.CompleteAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);
            return course;
        }
    }
}