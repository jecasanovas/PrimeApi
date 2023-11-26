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



    public record UpdateCourseCommand : IRequest<bool>
    {
        public CourseDto Course;
    }

    public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, bool>
    {

        public readonly ICourseService _courseService;
        public readonly IMapper _mapper;


        public UpdateCourseCommandHandler(ICourseService course, IMapper mapper)
        {
            _courseService = course;
            _mapper = mapper;
        }


        public async Task<bool> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var result = await _courseService.UpdateCourse(_mapper.Map<Course>(request.Course));
            return true;

        }
    }


}
