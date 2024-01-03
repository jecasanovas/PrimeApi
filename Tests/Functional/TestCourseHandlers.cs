using AutoMapper;
using BLL.CQRS.Commands;
using BLL.CQRS.Queries;
using BLL.Dtos;
using BLL.Interfaces;
using BLL.SearchParams;
using BLL.Services;
using Core.Entities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
namespace Tests.Functional;
public class TestCourseHandlers
{
    private readonly Mediator _mediator;
    private readonly Mock<ICourseService> _courseService;
    private readonly Mock<IMapper> _autoMapper;
    public TestCourseHandlers()
    {

        _courseService = new Mock<ICourseService>();
        _autoMapper = new Mock<IMapper>();

        var provider = new ServiceCollection()
                        .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetCourseQuery).Assembly))
                        .AddSingleton(_courseService.Object)
                        .AddSingleton(_autoMapper.Object).BuildServiceProvider();
        _mediator = new Mediator(provider);

    }

    [Fact]
    public async Task ShouldCallGetCourseHandlerAndRetrieveCourses()
    {

        _autoMapper.Setup(m => m.Map<IEnumerable<CourseDto>>((Course)It.IsAny<IEnumerable<Course>>()))
            .Returns(new List<CourseDto>() {
                    new CourseDto() { Id = 1, Name = "TestResult" }});
        _courseService.Setup(x => x.GetCoursesAsync(It.IsAny<SearchParamCourses>(), It.IsAny<CancellationToken>()));

        var result = await _mediator.Send(new GetCourseQuery());
        Assert.Equal("TestResult", result.Dto.ElementAt(0).Name);

    }
    [Fact]
    public async Task ShouldCallInsertCourseCommandHandler()
    {
        _courseService.Setup(x => x.InsertCourseAsync(It.IsAny<Course>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(5));
        var result = await _mediator.Send(new InsertCourseCommand(), CancellationToken.None);
        Assert.Equal(5, result);
    }

    [Fact]
    public async Task ShouldCallUpdateCourseCommandHandler()
    {
        _courseService.Setup(X => X.UpdateCourseAsync(It.IsAny<Course>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(5));
        var result = await _mediator.Send(new UpdateCourseCommand());
        Assert.Equal(5, result);
    }
    [Fact]
    public async Task ShouldCallDeleteCourseCommandHandler()
    {
        _courseService.Setup(x => x.DeleteCourseAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(true));
        var result = await _mediator.Send(new DeleteCourseCommand());
        Assert.True(result);
    }

    [Fact]
    public async Task ShouldCallGetCourseDetailHandlerAndRetrieveCourseDetail()
    {
        _autoMapper.Setup(m => m.Map<IEnumerable<CourseDetailDto>>((Course)It.IsAny<IEnumerable<CourseDetail>>()))
            .Returns(new List<CourseDetailDto>() {
                    new CourseDetailDto() { Id = 1, Description = "TestResult" }});
        _courseService.Setup(x => x.GetCourseDetailsAsync(It.IsAny<SearchParamCourses>(), It.IsAny<CancellationToken>()));


        var result = await _mediator.Send(new GetCourseDetailQuery());
        Assert.Equal("TestResult", result.Dto.ElementAt(0).Description);
    }
    [Fact]
    public async Task ShouldCallInsertCourseDetailCommandHandler()
    {
        _courseService.Setup(x => x.InsertCourseDetailAsync(It.IsAny<CourseDetail>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(5));
        var result = await _mediator.Send(new InsertCourseDetailCommand(), CancellationToken.None);
        Assert.Equal(5, result);
    }

    [Fact]
    public async Task ShouldCallUpdateCourseDetailCommandHandler()
    {
        _courseService.Setup(X => X.UpdateCourseDetailsAsync(It.IsAny<CourseDetail>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(5));
        var result = await _mediator.Send(new UpdateCourseDetailCommand(), CancellationToken.None);
        Assert.Equal(5, result);
    }
    [Fact]
    public async Task ShouldCallDeleteCourseDetailCommandHandler()
    {
        _courseService.Setup(x => x.DeleteCourseAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(true));
        var result = await _mediator.Send(new DeleteCourseCommand());
        Assert.True(result);
    }

}