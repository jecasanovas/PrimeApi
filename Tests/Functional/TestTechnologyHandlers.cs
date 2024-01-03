using AutoMapper;
using BLL.CQRS.Commands;
using BLL.CQRS.Queries;
using BLL.Dtos;
using BLL.Interfaces.Services;
using BLL.SearchParams;
using Core.Entities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
namespace Tests.Functional;

public class TestTechnologyHandlers
{
    private readonly Mediator _mediator;
    private readonly Mock<ITechnologyService> _technologyService;
    private readonly Mock<IMapper> _autoMapper;
    public TestTechnologyHandlers()
    {

        _technologyService = new Mock<ITechnologyService>();
        _autoMapper = new Mock<IMapper>();

        var provider = new ServiceCollection()
                        .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetCourseQuery).Assembly))
                        .AddSingleton(_technologyService.Object)
                        .AddSingleton(_autoMapper.Object).BuildServiceProvider();
        _mediator = new Mediator(provider);

    }

    [Fact]
    public async Task ShouldCallGetTechnologyHandlerAndRetrieveTechnologies()
    {


        _autoMapper.Setup(m => m.Map<IEnumerable<TechnologyDto>>((Technology)It.IsAny<IEnumerable<Technology>>()))
            .Returns(new List<TechnologyDto>() {
                    new TechnologyDto() { Id = 1, Description="TechnologyTest" }});
        _technologyService.Setup(x => x.GetTechnologyAsync(It.IsAny<SearchParam>(), It.IsAny<CancellationToken>()));


        var result = await _mediator.Send(new GetTechnologyQuery());
        Assert.Equal("TechnologyTest", result.First().Description);

    }
    [Fact]
    public async Task ShouldCallInsertTechnologyCommandHandler()
    {
        _technologyService.Setup(x => x.InsertTechnologyAsync(It.IsAny<Technology>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(5));
        var result = await _mediator.Send(new InsertTechologyCommand(), CancellationToken.None);
        Assert.Equal(5, result);
    }

    [Fact]
    public async Task ShouldCallUpdateTechnologyCommandHandler()
    {
        _technologyService.Setup(x => x.UpdateTechnologyAsync(It.IsAny<Technology>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(5));
        var result = await _mediator.Send(new UpdateTechologyCommand());
        Assert.Equal(5, result);
    }
    [Fact]
    public async Task ShouldCallDeleteTechnologyCommandHandler()
    {
        _technologyService.Setup(x => x.DeleteTechnologyAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(true));
        var result = await _mediator.Send(new DeleteTechologyCommand());
        Assert.True(result);
    }

    [Fact]
    public async Task ShouldCallGetTechnologyDetailHandlerAndRetrieveTechnologies()
    {
        _autoMapper.Setup(m => m.Map<IEnumerable<TechnologyDetailDto>>((TechnologyDetail)It.IsAny<IEnumerable<TechnologyDetail>>()))
            .Returns(new List<TechnologyDetailDto>() {
                    new TechnologyDetailDto() { Id = 1, Description="TechnologyDetailTest" }});
        _technologyService.Setup(x => x.GetTechnologyDetailsAsync(It.IsAny<SearchParam>(), It.IsAny<CancellationToken>()));


        var result = await _mediator.Send(new GetTechnologyDetailQuery());
        Assert.Equal("TechnologyDetailTest", result.First().Description);

    }
    [Fact]
    public async Task ShouldCallInsertTechnologyDetailCommandHandler()
    {
        _technologyService.Setup(x => x.InsertTechnologyDetailAsync(It.IsAny<TechnologyDetail>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(5));
        var result = await _mediator.Send(new InsertTechologyDetailCommand(), CancellationToken.None);
        Assert.Equal(5, result);
    }

    [Fact]
    public async Task ShouldCallUpdateTechnologyDetailCommandHandler()
    {
        _technologyService.Setup(x => x.UpdateTechnologyDetailAsync(It.IsAny<TechnologyDetail>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(5));
        var result = await _mediator.Send(new UpdateTechologyDetailCommand());
        Assert.Equal(5, result);
    }
    [Fact]
    public async Task ShouldCallDeleteTechnologyDetailCommandHandler()
    {
        _technologyService.Setup(x => x.DeleteTechnologyDetailAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(true));
        var result = await _mediator.Send(new DeleteTechologyDetailCommand());
        Assert.True(result);
    }


}

