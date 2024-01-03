using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces.Services;
using Core.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.CQRS.Commands
{
    public record InsertTechologyCommand : IRequest<int>
    {
        public TechnologyDto Techology;
    }

    public class InsertTechologyCommandHandler : IRequestHandler<InsertTechologyCommand, int>
    {
        public readonly ITechnologyService _techologyService;
        public readonly IMapper _mapper;
        public InsertTechologyCommandHandler(ITechnologyService course, IMapper mapper)
        {
            _techologyService = course;
            _mapper = mapper;
        }
        public async Task<int> Handle(InsertTechologyCommand request, CancellationToken cancellationToken)
        {
            return await _techologyService.InsertTechnologyAsync(_mapper.Map<Technology>(request.Techology), cancellationToken);

        }
    }
}
