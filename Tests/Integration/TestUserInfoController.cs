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


public class TestUserInfoController
{
    private readonly Mock<IUserInfoService> _userService;
    private readonly UserInfoController _userInfoController;

    private readonly Mock<IMediator> _mediatorMock;

    private readonly Mock<IMapper> _mapper;

    public TestUserInfoController()
    {
        _mediatorMock = new Mock<IMediator>();
        _userService = new Mock<IUserInfoService>();
        _mapper = new Mock<IMapper>();
        _userInfoController = new UserInfoController(_mapper.Object,_mediatorMock.Object, _userService.Object);
    }

    [Fact]
    public async Task ShouldCallGetUserInfoControllerAndRetrieveUsers()
    {

        var expectedresult = new DataResults<UserInfoDto>()
        {
            Results = 1,
            Dto = new List<UserInfoDto>()
                        {
                            new UserInfoDto() {
                                Id= 1,
                                Name = "UserMock"
                           }

                }
        };


        _mediatorMock.Setup(x => x.Send(It.IsAny<GetUserInfoQuery>(), It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(expectedresult);
        var resultData = await _userInfoController.GetUsersInfo(new SearchParamUsers()
        {
            page = 1,
            pageSize = 1
        });
        var result = ((Pagination<UserInfoDto>)((OkObjectResult)resultData.Result!).Value!).Data[0].Name;
        Assert.Equal("UserMock", result);
    }


    [Fact]
    public async Task ShouldCallUserInfoControllerAndInsertUser()
    {
        _mediatorMock.Setup(x => x.Send(It.IsAny<InsertUserInfoCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(5);
        var resultData = await _userInfoController.InsertUserInfo(new UserInfoDto());
        Assert.Equal(5, ((OkObjectResult)resultData).Value);
    }
    [Fact]
    public async Task ShouldCallUserInfoControllerAndUpdateUser()
    {
        _mediatorMock.Setup(x => x.Send(It.IsAny<UpdateUserInfoCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(5);
        var resultData = await _userInfoController.UpdateUserInfo(new UserInfoDto());

        Assert.Equal(5, ((OkObjectResult)resultData.Result!).Value);
    }

    [Fact]
    public async Task ShouldCallUserInfoControllerAndDeleteUser()
    {
        _mediatorMock.Setup(x => x.Send(It.IsAny<DeleteUserInfoCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
        var resultData = await _userInfoController.DeleteUser(5);
        Assert.True  ((bool)((OkObjectResult)resultData).Value);

   

}