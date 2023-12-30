using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces;
using BLL.SearchParams;
using Core.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.CQRS.Queries
{
    public record GetCourseQuery : IRequest<DataResults<CourseDto>>
    {
        public SearchParamCourses searchParams;
    }

    public class GetCourseQueryHandler : IRequestHandler<GetCourseQuery, DataResults<CourseDto>>
    {
        private ICourseService _courseService;
        private IMapper _mapper;
        public GetCourseQueryHandler(ICourseService course, IMapper mapper)
        {
            _courseService = course;
            _mapper = mapper;
        }

        public async Task<DataResults<CourseDto>> Handle(GetCourseQuery request, CancellationToken cancellationToken)
        {
            var courses = await _courseService.GetCoursesAsync(request.searchParams, cancellationToken);
            //Count results without pagination active, for paging info
            var nrows = await _courseService.GetTotalRowsAsysnc(request.searchParams, cancellationToken);
            var test = _mapper.Map<IEnumerable<CourseDto>>(courses);
            return new DataResults<CourseDto>()
            {
                Dto = _mapper.Map<IEnumerable<CourseDto>>(courses),
                Results = nrows
            };

        }
    }

}
