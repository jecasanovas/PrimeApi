using API.Helpers;
using BLL.CQRS.Queries;
using BLL.Dtos;
using BLL.Interfaces;
using BLL.SearchParams;
using Core.Entities;
using Courses.Api.Controllers;
using MediatR;
using Microsoft.Extensions.Configuration;
using Moq;

namespace CourseAPI.Tests
{
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
        public async Task ShouldCallICourseServiceToRetrieveCourses()
        {
            var result = new DataResults<CourseDto>()
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
            _mediatorMock.Setup(Mediator => Mediator.Send(It.IsAny<GetCourseQuery>(), It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(result);
            var resultData = await _courseController.Course(new SearchParamCourses()
            {
                page = 1,
                pageSize = 1
            });


        }
    }

}