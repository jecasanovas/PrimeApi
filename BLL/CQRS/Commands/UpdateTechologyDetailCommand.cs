using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces.Services;
using Core.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.CQRS.Commands
{
    public record UpdateTechologyDetailCommand : IRequest<int>
    {
        public TechnologyDetailDto TechologyDetail;
    }

    public class UpdateTechologyDetailCommandHandler : IRequestHandler<UpdateTechologyDetailCommand, int>
    {
        public readonly ITechnologyService _techologyService;
        public readonly IMapper _mapper;
        public UpdateTechologyDetailCommandHandler(ITechnologyService course, IMapper mapper)
        {
            _techologyService = course;
            _mapper = mapper;
        }
        public async Task<int> Handle(UpdateTechologyDetailCommand request, CancellationToken cancellationToken)
        {
            return await _techologyService.UpdateTechnologyDetailAsync(_mapper.Map<TechnologyDetail>(request.TechologyDetail), cancellationToken);

        }
    }
}
