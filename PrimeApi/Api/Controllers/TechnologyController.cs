using BLL.CQRS.Commands;
using BLL.CQRS.Queries;
using BLL.Dtos;
using BLL.Interfaces.Services;
using BLL.Models;
using BLL.SearchParams;
using Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;



namespace Courses.Api.Controllers
{
    public class TechnologyController : BaseApiController
    {

        private readonly IMediator _mediator;

        public TechnologyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TechnologyDto>>> Technology([FromQuery] SearchParam searchParam)
        {
            try
            {
                return Ok(await _mediator.Send(new GetTechnologyQuery()
                {
                    searchParams = searchParam
                }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
        [HttpPut]
        public async Task<ActionResult<int>> UpdateTechnology([FromBody] TechnologyDto technologyDto)
        {
            try
            {
                return Ok(await _mediator.Send(new UpdateTechologyCommand()
                {
                    Techology = technologyDto
                }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }

        [HttpPost]
        public async Task<ActionResult<int>> InsertTechnology([FromBody] TechnologyDto technologyDto)
        {
            try
            {
                return Ok(await _mediator.Send(new InsertTechologyCommand()
                {
                    Techology = technologyDto
                }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteTechnology([FromQuery] int id)
        {
            try
            {
                return Ok(await _mediator.Send(new DeleteTechologyCommand()
                {
                    idTechology = id
                }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpDelete]
        [Route("Details")]
        public async Task<ActionResult<bool>> DeleteTechnologyDetails([FromQuery] int id)
        {
            try
            {
                return Ok(await _mediator.Send(new DeleteTechologyDetailCommand()
                {
                    idTechologyDetail = id
                }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet]
        [Route("Details")]
        public async Task<ActionResult<IEnumerable<TechnologyDetailDto>>> TechnologyDetails([FromQuery] SearchParam searchParam)
        {
            try
            {
                return Ok(await _mediator.Send(new GetTechnologyDetailQuery()
                {
                    searchParams = searchParam
                }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPut]
        [Route("Details")]
        public async Task<ActionResult<int>> UpdateTechnologyDetails([FromBody] TechnologyDetailDto technologyDetailDto)
        {
            try
            {
                return Ok(await _mediator.Send(new UpdateTechologyDetailCommand() { TechologyDetail = technologyDetailDto }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPost]
        [Route("Details")]
        public async Task<ActionResult<int>> InsertTechnolgyDetails([FromBody] TechnologyDetailDto technologyDetailDto)
        {
            try
            {
                return Ok(await _mediator.Send(new InsertTechologyDetailCommand() { TechologyDetails = technologyDetailDto }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
