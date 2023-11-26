using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces.Services;
using Core.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.CQRS.Commands
{
    public record InsertTechologyCommand : IRequest<bool>
    {
        public TechnologyDto Techology;
    }

    public class InsertTechologyCommandHandler : IRequestHandler<InsertTechologyCommand, bool>
    {
        public readonly ITechnologyService _techologyService;
        public readonly IMapper _mapper;
        public InsertTechologyCommandHandler(ITechnologyService course, IMapper mapper)
        {
            _techologyService = course;
            _mapper = mapper;
        }
        public async Task<bool> Handle(InsertTechologyCommand request, CancellationToken cancellationToken)
        {
            var result = await _techologyService.InsertTechnology(_mapper.Map<Technology>(request.Techology));
            return true;
        }
    }
}
