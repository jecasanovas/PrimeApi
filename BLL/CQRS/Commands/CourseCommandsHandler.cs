using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces;
using Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.CQRS.Commands
{
    public enum Operation
    {
        InsertUpdate = 1,
        Delete = 2,
    }


    public  class CourseCommand: IRequest<CourseDto>
    {
        public CourseDto Course;
        public Operation OperationDB;
    }

    public class CourseCommandHandler : IRequestHandler<CourseCommand, CourseDto>
    {

        public readonly ICourseService _courseService;
        public readonly IMapper _mapper; 
       

        public CourseCommandHandler(ICourseService course, IMapper mapper)
        {
            _courseService = course;
            _mapper = mapper;
        }


        public async Task<CourseDto> Handle(CourseCommand request, CancellationToken cancellationToken)
        {
            switch (request.OperationDB)
            {
                case Operation.InsertUpdate:
                    //Need to return the info for automatic id in frontend
                    var affectedCourse = request.Course.Id == 0 ? await _courseService.InsertCourse(_mapper.Map<Course>(request.Course)) :
                                             await _courseService.UpdateCourse(_mapper.Map<Course>(request.Course));

                    return _mapper.Map<CourseDto>(affectedCourse);
                    

                case Operation.Delete:
                    _ = _courseService.DeleteCourse(request.Course.Id);
                    return request.Course;

                default:
                    return request.Course;

            }
        }
    }
}
