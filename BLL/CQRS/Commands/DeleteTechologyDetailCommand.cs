using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces.Services;
using Core.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.CQRS.Commands
{
    public record DeleteTechologyDetailCommand : IRequest<bool>
    {
        public int idTechologyDetail;
    }

    public class DeleteTechologyDetailsCommandHandler : IRequestHandler<DeleteTechologyDetailCommand, bool>
    {
        public readonly ITechnologyService _techologyService;
        public readonly IMapper _mapper;
        public DeleteTechologyDetailsCommandHandler(ITechnologyService course, IMapper mapper)
        {
            _techologyService = course;
            _mapper = mapper;
        }
        public async Task<bool> Handle(DeleteTechologyDetailCommand request, CancellationToken cancellationToken)
        {
            await _techologyService.DeleteTechnologyDetailAsync(request.idTechologyDetail, cancellationToken);
            return true;
        }
    }
}
