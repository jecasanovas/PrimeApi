using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces.Services;
using Core.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.CQRS.Commands
{
    public record UpdateTechologyCommand : IRequest<int>
    {
        public TechnologyDto Techology;
    }

    public class UpdateTechologyCommandHandler : IRequestHandler<UpdateTechologyCommand, int>
    {
        public readonly ITechnologyService _techologyService;
        public readonly IMapper _mapper;
        public UpdateTechologyCommandHandler(ITechnologyService course, IMapper mapper)
        {
            _techologyService = course;
            _mapper = mapper;
        }
        public async Task<int> Handle(UpdateTechologyCommand request, CancellationToken cancellationToken)
        {
            return await _techologyService.UpdateTechnologyAsync(_mapper.Map<Technology>(request.Techology), cancellationToken);
        }
    }
}
