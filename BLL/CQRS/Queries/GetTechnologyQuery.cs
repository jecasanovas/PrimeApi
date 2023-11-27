using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces;
using BLL.Interfaces.Services;
using BLL.Models;
using BLL.SearchParams;
using Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.CQRS.Queries
{
    public record GetTechnologyQuery : IRequest<IEnumerable<TechnologyDto>>
    {
        public SearchParam searchParams;
    }

    public class GetTechnologyQueryHandler : IRequestHandler<GetTechnologyQuery, IEnumerable<TechnologyDto>>
    {
        private ITechnologyService _technologyService;
        private IMapper _mapper;
        public GetTechnologyQueryHandler(ITechnologyService technologyService, IMapper mapper)
        {
            _technologyService = technologyService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TechnologyDto>> Handle(GetTechnologyQuery request, CancellationToken cancellationToken)
        {
            var result = await _technologyService.GetTechnologyAsync(request.searchParams, cancellationToken);
            return _mapper.Map<IEnumerable<TechnologyDto>>(result);
        }
    }

}