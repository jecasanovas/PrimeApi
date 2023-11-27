using BLL.SearchParams;
using Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Interfaces.Services
{
    public interface IAddressesService
    {
        Task<Adresses> InsertAddressesAsync(Adresses Addresses, CancellationToken cancellationToken);
        Task<Adresses> UpdateAddressesAsync(Adresses Addresses, CancellationToken cancellationToken);
        Task<int> GetTotalRowsAsync(SearchParamsAddresses searchParams, CancellationToken cancellationToken);
        Task<int> DeleteAddressesAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Adresses>> GetAddressessAsync(SearchParamsAddresses searchParameters, CancellationToken cancellationToken);
    }
}