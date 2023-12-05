using BLL.SearchParams;
using Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Interfaces.Services
{
    public interface IAddressesService
    {
        Task<int> InsertAddressesAsync(Adresses Addresses, CancellationToken cancellationToken);
        Task<int> UpdateAddressesAsync(Adresses Addresses, CancellationToken cancellationToken);
        Task<int> GetTotalRowsAsync(SearchParamsAddresses searchParams, CancellationToken cancellationToken);
        Task<bool> DeleteAddressesAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Adresses>> GetAddressessAsync(SearchParamsAddresses searchParameters, CancellationToken cancellationToken);
    }
}