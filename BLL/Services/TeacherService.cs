using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces;
using BLL.Interfaces.Repositories;
using BLL.Models;
using BLL.Parameters;
using Core.DBContext;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class TeacherService : ITeacherService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Teacher> _teacherRepository;
        private readonly IPhotoService _photoService;

     

        public TeacherService(IUnitOfWork unitOfWork, IMapper mapper, IGenericRepository<Teacher> teacherRepository, IPhotoService photoService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _teacherRepository = teacherRepository;
            _photoService = photoService;
     
        }
        public async Task<IEnumerable<Teacher>> GetTeachers(SearchParamTeachers searchParameters)
        {
            return await _teacherRepository.ListAsync(new TeacherParams(searchParameters));
        }

        public async Task<int> GetTotalRowsAsysnc(SearchParamTeachers searchParameters)
        {
            return await _teacherRepository.CountAsync(new TeacherParams(searchParameters, true));
        }


        public async Task<Teacher> InsertTeacher(Teacher teacher)
        {

            return await  UpdateTeacher(teacher);
        }

        public async Task<Teacher> PostFile(int id, IFormFile file)
        {
            try
            {
              var result =  await _photoService.AddPhotoAsync(file);
              await _unitOfWork.BeginTransactionAsync();

              var teacher = await _teacherRepository.GetByIdAsync(id);
                Uri url = result.SecureUrl;
                teacher.Photo = url.AbsoluteUri;

                _unitOfWork.Repository<Teacher>().Update(teacher);

                await _unitOfWork.Complete();

                await _unitOfWork.CommitTransaction();
               
                return teacher;


            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Teacher> UpdateTeacher(Teacher teacher)
        {
            await _unitOfWork.BeginTransactionAsync();

     

            if (teacher.Id > 0) 
                _unitOfWork.Repository<Teacher>().Update(teacher);
            else
                _unitOfWork.Repository<Teacher>().Add(teacher); 

  
            await _unitOfWork.Complete();

            await _unitOfWork.CommitTransaction();

            return teacher;
        }

        public async Task<int> DeleteTeacher(int id)
         {
           await _unitOfWork.BeginTransactionAsync();

            var entityTeacher = await _teacherRepository.GetByIdAsync(id);

            _unitOfWork.Repository<Teacher>().Delete(entityTeacher);

            await _unitOfWork.Complete();

            await _unitOfWork.CommitTransaction();

            return 1;

        }

    }
}
