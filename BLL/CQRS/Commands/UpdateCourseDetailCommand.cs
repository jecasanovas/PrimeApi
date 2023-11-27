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



    public record UpdateCourseDetailCommand : IRequest<bool>
    {
        public CourseDetailDto Course;

    }

    public class UpdateCourseDetailCommandHandler : IRequestHandler<UpdateCourseDetailCommand, bool>
    {

        public readonly ICourseService _courseService;
        public readonly IMapper _mapper;


        public UpdateCourseDetailCommandHandler(ICourseService course, IMapper mapper)
        {
            _courseService = course;
            _mapper = mapper;
        }


        public async Task<bool> Handle(UpdateCourseDetailCommand request, CancellationToken cancellationToken)
        {
            var result = await _courseService.UpdateCourseDetailsAsync(_mapper.Map<CourseDetail>(request.Course), cancellationToken);
            return true;

        }
    }


}
