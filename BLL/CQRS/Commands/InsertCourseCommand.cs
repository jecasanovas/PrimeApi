using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces;
using Core.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.CQRS.Commands
{
    public record InsertCourseCommand : IRequest<int>
    {
        public CourseDto Course;
    }

    public class InsertCourseCommandHandler : IRequestHandler<InsertCourseCommand, int>
    {

        public readonly ICourseService _courseService;
        public readonly IMapper _mapper;
        public InsertCourseCommandHandler(ICourseService courseService, IMapper mapper)
        {
            _courseService = courseService;
            _mapper = mapper;
        }

        public async Task<int> Handle(InsertCourseCommand request, CancellationToken cancellationToken)
        {
            return await _courseService.InsertCourseAsync(_mapper.Map<Course>(request.Course), cancellationToken);

        }
    }
}
