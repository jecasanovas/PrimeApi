using BLL.Reposititories;
using BLL.Services;
using Core.DBContext;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.UnitTests;


public class TestTeacher
{

    public readonly TestGestionCursosContext _context;
    public TestTeacher()
    {
        _context = new TestGestionCursosContext();
        _context.Countries.Add(new Country()
        {
            Id = 1,
            CountryDesc = "España"
        });

        _context.Technologies.AddAsync(new Technology()
        {
            Id = 1,
            Description = "FrontEnd"
        });
        _context.TechnologyDetails.AddAsync(new TechnologyDetail()
        {
            Id = 1,
            TechnologyId = 1,
            Description = "Angular"
        });
        _context.SaveChanges();
    }

    [Fact]
    public async Task CanInsertTeacher()
    {
        var teacherService = new TeacherService(new UnitOfWork(_context), new GenericRepository<Teacher>(_context), null);

        await teacherService.InsertTeacherAsync(new Teacher()
        {
            CountryId = 1,
            UrlSite = "urltest",
            UrlSocial = "urlsocial",
            Surname = "TeacherSurname",
            Name = "TeacherTest1",
            Photo = "",

        }, CancellationToken.None);

        // Verificamos que el producto se haya guardado correctamente
        Assert.True(await _context.Teachers.AnyAsync());
        var teacher = await _context.Teachers.FirstAsync();
        Assert.Equal("TeacherTest1", teacher.Name);
        _context.Database.EnsureDeleted();
    }

    [Fact]
    public async Task CanUpdateTeacher()
    {
        var teacherService = new TeacherService(new UnitOfWork(_context), new GenericRepository<Teacher>(_context), null);

        await _context.Teachers.AddAsync(new Teacher()
        {
            CountryId = 1,
            Name = "Enrique",
            Surname = "Casanovas",
            description = "desc",
            UrlSite = "test",
            UrlSocial = "test"
        });
        _context.SaveChanges();
        _context.ChangeTracker.Clear();

        var result = await teacherService.UpdateTeacherAsync(new Teacher()
        {
            Id = 1,
            CountryId = 1,
            Name = "EnriqueChanged",
            Surname = "Casanovas",
            description = "desc",
            UrlSite = "test",
            UrlSocial = "test"

        }, CancellationToken.None);

        var teacher = await _context.Teachers.FirstAsync();
        Assert.Equal("EnriqueChanged", teacher.Name);
        Assert.Equal(1, result);
        _context.Database.EnsureDeleted();
    }
    [Fact]
    public async Task CanDeleteTeacher()
    {
        var teacherService = new TeacherService(new UnitOfWork(_context), new GenericRepository<Teacher>(_context), null);

        await _context.Teachers.AddAsync(new Teacher()
        {
            CountryId = 1,
            Name = "Enrique",
            Surname = "Casanovas",
            description = "desc",
            UrlSite = "test",
            UrlSocial = "test"
        });
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        var result = await teacherService.DeleteTeacherAsync(1, CancellationToken.None);
        Assert.True(result);
        _context.Database.EnsureDeleted();


    }
    [Fact]
    public async Task GetCourseData()
    {

        var teacherService = new TeacherService(new UnitOfWork(_context), new GenericRepository<Teacher>(_context), null);

        await _context.Teachers.AddAsync(new Teacher()
        {
            CountryId = 1,
            Name = "Enrique",
            Surname = "Casanovas",
            description = "desc",
            UrlSite = "test",
            UrlSocial = "test"
        });
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        var result = await teacherService.GetTeachersAsync(new BLL.SearchParams.SearchParamTeachers()
        {
            page = 1,
            pageSize = 1

        }, CancellationToken.None);
        var result2 = _context.Teachers.FirstAsync();

        Assert.Equal("Enrique", result.ElementAt(0).Name);
        _context.Database.EnsureDeleted();
    }
}
