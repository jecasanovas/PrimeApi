using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces;
using Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.CQRS.Commands
{



    public record InsertCourseDetailCommand : IRequest<bool>
    {
        public CourseDetailDto Course;
    }

    public class InsertCourseDetailCommandHandler : IRequestHandler<InsertCourseDetailCommand, bool>
    {

        public readonly ICourseService _courseService;
        public readonly IMapper _mapper;


        public InsertCourseDetailCommandHandler(ICourseService courseService, IMapper mapper)
        {
            _courseService = courseService;
            _mapper = mapper;
        }


        public async Task<bool> Handle(InsertCourseDetailCommand request, CancellationToken cancellationToken)
        {
            var result = await _courseService.InsertCourseDetailAsync(_mapper.Map<CourseDetail>(request.Course), cancellationToken);
            return true;

        }
    }
}
