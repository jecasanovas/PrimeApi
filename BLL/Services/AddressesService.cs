using BLL.Interfaces;
using BLL.Interfaces.Repositories;
using BLL.Interfaces.Services;
using BLL.Parameters;
using BLL.SearchParams;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AddressesService : IAddressesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Adresses> _addresesRepository;
        public AddressesService(IUnitOfWork unitOfWork, IGenericRepository<Adresses> addresesRepository)
        {
            _unitOfWork = unitOfWork;
            _addresesRepository = addresesRepository;
        }
        public async Task<bool> DeleteAddressesAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync(cancellationToken);
                var addrese = await _addresesRepository.GetByIdAsync(id);
                _unitOfWork.Repository<Adresses>().Delete(addrese);
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
        public async Task<IEnumerable<Adresses>> GetAddressessAsync(SearchParamsAddresses searchParameters, CancellationToken cancellationToken)
        {
            var criteria = new AddressesParams(searchParameters);
            return await _addresesRepository.ListAsync(criteria, cancellationToken);
        }
        public async Task<int> GetTotalRowsAsync(SearchParamsAddresses searchParams, CancellationToken cancellationToken)
        {
            var criteria = new AddressesParams(searchParams);
            return await _addresesRepository.CountAsync(criteria, cancellationToken);
        }
        public async Task<int> InsertAddressesAsync(Adresses Addresses, CancellationToken cancellationToken)
        {
            return await UpdateAddressesAsync(Addresses, cancellationToken);
        }
        public async Task<int> UpdateAddressesAsync(Adresses addresses, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync(cancellationToken);
                if (addresses.Id > 0)
                    _unitOfWork.Repository<Adresses>().Update(addresses);
                else
                    _unitOfWork.Repository<Adresses>().Add(addresses);
                await _unitOfWork.CompleteAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
                return addresses.Id;

            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }


        }
    }
}
