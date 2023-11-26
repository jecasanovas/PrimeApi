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



    public record InsertCourseDetailMasiveCommand : IRequest<bool>
    {
        public IEnumerable<CourseDetailDto> Course;
    }

    public class InsertCourseDetailMasiveCommandHandler : IRequestHandler<InsertCourseDetailMasiveCommand, bool>
    {

        public readonly ICourseService _courseService;
        public readonly IMapper _mapper;


        public InsertCourseDetailMasiveCommandHandler(ICourseService courseService, IMapper mapper)
        {
            _courseService = courseService;
            _mapper = mapper;
        }


        public async Task<bool> Handle(InsertCourseDetailMasiveCommand request, CancellationToken cancellationToken)
        {
            var result = await _courseService.InsertCourseDetailsMasive(_mapper.Map<IEnumerable<CourseDetail>>(request.Course));
            return true;

        }
    }
}
