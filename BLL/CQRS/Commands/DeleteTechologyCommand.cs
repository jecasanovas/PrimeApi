using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces.Services;
using Core.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.CQRS.Commands
{
    public record DeleteTechologyCommand : IRequest<bool>
    {
        public int idTechology;
    }

    public class DeleteTechologyCommandHandler : IRequestHandler<DeleteTechologyCommand, bool>
    {
        public readonly ITechnologyService _techologyService;
        public readonly IMapper _mapper;
        public DeleteTechologyCommandHandler(ITechnologyService course, IMapper mapper)
        {
            _techologyService = course;
            _mapper = mapper;
        }
        public async Task<bool> Handle(DeleteTechologyCommand request, CancellationToken cancellationToken)
        {
            await _techologyService.DeleteTechnology(request.idTechology);
            return true;
        }
    }
}
