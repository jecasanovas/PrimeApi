using BLL.Interfaces.Repositories;
using BLL.Reposititories;
using BLL.Services;
using Core.DBContext;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.UnitTests;


public class TestUserInfo
{

    public readonly TestGestionCursosContext _context;
    public TestUserInfo()
    {
        _context = new TestGestionCursosContext();
    }

    [Fact]
    public async Task CanInsertUser()
    {

        var userService = new UserService(new UnitOfWork(_context), null, new GenericRepository<UserInfo>(_context), null);

        await userService.InsertUserInfoAsync(new UserInfo()
        {
            Name = "User1",
            Surname = "Surname1"

        }, CancellationToken.None);



        // Verificamos que el producto se haya guardado correctamente
        Assert.True(await _context.UserInfo.AnyAsync());
        var user = await _context.UserInfo.FirstAsync();
        Assert.Equal("User1", user.Name);
        _context.Database.EnsureDeleted();
    }

    [Fact]
    public async Task CanUpdateUser()
    {
        var userService = new UserService(new UnitOfWork(_context), null, new GenericRepository<UserInfo>(_context), null);

        await _context.UserInfo.AddAsync(new UserInfo()
        {
            Name = "UserTest",
            Surname = "UserSurnameTest"

        }, CancellationToken.None);
        _context.SaveChanges();
        _context.ChangeTracker.Clear();

        var result = await userService.UpdateUserInfoAsync(new UserInfo()
        {
            Id = 1,
            Description = "UserTestChanged",
            Name = "UserTestChanged",
            Photo = "",


        }, CancellationToken.None);

        var user = await _context.UserInfo.FirstAsync();
        Assert.Equal("UserTestChanged", user.Name);
        Assert.Equal(1, result);
        _context.Database.EnsureDeleted();
    }
    [Fact]
    public async Task CanDeleteUser()
    {
        var userService = new UserService(new UnitOfWork(_context), null, new GenericRepository<UserInfo>(_context), null);

        await _context.UserInfo.AddAsync(new UserInfo()
        {
            Name = "UserTest",
            Surname = "UserSurnameTest"

        }, CancellationToken.None);
        _context.SaveChanges();
        _context.ChangeTracker.Clear();



        var result = await userService.DeleteUserInfoAsync(1, CancellationToken.None);
        Assert.True(result);
        _context.Database.EnsureDeleted();


    }
    [Fact]
    public async Task UsersData()
    {

        var userService = new UserService(new UnitOfWork(_context), null, new GenericRepository<UserInfo>(_context), null);
        await _context.UserInfo.AddAsync(new UserInfo()
        {
            Name = "UserTest",
            Surname = "UserSurnameTest"

        });
        _context.SaveChanges();
        await _context.SaveChangesAsync();

        var result = await userService.GetUsersInfoAsync(new BLL.SearchParams.SearchParamUsers()
        {
            page = 1,
            pageSize = 1

        }, CancellationToken.None);
        var result2 = _context.Courses.FirstAsync();

        Assert.Equal("UserTest", result.ElementAt(0).Name);
        _context.Database.EnsureDeleted();
    }
}


