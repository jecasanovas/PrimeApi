using AutoMapper;
using BLL.Interfaces;
using BLL.Interfaces.Repositories;
using BLL.Interfaces.Services;
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
    public class UserService : IUserInfoService
    {

        private readonly IUnitOfWork _unitOfWork;

        private readonly IGenericRepository<UserInfo> _userRepository;
        private readonly IPhotoService _photoService;



        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IGenericRepository<UserInfo> userRepository, IPhotoService photoService)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _photoService = photoService;

        }
        public async Task<IEnumerable<UserInfo>> GetUsersInfoAsync(SearchParamUsers searchParameters, CancellationToken cancellationToken)
        {
            var criteria = new UserParam(searchParameters);
            return await _userRepository.ListAsync(criteria, cancellationToken);
        }

        public async Task<int> GetTotalRowsAsync(SearchParamUsers searchParameters, CancellationToken cancellationToken)
        {
            var criteria = new UserParam(searchParameters, true);
            return await _userRepository.CountAsync(criteria, cancellationToken);
        }


        public async Task<int> InsertUserInfoAsync(UserInfo userInfo, CancellationToken cancellationToken)
        {
            return await UpdateUserInfoAsync(userInfo, cancellationToken);
        }

        public async Task<int> PostFileAsync(int id, IFormFile file, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _photoService.AddPhotoAsync(file);
                await _unitOfWork.BeginTransactionAsync(cancellationToken);
                var user = await _userRepository.GetByIdAsync(id);
                Uri url = result.SecureUrl;
                user.Photo = url.AbsoluteUri;
                _unitOfWork.Repository<UserInfo>().Update(user);
                await _unitOfWork.CompleteAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
                return user.Id;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }

        public async Task<int> UpdateUserInfoAsync(UserInfo user, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync(cancellationToken);
                if (user.Id > 0)
                    _unitOfWork.Repository<UserInfo>().Update(user);
                else
                    _unitOfWork.Repository<UserInfo>().Add(user);
                await _unitOfWork.CompleteAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
                return user.Id;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }

        public async Task<bool> DeleteUserInfoAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync(cancellationToken);
                var entityTeacher = await _userRepository.GetByIdAsync(id);
                _unitOfWork.Repository<UserInfo>().Delete(entityTeacher);
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
