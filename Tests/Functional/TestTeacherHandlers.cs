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
public class TestTeacherHandlers
{
    private readonly Mediator _mediator;
    private readonly Mock<ITeacherService> _teacherService;
    private readonly Mock<IMapper> _autoMapper;
    public TestTeacherHandlers()
    {

        _teacherService = new Mock<ITeacherService>();
        _autoMapper = new Mock<IMapper>();

        var provider = new ServiceCollection()
                        .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetCourseQuery).Assembly))
                        .AddSingleton(_teacherService.Object)
                        .AddSingleton(_autoMapper.Object).BuildServiceProvider();
        _mediator = new Mediator(provider);

    }

    [Fact]
    public async Task ShouldCallGetTeacherHandlerAndRetrieveTeachers()
    {


        _autoMapper.Setup(m => m.Map<IEnumerable<TeacherDto>>((Teacher)It.IsAny<IEnumerable<Teacher>>()))
            .Returns(new List<TeacherDto>() {
                    new TeacherDto() { Id = 1, Name = "TestResult" }});
        _teacherService.Setup(x => x.GetTeachersAsync(It.IsAny<SearchParamTeachers>(), It.IsAny<CancellationToken>()));


        var result = await _mediator.Send(new GetTeacherQuery());
        Assert.Equal("TestResult", result.Dto.ElementAt(0).Name);

    }
    [Fact]
    public async Task ShouldCallInsertTeacherCommandHandler()
    {
        _teacherService.Setup(x => x.InsertTeacherAsync(It.IsAny<Teacher>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(1));
        var result = await _mediator.Send(new InsertTeacherCommand(), CancellationToken.None);
        Assert.Equal(1, result);
    }

    [Fact]
    public async Task ShouldCallUpdateTeacherCommandHandler()
    {
        _teacherService.Setup(x => x.UpdateTeacherAsync(It.IsAny<Teacher>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(1));
        var result = await _mediator.Send(new UpdateTeacherCommand());
        Assert.Equal(1, result);
    }
    [Fact]
    public async Task ShouldCallDeleteTeacherCommandHandler()
    {
        _teacherService.Setup(x => x.DeleteTeacherAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(true));
        var result = await _mediator.Send(new DeleteTeacherCommand());
        Assert.True(result);
    }
}

