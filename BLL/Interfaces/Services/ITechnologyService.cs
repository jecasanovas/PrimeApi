using BLL.Dtos;
using BLL.Models;
using BLL.Parameters;
using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces.Services
{
    public interface ITechnologyService
    {
        Task<int> InsertTechnology(TechnologyDto technology);
        Task<int> InsertTechnologyDetail(TechnologyDetailsDto technologyDetails);

        Task<int> UpdateTechnology(TechnologyDto technology);
        Task<int> UpdateTechnologyDetail(TechnologyDetailsDto technologyDetails);

        Task<int> GetTotalRowsTechnology(SearchParam searchParam);
        Task<int> GetTotalRowsTechnologyDetails(SearchParam searchParam);

        Task DeleteTechnology(int id);

        Task DeleteTechnologyDetail(int id);
        Task<IEnumerable<Technology>> GetTechnology(SearchParam searchParam);

        Task<IEnumerable<TechnologyDetails>> GetTechnologyDetails(SearchParam searchParam);


    }
}
