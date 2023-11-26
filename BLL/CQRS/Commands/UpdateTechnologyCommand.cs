using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces.Services;
using Core.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.CQRS.Commands
{
    public record UpdateTechologyCommand : IRequest<bool>
    {
        public TechnologyDto Techology;
    }

    public class UpdateTechologyCommandHandler : IRequestHandler<UpdateTechologyCommand, bool>
    {
        public readonly ITechnologyService _techologyService;
        public readonly IMapper _mapper;
        public UpdateTechologyCommandHandler(ITechnologyService course, IMapper mapper)
        {
            _techologyService = course;
            _mapper = mapper;
        }
        public async Task<bool> Handle(UpdateTechologyCommand request, CancellationToken cancellationToken)
        {
            var result = await _techologyService.UpdateTechnology(_mapper.Map<Technology>(request.Techology));
            return true;
        }
    }
}
