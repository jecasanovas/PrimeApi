using BLL.Dtos;
using BLL.Models;
using BLL.Parameters;
using BLL.SearchParams;
using Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Interfaces.Services
{
    public interface ITechnologyService
    {
        Task<int> InsertTechnologyAsync(Technology technology, CancellationToken cancellationToken);
        Task<int> InsertTechnologyDetailAsync(TechnologyDetail technologyDetail, CancellationToken cancellationToken);
        Task<int> UpdateTechnologyAsync(Technology technology, CancellationToken cancellationToken);
        Task<int> UpdateTechnologyDetailAsync(TechnologyDetail technologyDetail, CancellationToken cancellationToken);
        Task<int> GetTotalRowsTechnologyAsync(SearchParam searchParam, CancellationToken cancellationToken);
        Task<int> GetTotalRowsTechnologyDetailsAsync(SearchParam searchParam, CancellationToken cancellationToken);
        Task DeleteTechnologyAsync(int id, CancellationToken cancellationToken);
        Task DeleteTechnologyDetailAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Technology>> GetTechnologyAsync(SearchParam searchParam, CancellationToken cancellationToken);
        Task<IEnumerable<TechnologyDetail>> GetTechnologyDetailsAsync(SearchParam searchParam, CancellationToken cancellationToken);
    }
}
