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
    public interface ITechnologyRepository
    {
        Task<int> InsertTechnology(TechnologyDto technologyDto);
        Task<int> UpdateTechnology(TechnologyDto technologyDto);

        Task DeleteTechnology(int id);

        Task<IEnumerable<TechnologyDto>> GetTechnology(SearchParam searchParam);
        Task<int> GetTotalTechnology(SearchParam searchParam);
    }
}
