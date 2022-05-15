using BLL.Dtos;
using BLL.Models;
using BLL.Parameters;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.Repositories
{
    public interface ITechnologyDetailRepository
    {
        Task<int> InsertTechnologyDetail(TechnologyDetailsDto technologyDetails);
        Task<int> UpdateTechnologyDetail(TechnologyDetailsDto technologyDetails);

        Task DeleteTechnologyDetail (int id);

        Task<IEnumerable<TechnologyDetailsDto>> GetTechnologyDetails(SearchParam searchParam);
        Task<int> GetTotalTechnologysDetails(SearchParam searchParam);

    }
}
