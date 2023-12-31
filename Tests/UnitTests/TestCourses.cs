using BLL.Reposititories;
using BLL.Services;
using Core.DBContext;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.UnitTests;


public class TestCourses
{

    public readonly TestGestionCursosContext _context;
    public TestCourses()
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
        _context.Teachers.AddAsync(new Teacher()
        {
            Id = 1,
            CountryId = 1,
            Name = "Enrique",
            Surname = "Casanovas",
            description = "desc",
            UrlSite = "test",
            UrlSocial = "test"
        });
        _context.SaveChanges();
    }

    [Fact]
    public async Task CanIsertCourse()
    {
        var courseService = new CoursesService(new UnitOfWork(_context), new GenericRepository<Course>(_context), null, null);

        await courseService.InsertCourseAsync(new Course()
        {
            Description = "courseTest",
            TeacherId = 1,
            TechnologyId = 1,
            TechnologyDetailsId = 1,
            Name = "courseTest",
            Photo = "",
            Url = "test"
        }, CancellationToken.None);

        // Verificamos que el producto se haya guardado correctamente
        Assert.True(await _context.Courses.AsNoTracking().AnyAsync());
        var course = await _context.Courses.AsNoTracking().FirstAsync();
        Assert.Equal("courseTest", course.Name);
        _context.Database.EnsureDeleted();
    }

    [Fact]
    public async Task CanUpdateCourse()
    {
        var courseService = new CoursesService(new UnitOfWork(_context), new GenericRepository<Course>(_context), null, null);

        await _context.Courses.AddAsync(new Course()
        {
            Description = "courseTest",
            TeacherId = 1,
            TechnologyId = 1,
            TechnologyDetailsId = 1,
            Name = "courseTest",
            Photo = "",
            Url = "test"
        });
        _context.SaveChanges();
        _context.ChangeTracker.Clear();

        var result = await courseService.UpdateCourseAsync(new Course()
        {
            Id = 1,
            Description = "courseTest2",
            TeacherId = 1,
            TechnologyId = 1,
            TechnologyDetailsId = 1,
            Name = "courseTest2",
            Photo = "",
            Url = "test"

        }, CancellationToken.None);

        var course = await _context.Courses.FirstAsync();
        Assert.Equal("courseTest2", course.Name);
        Assert.Equal(1, result);
        _context.Database.EnsureDeleted();
    }
    [Fact]
    public async Task CanDeleteCourse()
    {
        var courseService = new CoursesService(new UnitOfWork(_context), new GenericRepository<Course>(_context), null, null);

        await _context.Courses.AddAsync(new Course()
        {
            Description = "courseTest",
            TeacherId = 1,
            TechnologyId = 1,
            TechnologyDetailsId = 1,
            Name = "courseTest",
            Photo = "",
            Url = "test"
        });
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        var result = await courseService.DeleteCourseAsync(1, CancellationToken.None);
        Assert.True(result);
        _context.Database.EnsureDeleted();


    }
    [Fact]
    public async Task GetCourseData()
    {

        var courseService = new CoursesService(new UnitOfWork(_context), new GenericRepository<Course>(_context), null, null);
        await _context.Courses.AddAsync(new Course()
        {
            Description = "courseTest",
            TeacherId = 1,
            TechnologyId = 1,
            TechnologyDetailsId = 1,
            Name = "courseTest",
            Photo = "",
            Url = "test"
        });
        _context.SaveChanges();
        await _context.SaveChangesAsync();

        var result = await courseService.GetCoursesAsync(new BLL.SearchParams.SearchParamCourses()
        {
            page = 1,
            pageSize = 1

        }, CancellationToken.None);
        var result2 = _context.Courses.FirstAsync();

        Assert.Equal("courseTest", result.ElementAt(0).Name);
        _context.Database.EnsureDeleted();
    }
}




