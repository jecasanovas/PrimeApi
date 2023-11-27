using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces;
using Core.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.CQRS.Commands
{
    public record InsertTeacherCommand : IRequest<bool>
    {
        public TeacherDto Teacher;
    }

    public class InsertTeacherCommandHandler : IRequestHandler<InsertTeacherCommand, bool>
    {
        public readonly ITeacherService _teacherService;
        public readonly IMapper _mapper;
        public InsertTeacherCommandHandler(ITeacherService teacherService, IMapper mapper)
        {
            _teacherService = teacherService;
            _mapper = mapper;
        }
        public async Task<bool> Handle(InsertTeacherCommand request, CancellationToken cancellationToken)
        {
            var result = await _teacherService.InsertTeacherAsync(_mapper.Map<Teacher>(request.Teacher), cancellationToken);
            return true;
        }
    }
}
