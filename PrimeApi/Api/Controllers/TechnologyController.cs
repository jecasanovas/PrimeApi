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

        private readonly ITechnologyService _techologyService;
        private readonly IMediator _mediator;

        public TechnologyController(IMediator mediator, ITechnologyService technologyService)
        {
            _techologyService = technologyService;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<TechnologyDto>>> Technology([FromQuery] SearchParam searchParam)
        {
            return Ok(await _mediator.Send(new GetTechnologyQuery()
            {
                searchParams = searchParam
            }));
        }
        [HttpPut]
        public async Task<ActionResult<int>> UpdateTechnology([FromBody] TechnologyDto technologyDto)
        {
            return Ok(await _mediator.Send(new UpdateTechologyCommand()
            {
                Techology = technologyDto
            }));

        }

        [HttpPost]
        public async Task<ActionResult<int>> InsertTechnology([FromBody] TechnologyDto technologyDto)
        {
            return Ok(await _mediator.Send(new InsertTechologyCommand()
            {
                Techology = technologyDto
            }));

        }

        [HttpDelete]
        public async Task<ActionResult<int>> DeleteTechnology([FromQuery] int id)
        {
            return Ok(await _mediator.Send(new DeleteTechologyCommand()
            {
                idTechology = id
            }));

        }

        [HttpDelete]
        [Route("Details")]
        public async Task<ActionResult<int>> DeleteTechnologyDetails([FromQuery] int id)
        {
            return Ok(await _mediator.Send(new DeleteTechologyDetailCommand()
            {
                idTechologyDetail = id
            }));

        }

        [HttpGet]
        [Route("Details")]
        public async Task<ActionResult<List<TechnologyDetailDto>>> TechnologyDetails([FromQuery] SearchParam searchParam)
        {
            return Ok(await _mediator.Send(new GetTechnologyDetailQuery()
            {
                searchParams = searchParam
            }));
        }

        [HttpPut]
        [Route("Details")]
        public async Task<ActionResult<int>> UpdateTechnologyDetails([FromBody] TechnologyDetailDto technologyDetailDto)
        {
            return Ok(await _mediator.Send(new UpdateTechologyDetailCommand() { TechologyDetail = technologyDetailDto }));
        }

        [HttpPost]
        [Route("Details")]
        public async Task<ActionResult<int>> InsertTechnolgyDetails([FromBody] TechnologyDetail technologyDetailDto)
        {
            return Ok(await _mediator.Send(new InsertTechologyDetailCommand() { TechologyDetails = technologyDetailDto }));
        }

    }
}
