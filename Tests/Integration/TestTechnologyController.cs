using API.Helpers;
using AutoMapper;
using BLL.CQRS.Commands;
using BLL.CQRS.Queries;
using BLL.Dtos;
using BLL.Interfaces;
using BLL.Interfaces.Services;
using BLL.SearchParams;
using Core.Entities;
using Courses.Api.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Tests.Integration;


public class TestTechnologyController
{
    private readonly Mock<ITechnologyService> _technologyServiceMock;
    private readonly TechnologyController _technologyController;

    private readonly Mock<IMediator> _mediatorMock;

    public TestTechnologyController()
    {
        _mediatorMock = new Mock<IMediator>();
        _technologyServiceMock = new Mock<ITechnologyService>();
        _technologyController = new TechnologyController(_mediatorMock.Object, _technologyServiceMock.Object);
    }

    [Fact]
    public async Task ShouldCallGetTechnologyControllerAndRetrieveTechnology()
    {
        var expectedresult = new List<TechnologyDto>()
                        {
                            new() {
                                Id= 1,
                                Description = "TechnologyTest"
                           }

                };


        _mediatorMock.Setup(x => x.Send(It.IsAny<GetTechnologyQuery>(), It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(expectedresult);
        var resultData = await _technologyController.Technology(new SearchParamTeachers()
        {
            page = 1,
            pageSize = 1
        });
        var result = ((IList<TechnologyDto>)((OkObjectResult)resultData.Result!).Value!)[0].Description;
        Assert.Equal("TechnologyTest", result);
    }


    [Fact]
    public async Task ShouldCallTechnologyControllerAndInsertTechnlogy()
    {
        _mediatorMock.Setup(x => x.Send(It.IsAny<InsertTechologyCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(5);
        var resultData = await _technologyController.InsertTechnology(new TechnologyDto());

        Assert.Equal(5, ((OkObjectResult)resultData.Result!).Value);
    }
    [Fact]
    public async Task ShouldCallTechnologyControllerAndUpdateTechnology()
    {
        _mediatorMock.Setup(x => x.Send(It.IsAny<UpdateTechologyCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(5);
        var resultData = await _technologyController.UpdateTechnology(new TechnologyDto());

        Assert.Equal(5, ((OkObjectResult)resultData.Result!).Value);
    }

    [Fact]
    public async Task ShouldCallTechnologyControllerAndDeleteTechnlogy()
    {
        _mediatorMock.Setup(x => x.Send(It.IsAny<DeleteTechologyCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
        var resultData = await _technologyController.DeleteTechnology(5);
        Assert.True((bool)((OkObjectResult)resultData.Result!).Value!);
    }

    [Fact]
    public async Task ShouldCallGetTechnologyControllerAndRetrieveTechnologyDetail()
    {
        var expectedresult = new List<TechnologyDetailDto>()
                        {
                            new() {
                                Id= 1,
                                Description = "TechnologyDetailTest"
                           }

                };


        _mediatorMock.Setup(x => x.Send(It.IsAny<GetTechnologyDetailQuery>(), It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(expectedresult);
        var resultData = await _technologyController.TechnologyDetails(new SearchParam()
        {
            page = 1,
            pageSize = 1
        });
        var result = ((IList<TechnologyDetailDto>)((OkObjectResult)resultData.Result!).Value!)[0].Description;
        Assert.Equal("TechnologyDetailTest", result);
    }


    [Fact]
    public async Task ShouldCallTechnologyControllerAndInsertTechnlogyDetail()
    {
        _mediatorMock.Setup(x => x.Send(It.IsAny<InsertTechologyDetailCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(5);
        var resultData = await _technologyController.InsertTechnolgyDetails(new TechnologyDetailDto());

        Assert.Equal(5, ((OkObjectResult)resultData.Result!).Value);
    }
    [Fact]
    public async Task ShouldCallTechnologyControllerAndUpdateTechnologyDetail()
    {
        _mediatorMock.Setup(x => x.Send(It.IsAny<UpdateTechologyCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(5);
        var resultData = await _technologyController.UpdateTechnology(new TechnologyDto());

        Assert.Equal(5, ((OkObjectResult)resultData.Result!).Value);
    }

    [Fact]
    public async Task ShouldCallTechnologyControllerAndDeleteTechnlogyDetail()
    {
        _mediatorMock.Setup(x => x.Send(It.IsAny<DeleteTechologyCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
        var resultData = await _technologyController.DeleteTechnology(5);
        Assert.True((bool)((OkObjectResult)resultData.Result!).Value!);
    }

}