using System;
using System.Collections.Generic;
using System.Linq;
using Core.DBContext;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using API.Helpers;
using BLL.Interfaces.Repositories;
using AutoMapper;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;
using Courses.Api.Controllers;
using BLL.Dtos;

namespace PrimeApi.Api.Controllers
{

    public class AddressesController: BaseApiController
    {


        private readonly IAddressesRepository _addresses;
        public readonly IMapper _mapper;

        public AddressesController(IMapper mapper, IAddressesRepository userRepository)
        {
            _addresses = userRepository;
            _mapper = mapper;
        }



        [HttpGet]
        public async Task<ActionResult<Pagination<AdressesDto>>> GetAddresses([FromQuery] SearchParamsAddresses searchParameters)
        {
            try
            {
                var result = await _addresses.GetAddressess(searchParameters);
                IEnumerable<AdressesDto> data = _mapper.Map<IEnumerable<Adresses>, IEnumerable<AdressesDto>>(result);
                var rows = await _addresses.GetTotalAddresses(searchParameters);

                return  new Pagination<BLL.Dtos.AdressesDto> (searchParameters.page, searchParameters.pageSize, rows, (IReadOnlyList<BLL.Dtos.AdressesDto>)data);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }


        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<AddressDto>> UpdateAdrese([FromBody] AdressesDto addressDto)
        {

            try
            {
                var addreses = await _addresses.UpdateAddresses(addressDto);
                return Ok(_mapper.Map<AdressesDto>(addreses));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> CreateAddress([FromBody] AdressesDto addressesDto)
        {

            try
            {
                var addresses = await _addresses.InsertAddresses(addressesDto);
                return Ok(_mapper.Map<AdressesDto>(addresses));


            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }

        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> DeleteAddrese([FromQuery] int id)
        {
            try
            {
                await _addresses.DeleteAddresses(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }

    }
}
