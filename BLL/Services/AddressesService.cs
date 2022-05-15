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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AddressesService : IAddressesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Adresses> _addresesRepository;
        private readonly IPhotoService _photoService;
        public AddressesService(IUnitOfWork unitOfWork, IMapper mapper, IGenericRepository<Adresses> addresesRepository, IPhotoService photoService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _addresesRepository = addresesRepository;
            _photoService = photoService;

        }
        public async Task<int> DeleteAddresses(int id)
        {
            await _unitOfWork.BeginTransactionAsync();

            var addrese = await _addresesRepository.GetByIdAsync(id);

            _unitOfWork.Repository<Adresses>().Delete(addrese);

            await _unitOfWork.Complete();

            await _unitOfWork.CommitTransaction();

            return 1;
        }

        public async Task<IEnumerable<Adresses>> GetAddressess(SearchParamsAddresses searchParameters)
        {
            return await _addresesRepository.ListAsync(new AddressesParams(searchParameters));
        }

        public async Task<int> GetTotalRowsAsysnc(SearchParamsAddresses searchParams)
        {
            return await _addresesRepository.CountAsync(new AddressesParams(searchParams, true));
        }

        public async Task<Adresses> InsertAddresses(Adresses Addresses)
        {
            return await UpdateAddresses(Addresses);
        }

        public async Task<Adresses> UpdateAddresses(Adresses addresse)
        {
            await _unitOfWork.BeginTransactionAsync();



            if (addresse.Id > 0)
                _unitOfWork.Repository<Adresses>().Update(addresse);
            else
                _unitOfWork.Repository<Adresses>().Add(addresse);


            await _unitOfWork.Complete();

            await _unitOfWork.CommitTransaction();

            return addresse;
        }
    }

}
