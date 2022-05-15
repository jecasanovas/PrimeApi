using BLL.Dtos;
using BLL.Models;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.Repositories
{
    public interface IAddressesRepository
    {
        Task<Adresses> InsertAddresses(AdressesDto Addresses);
        Task<Adresses> UpdateAddresses(AdressesDto Addresses);
        Task<int> GetTotalAddresses(SearchParamsAddresses searchParams);

        Task<int> DeleteAddresses(int id);
        Task<IEnumerable<Adresses>> GetAddressess(SearchParamsAddresses searchParameters);
    }
}
