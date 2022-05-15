using AutoMapper;
using BLL.Interfaces;
using BLL.Interfaces.Repositories;
using BLL.Interfaces.Services;
using BLL.Models;
using BLL.Parameters;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService : IUserService
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
        public async Task<IEnumerable<UserInfo>> GetUsers(SearchParamUsers searchParameters)
        {
            return await _userRepository.ListAsync(new UserParam(searchParameters));
        }

        public async Task<int> GetTotalRowsAsysnc(SearchParamUsers searchParameters)
        {
            return await _userRepository.CountAsync(new UserParam(searchParameters, true));
        }


        public async Task<UserInfo> InsertUser(UserInfo user)
        {

            return await UpdateUser(user);
        }

        public async Task<UserInfo> PostFile(int id, IFormFile file)
        {
            try
            {
                var result = await _photoService.AddPhotoAsync(file);
                await _unitOfWork.BeginTransactionAsync();

                var user = await _userRepository.GetByIdAsync(id);
                Uri url = result.SecureUrl;
                user.Photo = url.AbsoluteUri;

                _unitOfWork.Repository<UserInfo>().Update(user);

                await _unitOfWork.Complete();

                await _unitOfWork.CommitTransaction();

                return user;


            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<UserInfo> UpdateUser(UserInfo user)
        {
            await _unitOfWork.BeginTransactionAsync();



            if (user.Id > 0)
                _unitOfWork.Repository<UserInfo>().Update(user);
            else
                _unitOfWork.Repository<UserInfo>().Add(user);


            await _unitOfWork.Complete();

            await _unitOfWork.CommitTransaction();

            return user;
        }

        public async Task<int> DeleteUser(int id)
        {
            await _unitOfWork.BeginTransactionAsync();

            var entityTeacher = await _userRepository.GetByIdAsync(id);

            _unitOfWork.Repository<UserInfo>().Delete(entityTeacher);

            await _unitOfWork.Complete();

            await _unitOfWork.CommitTransaction();

            return 1;

        }

    }
}
