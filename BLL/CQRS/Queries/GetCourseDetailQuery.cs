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
    public record GetCourseDetailQuery : IRequest<DataResults<CourseDetailDto>>
    {
        public SearchParamCourses searchParams;
    }

    public class GetCourseDetailQueryHandler : IRequestHandler<GetCourseDetailQuery, DataResults<CourseDetailDto>>
    {
        private ICourseService _courseService;
        private IMapper _mapper;
        public GetCourseDetailQueryHandler(ICourseService course, IMapper mapper)
        {
            _courseService = course;
            _mapper = mapper;
        }

        public async Task<DataResults<CourseDetailDto>> Handle(GetCourseDetailQuery request, CancellationToken cancellationToken)
        {
            var courses = await _courseService.GetCourseDetails(request.searchParams);
            //Count results without pagination active, for paging info
            var nrows = await _courseService.GetTotalDetailRowsAsysnc(request.searchParams);

            return new DataResults<CourseDetailDto>()
            {
                Dto = _mapper.Map<IEnumerable<CourseDetailDto>>(courses),
                Results = nrows
            };

        }
    }

}
