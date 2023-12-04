using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces;
using Core.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.CQRS.Commands
{
    public record InsertTeacherCommand : IRequest<int>
    {
        public TeacherDto Teacher;
    }

    public class InsertTeacherCommandHandler : IRequestHandler<InsertTeacherCommand, int>
    {
        public readonly ITeacherService _teacherService;
        public readonly IMapper _mapper;
        public InsertTeacherCommandHandler(ITeacherService teacherService, IMapper mapper)
        {
            _teacherService = teacherService;
            _mapper = mapper;
        }
        public async Task<int> Handle(InsertTeacherCommand request, CancellationToken cancellationToken)
        {
            return await _teacherService.InsertTeacherAsync(_mapper.Map<Teacher>(request.Teacher), cancellationToken);
        }
    }
}
