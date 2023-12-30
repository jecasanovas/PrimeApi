using BLL.Dtos;
using BLL.Interfaces;
using BLL.Interfaces.Repositories;
using BLL.Interfaces.Services;
using BLL.Models;
using BLL.Parameters;
using BLL.SearchParams;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class TechnologyService : ITechnologyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Technology> _technologyRepository;
        private readonly IGenericRepository<TechnologyDetail> _technologyDetailRepository;
        public TechnologyService(IUnitOfWork unitOfWork, IGenericRepository<Technology> technologyRepository, IGenericRepository<TechnologyDetail> technologyDetailRepository)
        {
            _unitOfWork = unitOfWork;
            _technologyRepository = technologyRepository;
            _technologyDetailRepository = technologyDetailRepository;
        }
        public async Task<bool> DeleteTechnologyAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync(cancellationToken);
                var entityTechnology = await _unitOfWork.Repository<Technology>().GetByIdAsync(id);
                _unitOfWork.Repository<Technology>().Delete(entityTechnology);
                await _unitOfWork.CompleteAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
            return true;
        }
        public async Task<bool> DeleteTechnologyDetailAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync(cancellationToken);
                var entityTechnology = await _unitOfWork.Repository<TechnologyDetail>().GetByIdAsync(id);
                _unitOfWork.Repository<TechnologyDetail>().Delete(entityTechnology);
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
        public async Task<IEnumerable<Technology>> GetTechnologyAsync(SearchParam searchParam, CancellationToken cancellationToken)
        {
            var criteria = new GenericParams<Technology>(searchParam);
            return await _technologyRepository.ListAsync(criteria, cancellationToken);
        }
        public async Task<IEnumerable<TechnologyDetail>> GetTechnologyDetailsAsync(SearchParam searchParam, CancellationToken cancellationToken)
        {
            var criteria = new GenericParams<TechnologyDetail>(searchParam);
            return await _technologyDetailRepository.ListAsync(criteria, cancellationToken);
        }
        public async Task<int> GetTotalRowsTechnologyAsync(SearchParam searchParam, CancellationToken cancellationToken)
        {
            var criteria = new GenericParams<Technology>(searchParam, true);
            return await _technologyRepository.CountAsync(criteria, cancellationToken);
        }
        public async Task<int> GetTotalRowsTechnologyDetailsAsync(SearchParam searchParam, CancellationToken cancellationToken)
        {
            var criteria = new GenericParams<TechnologyDetail>(searchParam, true);
            return await _technologyDetailRepository.CountAsync(criteria, cancellationToken);
        }
        public async Task<int> InsertTechnologyAsync(Technology technology, CancellationToken cancellationToken)
        {
            return await UpdateTechnologyAsync(technology, cancellationToken);
        }
        public async Task<int> InsertTechnologyDetailAsync(TechnologyDetail technologyDetail, CancellationToken cancellationToken)
        {
            return await UpdateTechnologyDetailAsync(technologyDetail, cancellationToken);
        }
        public async Task<int> UpdateTechnologyAsync(Technology technology, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync(cancellationToken);
                if (technology.Id > 0)
                    _unitOfWork.Repository<Technology>().Update(technology);
                else
                    _unitOfWork.Repository<Technology>().Add(technology);

                await _unitOfWork.CompleteAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
                return technology.Id;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }


        public async Task<int> UpdateTechnologyDetailAsync(TechnologyDetail technologyDetails, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync(cancellationToken);
                if (technologyDetails.Id > 0)
                    _unitOfWork.Repository<TechnologyDetail>().Update(technologyDetails);
                else
                    _unitOfWork.Repository<TechnologyDetail>().Add(technologyDetails);

                await _unitOfWork.CompleteAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
                return technologyDetails.Id;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }
    }
}


