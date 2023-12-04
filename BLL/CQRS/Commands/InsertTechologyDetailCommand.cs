using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces.Services;
using Core.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.CQRS.Commands
{
    public record InsertTechologyDetailCommand : IRequest<int>
    {
        public TechnologyDetail TechologyDetails;
    }

    public class InsertTechologyDetailCommandHandler : IRequestHandler<InsertTechologyDetailCommand, int>
    {
        public readonly ITechnologyService _techologyService;
        public readonly IMapper _mapper;
        public InsertTechologyDetailCommandHandler(ITechnologyService course, IMapper mapper)
        {
            _techologyService = course;
            _mapper = mapper;
        }
        public async Task<int> Handle(InsertTechologyDetailCommand request, CancellationToken cancellationToken)
        {
            return await _techologyService.InsertTechnologyDetailAsync(_mapper.Map<TechnologyDetail>(request.TechologyDetails), cancellationToken);

        }
    }
}
