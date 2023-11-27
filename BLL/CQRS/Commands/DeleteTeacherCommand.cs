using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.CQRS.Commands
{
    public record DeleteTeacherCommand : IRequest<bool>
    {
        public int TeacherId;
    }

    public class DeleteTeacherCommandHandler : IRequestHandler<DeleteTeacherCommand, bool>
    {
        public readonly ITeacherService _teacherService;
        public readonly IMapper _mapper;
        public DeleteTeacherCommandHandler(ITeacherService teacherService, IMapper mapper)
        {
            _teacherService = teacherService;
            _mapper = mapper;
        }
        public async Task<bool> Handle(DeleteTeacherCommand request, CancellationToken cancellationToken)
        {
            var result = await _teacherService.DeleteTeacherAsync(request.TeacherId, cancellationToken);
            return true;
        }
    }
}
