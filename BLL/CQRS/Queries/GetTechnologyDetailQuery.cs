using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces.Services;
using BLL.SearchParams;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.CQRS.Queries
{
    public record GetTechnologyDetailQuery : IRequest<IEnumerable<TechnologyDetailDto>>
    {
        public SearchParam searchParams;
    }

    public class GetTechnologyDetailQueryHandler : IRequestHandler<GetTechnologyDetailQuery, IEnumerable<TechnologyDetailDto>>
    {
        private ITechnologyService _technologyService;
        private IMapper _mapper;
        public GetTechnologyDetailQueryHandler(ITechnologyService technologyService, IMapper mapper)
        {
            _technologyService = technologyService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TechnologyDetailDto>> Handle(GetTechnologyDetailQuery request, CancellationToken cancellationToken)
        {
            var result = await _technologyService.GetTechnologyDetailsAsync(request.searchParams, cancellationToken);
            return _mapper.Map<IEnumerable<TechnologyDetailDto>>(result);
        }
    }

}