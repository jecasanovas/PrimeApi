using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces.Services;
using Core.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.CQRS.Commands
{
    public record InsertTechologyDetailCommand : IRequest<bool>
    {
        public TechnologyDetail TechologyDetails;
    }

    public class InsertTechologyDetailCommandHandler : IRequestHandler<InsertTechologyDetailCommand, bool>
    {
        public readonly ITechnologyService _techologyService;
        public readonly IMapper _mapper;
        public InsertTechologyDetailCommandHandler(ITechnologyService course, IMapper mapper)
        {
            _techologyService = course;
            _mapper = mapper;
        }
        public async Task<bool> Handle(InsertTechologyDetailCommand request, CancellationToken cancellationToken)
        {
            var result = await _techologyService.InsertTechnologyAsync(_mapper.Map<Technology>(request.TechologyDetails), cancellationToken);
            return true;
        }
    }
}
