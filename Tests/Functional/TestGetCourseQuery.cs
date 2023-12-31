using AutoMapper;
using BLL.CQRS.Queries;
using BLL.Dtos;
using BLL.Interfaces;
using BLL.SearchParams;
using Core.Entities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
namespace Testing;
public class GetCourseQueryTests
{
    private readonly Mediator _mediator;


    public GetCourseQueryTests()
    {
        var _expectedCourses = new List<Course>()
        {
                new Course()
                {
                    Id = 1,
                    Name = "Course 1",
                    Description = "Description of Course 1",

                },
             };

        //Mock handles services call
        var courseService = new Mock<ICourseService>();
        var autoMapper = new Mock<IMapper>();
        courseService.Setup(x => x.GetCoursesAsync(It.IsAny<SearchParamCourses>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_expectedCourses);

        autoMapper.Setup(m => m.Map<IEnumerable<CourseDto>>((Course)It.IsAny<IEnumerable<Course>>()))
            .Returns(new List<CourseDto>() {
                    new CourseDto() { Id = 1, Name = "TestResult" }});

        var provider = new ServiceCollection()
                        .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetCourseQuery).Assembly))
                        .AddSingleton(courseService.Object)
                        .AddSingleton(autoMapper.Object).BuildServiceProvider();
        _mediator = new Mediator(provider);

    }

    [Fact]
    public async Task TestQueryAsync()
    {
        // Prepara los datos de prueba
        var searchParameters = new SearchParamCourses
        {
            page = 1,
            pageSize = 1
        };


        // Envia la consulta
        var result = await _mediator.Send(new GetCourseQuery()
        {
            searchParams = searchParameters
        });

    }
}