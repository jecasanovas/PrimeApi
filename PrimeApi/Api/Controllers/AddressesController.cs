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
using BLL.Interfaces.Services;
using Identity.Models;
using BLL.SearchParams;

namespace PrimeApi.Api.Controllers
{

    public class AddressesController : BaseApiController
    {


        private readonly IAddressesService _addresses;
        public readonly IMapper _mapper;

        public AddressesController(IMapper mapper, IAddressesService userRepository)
        {
            _addresses = userRepository;
            _mapper = mapper;
        }



        [HttpGet]
        public async Task<ActionResult<Pagination<AdressesDto>>> GetAddresses([FromQuery] SearchParamsAddresses searchParameters, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _addresses.GetAddressessAsync(searchParameters, cancellationToken);
                IEnumerable<AdressesDto> data = _mapper.Map<IEnumerable<Adresses>, IEnumerable<AdressesDto>>(result);
                var rows = await _addresses.GetTotalRowsAsync(searchParameters, cancellationToken);

                return new Pagination<BLL.Dtos.AdressesDto>(searchParameters.page, searchParameters.pageSize, rows, (IReadOnlyList<BLL.Dtos.AdressesDto>)data);

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
                var addreses = await _addresses.UpdateAddressesAsync(_mapper.Map<Adresses>(addressDto), CancellationToken.None);
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
                var addresses = await _addresses.InsertAddressesAsync(_mapper.Map<Adresses>(addressesDto), CancellationToken.None);
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
                await _addresses.DeleteAddressesAsync(id, CancellationToken.None);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }

    }
}
