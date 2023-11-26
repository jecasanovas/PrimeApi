using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces;
using BLL.Interfaces.Services;
using Core.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.CQRS.Commands
{
    public record UpdateTeacherCommand : IRequest<bool>
    {
        public TeacherDto Teacher;
    }

    public class UpdateTeacherCommandHandler : IRequestHandler<UpdateTeacherCommand, bool>
    {
        public readonly ITeacherService _teacherService;
        public readonly IMapper _mapper;
        public UpdateTeacherCommandHandler(ITeacherService teacherService, IMapper mapper)
        {
            _teacherService = teacherService;
            _mapper = mapper;
        }
        public async Task<bool> Handle(UpdateTeacherCommand request, CancellationToken cancellationToken)
        {
            var result = await _teacherService.UpdateTeacher(_mapper.Map<Teacher>(request.Teacher));
            return true;
        }
    }
}
