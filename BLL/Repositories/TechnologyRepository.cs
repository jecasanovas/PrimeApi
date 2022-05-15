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
    public class TechnologyRepository : ITechnologyRepository
    {

        private readonly ITechnologyService _technologyService;
        public readonly IMapper _mapper;

        public TechnologyRepository(ITechnologyService technologyService, IMapper mapper)
        {
            _technologyService = technologyService;
            _mapper = mapper;
        }

        public async Task DeleteTechnology(int id)
        {
            await _technologyService.DeleteTechnology(id);
        }

        public async Task<IEnumerable<TechnologyDto>> GetTechnology(SearchParam searchParam)
        {
            var tecnology = await _technologyService.GetTechnology(searchParam);
            var tecnologydata = _mapper.Map<IEnumerable<Technology>, IEnumerable<TechnologyDto>>(tecnology);
            return tecnologydata; 
        }

        public async Task<int> GetTotalTechnology(SearchParam searchParam)
        {
            return await _technologyService.GetTotalRowsTechnology(searchParam);
        }

        public async Task<int> InsertTechnology(TechnologyDto technologyDto)
        {
            return await _technologyService.InsertTechnology(technologyDto);
        }

        public async Task<int> UpdateTechnology(TechnologyDto technologyDto)
        {
            return await _technologyService.UpdateTechnology(technologyDto);
        }
    }
}
