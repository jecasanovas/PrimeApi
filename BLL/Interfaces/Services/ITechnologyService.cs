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
        Task<int> InsertTechnology(Technology technology);
        Task<int> InsertTechnologyDetail(TechnologyDetail technologyDetail);

        Task<int> UpdateTechnology(Technology technology);
        Task<int> UpdateTechnologyDetail(TechnologyDetail technologyDetail);

        Task<int> GetTotalRowsTechnology(SearchParam searchParam);
        Task<int> GetTotalRowsTechnologyDetails(SearchParam searchParam);

        Task DeleteTechnology(int id);

        Task DeleteTechnologyDetail(int id);
        Task<IEnumerable<Technology>> GetTechnology(SearchParam searchParam);

        Task<IEnumerable<TechnologyDetail>> GetTechnologyDetails(SearchParam searchParam);


    }
}
