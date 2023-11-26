using BLL.Models;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.Services
{
    public interface IAddressesService
    {
        Task<Adresses> InsertAddresses(Adresses Addresses);
        Task<Adresses> UpdateAddresses(Adresses Addresses);
        Task<int> GetTotalRowsAsysnc(SearchParamsAddresses searchParams);
        Task<int> DeleteAddresses(int id);
        Task<IEnumerable<Adresses>> GetAddressess(SearchParamsAddresses searchParameters);
    }
}