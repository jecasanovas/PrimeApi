using AutoMapper;
using BLL.CQRS.Commands;
using BLL.CQRS.Queries;
using BLL.Dtos;
using BLL.Interfaces;
using BLL.Interfaces.Services;
using BLL.SearchParams;
using BLL.Services;
using Core.Entities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
namespace Tests.Functional;
public class TestUserInfoHandlers
{
    private readonly Mediator _mediator;
    private readonly Mock<IUserInfoService> _userService;
    private readonly Mock<IMapper> _autoMapper;
    public TestUserInfoHandlers()
    {

        _userService = new Mock<IUserInfoService>();
        _autoMapper = new Mock<IMapper>();

        var provider = new ServiceCollection()
                        .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetCourseQuery).Assembly))
                        .AddSingleton(_userService.Object)
                        .AddSingleton(_autoMapper.Object).BuildServiceProvider();
        _mediator = new Mediator(provider);

    }

    [Fact]
    public async Task ShouldCallGerUserInfoHandlerAndRetrieveUsers()
    {
        _autoMapper.Setup(m => m.Map<IEnumerable<UserInfoDto>>((UserInfo)It.IsAny<IEnumerable<UserInfo>>()))
            .Returns(new List<UserInfoDto>() {
                    new UserInfoDto() { Id = 1, Name = "TestResult" }});
        _userService.Setup(x => x.GetUsersInfoAsync(It.IsAny<SearchParamUsers>(), It.IsAny<CancellationToken>()));


        var result = await _mediator.Send(new GetUserInfoQuery());
        Assert.Equal("TestResult", result.Dto.ElementAt(0).Name);

    }
    [Fact]
    public async Task ShouldCallInsertUserInfoCommandHandler()
    {
        _userService.Setup(x => x.InsertUserInfoAsync(It.IsAny<UserInfo>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(5));
        var result = await _mediator.Send(new InsertUserInfoCommand(), CancellationToken.None);
        Assert.Equal(5, result);
    }

    [Fact]
    public async Task ShouldCallUpdateUserInfoCommandHandler()
    {
        _userService.Setup(x => x.UpdateUserInfoAsync(It.IsAny<UserInfo>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(5));
        var result = await _mediator.Send(new UpdateUserInfoCommand());
        Assert.Equal(5, result);
    }
    [Fact]
    public async Task ShouldCallDeleteUserInfoCommandHandler()
    {
        _userService.Setup(x => x.DeleteUserInfoAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(true));
        var result = await _mediator.Send(new DeleteUserInfoCommand());
        Assert.True(result);
    }
}
