using BLL.Dtos;
using BLL.Interfaces.Repositories;
using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace Courses.Api.Controllers
{
    public class TechnologyController : BaseApiController
    {

        private readonly ITechnologyRepository _technologyRepository;
        private readonly ITechnologyDetailRepository _technologyDetailsRepository;

        public TechnologyController(ITechnologyRepository technologyRepository, ITechnologyDetailRepository technologyDetailsRepository)
        {
            _technologyDetailsRepository = technologyDetailsRepository;
            _technologyRepository = technologyRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<TechnologyDto>>> Technology([FromQuery] SearchParam searchParam)
        {
            return Ok(await _technologyRepository.GetTechnology(searchParam));
        }
        [HttpPut]
        public async Task<ActionResult<int>> UpdateTechnology([FromBody] TechnologyDto technologyDto)
        {
            return Ok(await _technologyRepository.UpdateTechnology(technologyDto));


        }

        [HttpPost]
        public async Task<ActionResult<int>> InsertTechnology([FromBody] TechnologyDto technologyDto)
        {
            return Ok(await _technologyRepository.InsertTechnology(technologyDto));

        }

        [HttpDelete]
        public async Task<ActionResult<int>> DeleteTechnology([FromQuery] int id)
        {
            await _technologyRepository.DeleteTechnology(id);
            return Ok();

        }

        [HttpDelete]
        [Route("Details")]
        public async Task<ActionResult<int>> DeleteTechnologyDetails([FromQuery] int id)
        {
            await _technologyDetailsRepository.DeleteTechnologyDetail(id);
            return Ok();

        }

        [HttpGet]
        [Route("Details")]
        public async Task<ActionResult<List<TechnologyDetailsDto>>> TechnologyDetails([FromQuery] SearchParam searchParam)
        {
            return Ok(await _technologyDetailsRepository.GetTechnologyDetails(searchParam));
        }

        [HttpPut]
        [Route("Details")]
        public async Task<ActionResult<int>> UpdateTechnologyDetails([FromBody] TechnologyDetailsDto technologyDetailsDto)
        {
            return Ok(await _technologyDetailsRepository.UpdateTechnologyDetail(technologyDetailsDto));
        }

        [HttpPost]
        [Route("Details")]
        public async Task<ActionResult<int>> InsertTechnolgyDetails([FromBody] TechnologyDetailsDto technologyDetailsDto)
        {
            return Ok(await _technologyDetailsRepository.InsertTechnologyDetail(technologyDetailsDto));
        }

    }
}
