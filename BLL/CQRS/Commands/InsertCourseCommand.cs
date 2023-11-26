using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces;
using Core.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.CQRS.Commands
{



    public record InsertCourseCommand : IRequest<bool>
    {
        public CourseDto Course;
    }

    public class InsertCourseCommandHandler : IRequestHandler<InsertCourseCommand, bool>
    {

        public readonly ICourseService _courseService;
        public readonly IMapper _mapper;


        public InsertCourseCommandHandler(ICourseService courseService, IMapper mapper)
        {
            _courseService = courseService;
            _mapper = mapper;
        }


        public async Task<bool> Handle(InsertCourseCommand request, CancellationToken cancellationToken)
        {
            var result = await _courseService.InsertCourse(_mapper.Map<Course>(request.Course));
            return true;

        }
    }


}
