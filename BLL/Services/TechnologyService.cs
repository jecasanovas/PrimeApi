using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces;
using BLL.Interfaces.Repositories;
using BLL.Interfaces.Services;
using BLL.Models;
using BLL.Parameters;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class TechnologyService : ITechnologyService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Technology> _technologyRepository;
        private readonly IGenericRepository<TechnologyDetails> _technologyDetailRepository;

        public TechnologyService(IUnitOfWork unitOfWork, IMapper mapper, IGenericRepository<Technology> technologyRepository, IGenericRepository<TechnologyDetails> technologyDetailRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _technologyRepository = technologyRepository;
            _technologyDetailRepository = technologyDetailRepository;

        }

        public async Task DeleteTechnology(int id)
        {
            await _unitOfWork.BeginTransactionAsync();
            var entityTechnology = await _unitOfWork.Repository<Technology>().GetByIdAsync(id);
            _unitOfWork.Repository<Technology>().Delete(entityTechnology);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();

        }


        public async Task DeleteTechnologyDetail(int id)
        {
            await _unitOfWork.BeginTransactionAsync();
            var entityTechnology = await _unitOfWork.Repository<TechnologyDetails>().GetByIdAsync(id);
            _unitOfWork.Repository<TechnologyDetails>().Delete(entityTechnology);
            await _unitOfWork.Complete();
            await _unitOfWork.CommitTransaction();

        }

        public async Task<IEnumerable<Technology>> GetTechnology(SearchParam searchParam)
        {
            return await _technologyRepository.ListAsync(new GenericParams<Technology>(searchParam));
        }

        public async Task<IEnumerable<TechnologyDetails>> GetTechnologyDetails(SearchParam searchParam)
        {
            return await _technologyDetailRepository.ListAsync(new GenericParams<TechnologyDetails>(searchParam));
        }

        public async Task<int> GetTotalRowsTechnology(SearchParam searchParam)
        {
            return await _technologyRepository.CountAsync(new GenericParams<Technology>(searchParam, true));
        }

        public async Task<int> GetTotalRowsTechnologyDetails(SearchParam searchParam)
        {
            return await _technologyDetailRepository.CountAsync(new GenericParams<TechnologyDetails>(searchParam, true));
        }


        public async Task<int> InsertTechnology(TechnologyDto technology)
        {
           return  await UpdateTechnology(technology);
            
        }

        public async Task<int> InsertTechnologyDetail(TechnologyDetailsDto technologyDetails)
        {
            return await UpdateTechnologyDetail(technologyDetails);
            
        }

        public async Task<int> UpdateTechnology(TechnologyDto technology)
        {
            await _unitOfWork.BeginTransactionAsync();

            var entityTechnology = _mapper.Map<Technology>(technology);

            if (technology.Id > 0)
                _unitOfWork.Repository<Technology>().Update(entityTechnology);
            else
                _unitOfWork.Repository<Technology>().Add(entityTechnology);

            await _unitOfWork.Complete();

            await _unitOfWork.CommitTransaction();
            return entityTechnology.Id;
        }

        public async Task<int> UpdateTechnologyDetail(TechnologyDetailsDto technologyDetails)
        {

            await _unitOfWork.BeginTransactionAsync();

            var entityTechnology = _mapper.Map<TechnologyDetails>(technologyDetails);

            if (entityTechnology.Id > 0)
                _unitOfWork.Repository<TechnologyDetails>().Update(entityTechnology);
            else
                _unitOfWork.Repository<TechnologyDetails>().Add(entityTechnology);

            await _unitOfWork.Complete();

            await _unitOfWork.CommitTransaction();

            return entityTechnology.Id;
        }
    }
}
 

