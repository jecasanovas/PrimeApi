using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces;
using BLL.Models;
using Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.CQRS.Queries
{
    public class CourseQueries : IRequest<DataResults<CourseDto>>
    {
        public SearchParamCourses searchParams;
    }
    
    public class CourseQueriesHandler:IRequestHandler<CourseQueries, DataResults<CourseDto>>
    {
        private ICourseService _courseService;
        private IMapper _mapper;
        public CourseQueriesHandler(ICourseService course, IMapper mapper)
        {   
            _courseService = course;
            _mapper = mapper;   
        }

        public async Task<DataResults<CourseDto>> Handle(CourseQueries request, CancellationToken cancellationToken)
        {
            var courses = await _courseService.GetCourses(request.searchParams);
            //Count results without pagination active, for paging info
            var nrows = await _courseService.GetTotalRowsAsysnc(request.searchParams);

            return new DataResults<CourseDto>()
            {
                Dto = _mapper.Map<IEnumerable<Course>, IEnumerable<CourseDto>>(courses),
                Results = nrows
            };

        }
    }

}
