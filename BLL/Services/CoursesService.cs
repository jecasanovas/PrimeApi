using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces;
using BLL.Interfaces.Repositories;
using BLL.Models;
using BLL.Parameters;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CoursesService  : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Course> _courseRepository;
        private readonly IGenericRepository<CourseDetail> _courseDetailRepository;
        private readonly IPhotoService _photoService;

        public CoursesService(IUnitOfWork unitOfWork,  IGenericRepository<Course> courseRepository, 
            IGenericRepository<CourseDetail> courseDetailRepository, IPhotoService photoService)
        {
            _unitOfWork = unitOfWork;
            _courseRepository = courseRepository;
            _courseDetailRepository = courseDetailRepository;
            _photoService = photoService;

        }

        public async Task<Course> GetCoursebyId(int id)
        {
            return await _courseRepository.GetByIdAsync(id);
        }


        public async Task<Course> PostFile(int id, IFormFile file)
        {
            try
            {
                var result = await _photoService.AddPhotoAsync(file);
                await _unitOfWork.BeginTransactionAsync();

                var course = await _courseRepository.GetByIdAsync(id);
                Uri url = result.SecureUrl;
                course.Photo = url.AbsoluteUri;

                _unitOfWork.Repository<Course>().Update(course);

                await _unitOfWork.Complete();

                await _unitOfWork.CommitTransaction();

                return course;

            }
            catch (Exception)
            {
                return null;
            }
        }


        public async Task<IEnumerable<Course>> GetCourses(SearchParamCourses searchParams)
        {
            return await _courseRepository.ListAsync(new CoursesParams(searchParams));

        }

        public async Task<IEnumerable<CourseDetail>> GetCourseDetails(SearchParamCourses searchParams)
        {
            return await _courseDetailRepository.ListAsync(new CourseDetailsParam(searchParams));

        }

        public async Task<int> GetTotalRowsAsysnc(SearchParamCourses searchParams)
        {
            return await _courseRepository.CountAsync(new CoursesParams(searchParams, true));
        }


        public async Task<int> GetTotalDetailRowsAsysnc(SearchParamCourses searchParameters)
        {
            return await _courseDetailRepository.CountAsync(new CourseDetailsParam(searchParameters, true));
        }

        public async Task<Course> UpdateCourse(Course course)
        {
            await _unitOfWork.BeginTransactionAsync();

            if (course.Id == 0)
                _unitOfWork.Repository<Course>().Add(course);
            else
                _unitOfWork.Repository<Course>().Update(course);

            await _unitOfWork.Complete();

            await _unitOfWork.CommitTransaction();

            return course;
        }

        public async Task<Course> InsertCourse(Course course)
        {
            return await UpdateCourse(course);
        }

        public async Task<IEnumerable<CourseDetail>> InsertCourseDetailsMasive(IEnumerable<CourseDetail> courseDetails)
        {
            await _unitOfWork.BeginTransactionAsync();

            _unitOfWork.Repository<CourseDetail>().AddRange(courseDetails);

            await _unitOfWork.Complete();

            await _unitOfWork.CommitTransaction();

            return courseDetails;
        }


        public async Task<CourseDetail> InsertCourseDetail(CourseDetail courseDetail)
        {
            return await UpdateCourseDetails(courseDetail);
        }

        public async Task<CourseDetail> UpdateCourseDetails(CourseDetail courseDetail)
        {
            await _unitOfWork.BeginTransactionAsync();

            if (courseDetail.Id == 0)
                _unitOfWork.Repository<CourseDetail>().Add(courseDetail);
            else
                _unitOfWork.Repository<CourseDetail>().Update(courseDetail);

            await _unitOfWork.Complete();



            await _unitOfWork.CommitTransaction();

            return courseDetail;
        }

        public async Task<Course> DeleteCourse(int id)
        {

            await _unitOfWork.BeginTransactionAsync();

            var course = await _courseRepository.GetByIdAsync(id);
            _unitOfWork.Repository<Course>().Delete(course);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();

            return course;

        }
    }
}