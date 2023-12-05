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
    public record UpdateCourseCommand : IRequest<int>
    {
        public CourseDto Course;
    }

    public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, int>
    {
        public readonly ICourseService _courseService;
        public readonly IMapper _mapper;

        public UpdateCourseCommandHandler(ICourseService course, IMapper mapper)
        {
            _courseService = course;
            _mapper = mapper;
        }


        public async Task<int> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            return await _courseService.UpdateCourseAsync(_mapper.Map<Course>(request.Course), cancellationToken);
        }
    }
}
