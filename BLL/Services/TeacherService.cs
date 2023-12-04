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
    public class TeacherService : ITeacherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Teacher> _teacherRepository;
        private readonly IPhotoService _photoService;

        public TeacherService(IUnitOfWork unitOfWork, IGenericRepository<Teacher> teacherRepository, IPhotoService photoService)
        {
            _unitOfWork = unitOfWork;
            _teacherRepository = teacherRepository;
            _photoService = photoService;
        }
        public async Task<IEnumerable<Teacher>> GetTeachersAsync(SearchParamTeachers searchParameters, CancellationToken cancellationToken)
        {
            var criteria = new TeacherParams(searchParameters);
            return await _teacherRepository.ListAsync(criteria, cancellationToken);
        }

        public async Task<int> GetTotalRowsAsync(SearchParamTeachers searchParameters, CancellationToken cancellationToken)
        {
            var criteria = new TeacherParams(searchParameters, true);
            return await _teacherRepository.CountAsync(criteria, cancellationToken);
        }
        public async Task<int> InsertTeacherAsync(Teacher teacher, CancellationToken cancellationToken)
        {
            return await UpdateTeacherAsync(teacher, cancellationToken);
        }
        public async Task<int> PostFileAsync(int id, IFormFile file, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _photoService.AddPhotoAsync(file);
                await _unitOfWork.BeginTransactionAsync(cancellationToken);
                var teacher = await _teacherRepository.GetByIdAsync(id);
                Uri url = result.SecureUrl;
                teacher.Photo = url.AbsoluteUri;
                _unitOfWork.Repository<Teacher>().Update(teacher);
                await _unitOfWork.CompleteAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
                return teacher.Id;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }
        public async Task<int> UpdateTeacherAsync(Teacher teacher, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync(cancellationToken);
                if (teacher.Id > 0)
                    _unitOfWork.Repository<Teacher>().Update(teacher);
                else
                    _unitOfWork.Repository<Teacher>().Add(teacher);
                await _unitOfWork.CompleteAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
                return teacher.Id;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }
        public async Task<bool> DeleteTeacherAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync(cancellationToken);
                var entityTeacher = await _teacherRepository.GetByIdAsync(id);
                _unitOfWork.Repository<Teacher>().Delete(entityTeacher);
                await _unitOfWork.CompleteAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
                return true;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }
    }
}
