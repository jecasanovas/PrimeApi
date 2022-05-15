using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces.Repositories;
using BLL.Interfaces.Services;
using BLL.Models;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories
{
    public class AddressesRepository : IAddressesRepository

    {

        private readonly IAddressesService _addressesService;
        private readonly IMapper _mapper;

        public AddressesRepository(IAddressesService addressesService, IMapper mapper)
        {
            _addressesService = addressesService;
            _mapper = mapper;   
        }

        public async Task<int> DeleteAddresses(int id)
        {
            return await _addressesService.DeleteAddresses(id);
        }

        public async Task<IEnumerable<Adresses>> GetAddressess(SearchParamsAddresses searchParameters)
        {
            return await _addressesService.GetAddressess(searchParameters);
        }

        public async Task<int> GetTotalAddresses(SearchParamsAddresses searchParams)
        {
            return await _addressesService.GetTotalRowsAsysnc(searchParams);
        }

        public async Task<Adresses> InsertAddresses(AdressesDto addresseDto)
        {
            var addresse = _mapper.Map<Adresses>(addresseDto);
            return await  _addressesService.InsertAddresses(addresse);
        }

        public async Task<Adresses> UpdateAddresses(AdressesDto addresseDto)
        {
            var addresse = _mapper.Map<Adresses>(addresseDto);
            return await _addressesService.UpdateAddresses(addresse);
        }
    }
}
