using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces.Repositories;
using BLL.Interfaces.Services;
using BLL.Models;
using BLL.Parameters;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories
{
    public class TechnologyDetailRepository : ITechnologyDetailRepository
    {

        private readonly ITechnologyService  _technologyService;
        public readonly IMapper _mapper;


        public TechnologyDetailRepository(IMapper mapper, ITechnologyService technologyService)
        {
            _technologyService = technologyService;
            _mapper = mapper;

        }


        public async Task DeleteTechnologyDetail(int id)
        {
            await _technologyService.DeleteTechnologyDetail(id);
        }

        public async Task<IEnumerable<TechnologyDetailsDto>> GetTechnologyDetails(SearchParam searchParam)
        {
            var tecnology = await _technologyService.GetTechnologyDetails(searchParam);
            var tecnologydata =  _mapper.Map<IEnumerable<TechnologyDetails>, IEnumerable<TechnologyDetailsDto>>(tecnology);
            return tecnologydata;

        }

        public async Task<int> GetTotalTechnologysDetails(SearchParam searchParam)
        {
            return await _technologyService.GetTotalRowsTechnology(searchParam);
        }

        public async Task<int> InsertTechnologyDetail(TechnologyDetailsDto technologyDetails)
        {
            return await _technologyService.InsertTechnologyDetail(technologyDetails);
        }

        public async Task<int> UpdateTechnologyDetail(TechnologyDetailsDto technologyDetails)
        {
            return await _technologyService.UpdateTechnologyDetail(technologyDetails);
            
        }
    }
}
