using API.Helpers;
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


public class CourseControllerTests
{
    private readonly Mock<ICourseService> _courseServiceMock;
    private readonly CourseController _courseController;

    private readonly Mock<IMediator> _mediatorMock;

    private readonly Mock<IConfiguration> _configurationMock;


    public CourseControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _courseServiceMock = new Mock<ICourseService>();
        _configurationMock = new Mock<IConfiguration>();
        _courseController = new CourseController(_courseServiceMock.Object, _mediatorMock.Object, _configurationMock.Object);
    }

    [Fact]
    public async Task ShouldCallGetCourseControllerAndRetrieveCourses()
    {
        var expectedresult = new DataResults<CourseDto>()
        {
            Results = 1,
            Dto = new List<CourseDto>()
                        {
                            new CourseDto() {
                                Id= 1,
                                Name = "CourseMock"
                           }

                }
        };
        _mediatorMock.Setup(Mediator => Mediator.Send(It.IsAny<GetCourseQuery>(), It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(expectedresult);
        var resultData = await _courseController.Course(new SearchParamCourses()
        {
            page = 1,
            pageSize = 1
        });
        var result = ((Paginator<CourseDto>)((OkObjectResult)resultData.Result!).Value!).Data[0].Name;

        Assert.Equal("CourseMock", result);
    }


    [Fact]
    public async Task ShouldCallCourseControllerAndInsertCourse()
    {
        _mediatorMock.Setup(x => x.Send(It.IsAny<InsertCourseCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(5);
        var resultData = await _courseController.InsertCourse(new CourseDto());

        Assert.Equal(5, (int)((OkObjectResult)resultData.Result!).Value!);
    }
    [Fact]
    public async Task ShouldCallCourseControllerAndUpdateCourse()
    {
        _mediatorMock.Setup(x => x.Send(It.IsAny<UpdateCourseCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(5);
        var resultData = await _courseController.UpdateCourse(new CourseDto());

        Assert.Equal(5, (int)((OkObjectResult)resultData.Result!).Value!);
    }

    [Fact]
    public async Task ShouldCallCourseControllerAndDeleteCourse()
    {
        _mediatorMock.Setup(X => X.Send(It.IsAny<DeleteCourseCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
        var resultData = await _courseController.DeleteCourse(5);
        Assert.True((bool)((OkObjectResult)resultData.Result!).Value!);

    }


    [Fact]
    public async Task ShouldCallGetCourseControllerAndRetrieveCourseDetail()
    {
        var expectedresult = new DataResults<CourseDetailDto>()
        {
            Results = 1,
            Dto = new List<CourseDetailDto>()
                        {
                            new CourseDetailDto() {
                                Id= 1,
                                Description = "CourseDetailTest"
                           }

                }
        };
        _mediatorMock.Setup(x => x.Send(It.IsAny<GetCourseDetailQuery>(), It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(expectedresult);
        var resultData = await _courseController.CourseDetails(new SearchParamCourses()
        {
            page = 1,
            pageSize = 1
        });
        var result = ((Paginator<CourseDetailDto>)((OkObjectResult)resultData.Result!).Value!).Data[0].Description;

        Assert.Equal("CourseDetailTest", result);
    }


    [Fact]
    public async Task ShouldCallCourseControllerAndInsertCourseDetail()
    {
        _mediatorMock.Setup(x => x.Send(It.IsAny<InsertCourseDetailCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(5);
        var resultData = await _courseController.InsertCourseDetail(new CourseDetailDto());

        Assert.Equal(5, (int)((OkObjectResult)resultData.Result!).Value!);
    }
    [Fact]
    public async Task ShouldCallCourseControllerAndUpdateCourseDetail()
    {
        _mediatorMock.Setup(x => x.Send(It.IsAny<UpdateCourseDetailCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(5);
        var resultData = await _courseController.UpdateCourseDetail(new CourseDetailDto());
        Assert.Equal(5, (int)((OkObjectResult)resultData.Result!).Value!);
    }

    /* [Fact]
     public async Task ShouldCallCourseControllerAndDeleteCourseDetail()
     {
         _mediatorMock.Setup(X => X.Send(It.IsAny<DeleteCourseDetailCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
         var resultData = await _courseController.del(5);
         Assert.True(resultData.Value);

     }*/
}



