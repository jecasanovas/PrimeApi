using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces;
using Core.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.CQRS.Commands
{

    public record InsertCourseDetailCommand : IRequest<int>
    {
        public CourseDetailDto Course;
    }

    public class InsertCourseDetailCommandHandler : IRequestHandler<InsertCourseDetailCommand, int>
    {

        public readonly ICourseService _courseService;
        public readonly IMapper _mapper;


        public InsertCourseDetailCommandHandler(ICourseService courseService, IMapper mapper)
        {
            _courseService = courseService;
            _mapper = mapper;
        }


        public async Task<int> Handle(InsertCourseDetailCommand request, CancellationToken cancellationToken)
        {
            return await _courseService.InsertCourseDetailAsync(_mapper.Map<CourseDetail>(request.Course), cancellationToken);
        }
    }
}
