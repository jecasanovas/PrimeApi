using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces;
using Core.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.CQRS.Commands
{
    public record UpdateCourseDetailCommand : IRequest<int>
    {
        public CourseDetailDto Course;

    }

    public class UpdateCourseDetailCommandHandler : IRequestHandler<UpdateCourseDetailCommand, int>
    {

        public readonly ICourseService _courseService;
        public readonly IMapper _mapper;


        public UpdateCourseDetailCommandHandler(ICourseService course, IMapper mapper)
        {
            _courseService = course;
            _mapper = mapper;
        }


        public async Task<int> Handle(UpdateCourseDetailCommand request, CancellationToken cancellationToken)
        {
            return await _courseService.UpdateCourseDetailsAsync(_mapper.Map<CourseDetail>(request.Course), cancellationToken);
        }
    }

}
