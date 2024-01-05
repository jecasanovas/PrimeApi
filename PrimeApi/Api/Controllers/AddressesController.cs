using Microsoft.AspNetCore.Authorization;
using API.Helpers;
using Microsoft.AspNetCore.Mvc;
using Courses.Api.Controllers;
using BLL.Dtos;
using BLL.SearchParams;
using MediatR;
using BLL.CQRS.Queries;
using BLL.CQRS.Commands;

namespace PrimeApi.Api.Controllers
{

    public class AddressesController : BaseApiController
    {
        private readonly IMediator _mediator;
        public AddressesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<Paginator<AddressesDto>>> GetAddresses([FromQuery] SearchParamsAddresses searchParameters)
        {
            try
            {
                var result = await _mediator.Send(new GetAddressesQuery()
                {
                    searchParams = searchParameters
                });
                return new Paginator<AddressesDto>(searchParameters.page, searchParameters.pageSize, result.Results, (IReadOnlyList<AddressesDto>)result.Dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<int>> UpdateAddress([FromBody] AddressesDto addressDto)
        {
            try
            {
                return Ok(await _mediator.Send(new UpdateAddressesCommand()
                {
                    addressDto = addressDto
                }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<int>> InsertAddress([FromBody] AddressesDto addressDto)
        {
            try
            {
                return Ok(await _mediator.Send(new InsertAddressesCommand()
                {
                    AddressDto = addressDto
                }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [Authorize]
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteAddress([FromQuery] int id)
        {
            try
            {
                return Ok(await _mediator.Send(new DeleteAddressesCommand()
                {
                    idAddress = id
                }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
