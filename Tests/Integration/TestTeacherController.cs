using API.Helpers;
using AutoMapper;
using BLL.CQRS.Commands;
using BLL.CQRS.Queries;
using BLL.Dtos;
using BLL.Interfaces;
using BLL.SearchParams;
using Courses.Api.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Tests.Integration;


public class TestTeacherController
{
    private readonly Mock<ITeacherService> _techerServiceMock;
    private readonly TeacherController _teacherController;

    private readonly Mock<IMediator> _mediatorMock;

    private readonly Mock<IMapper> _mapperMock;

    public TestTeacherController()
    {
        _mediatorMock = new Mock<IMediator>();
        _mapperMock = new Mock<IMapper>();
        _techerServiceMock = new Mock<ITeacherService>();
        _teacherController = new TeacherController(_mapperMock.Object, _mediatorMock.Object, _techerServiceMock.Object);
    }

    [Fact]
    public async Task ShouldCallGetTeacherControllerAndRetrieveTeachers()
    {
        var expectedresult = new DataResults<TeacherDto>()
        {
            Results = 1,
            Dto = new List<TeacherDto>()
                        {
                            new() {
                                Id= 1,
                                Name = "TeacherTest"
                           }

                }
        };
        _mediatorMock.Setup(Mediator => Mediator.Send(It.IsAny<GetTeacherQuery>(), It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(expectedresult);
        var resultData = await _teacherController.Teacher(new SearchParamTeachers()
        {
            page = 1,
            pageSize = 1
        });

        var result = resultData.Value!.Data[0].Name;
        Assert.Equal("TeacherTest", result);
    }


    [Fact]
    public async Task ShouldCallTeacherControllerAndInsertTeacher()
    {
        _mediatorMock.Setup(x => x.Send(It.IsAny<InsertTeacherCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(5);
        var resultData = await _teacherController.CreateTeacher(new TeacherDto());

        Assert.Equal(5, (int)((OkObjectResult)resultData.Result!).Value!);
    }
    [Fact]
    public async Task ShouldCallTeacherControllerAndUpdateTeacher()
    {
        _mediatorMock.Setup(x => x.Send(It.IsAny<UpdateTeacherCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(5);
        var resultData = await _teacherController.UpdateTeacher(new TeacherDto());

        Assert.Equal(5, (int)((OkObjectResult)resultData.Result!).Value!);
    }

    [Fact]
    public async Task ShouldCallTeacherControllerAndDeleteTeacher()
    {
        _mediatorMock.Setup(X => X.Send(It.IsAny<DeleteTeacherCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
        var resultData = await _teacherController.DeleteTeacher(5);
        Assert.True((bool)((OkObjectResult)resultData.Result!).Value!);
    }
}