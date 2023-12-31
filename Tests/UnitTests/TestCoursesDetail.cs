using BLL.Reposititories;
using BLL.SearchParams;
using BLL.Services;
using Core.DBContext;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.UnitTests;

public class TestCoursesDetail
{
    public readonly TestGestionCursosContext _context;
    public TestCoursesDetail()
    {
        _context = new TestGestionCursosContext();

    }

    [Fact]
    public async Task CanIsertCourseDetail()
    {
        var courseService = new CoursesService(new UnitOfWork(_context), null, new GenericRepository<CourseDetail>(_context), null);

        await courseService.InsertCourseDetailAsync(new CourseDetail()
        {
            Courseid = 1,
            Description = "TestDetail",
            Lessonid = 1

        }, CancellationToken.None);


        Assert.True(await _context.CourseDetails.AnyAsync());
        var course = await _context.CourseDetails.FirstAsync();
        Assert.Equal("TestDetail", course.Description);
        _context.Database.EnsureDeleted();
    }

    [Fact]
    public async Task CanUpdateCourseDetail()
    {

        var courseService = new CoursesService(new UnitOfWork(_context), null, new GenericRepository<CourseDetail>(_context), null);

        await _context.AddAsync(new CourseDetail()
        {
            Courseid = 1,
            Description = "TestDetail",
            Lessonid = 1

        });
        _context.SaveChanges();
        _context.ChangeTracker.Clear();


        var result = await courseService.UpdateCourseDetailsAsync(new CourseDetail()
        {

            Id = 1,
            Courseid = 1,
            Description = "TestDetailChanged",
            Lessonid = 1
        }, CancellationToken.None);

        var course = await _context.CourseDetails.FirstAsync();
        Assert.Equal("TestDetailChanged", course.Description);
        Assert.True(result > 0);
        _context.Database.EnsureDeleted();
    }

    public async Task CanDeleteCourseDetail()
    {
        var courseService = new CoursesService(new UnitOfWork(_context), null, new GenericRepository<CourseDetail>(_context), null);

        await _context.AddAsync(new CourseDetail()
        {
            Courseid = 1,
            Description = "TestDetail",
            Lessonid = 1

        });
        _context.SaveChanges();
        _context.ChangeTracker.Clear();
        //   var result = await courseService.De(1, CancellationToken.None);

    }
    [Fact]
    public async Task GetCourseDetail()
    {
        var courseService = new CoursesService(new UnitOfWork(_context), null, new GenericRepository<CourseDetail>(_context), null);
        await _context.AddAsync(new CourseDetail()
        {
            Courseid = 1,
            Description = "TestDetail",
            Lessonid = 1

        });
        _context.SaveChanges();
        var result = await courseService.GetCourseDetailsAsync(new SearchParamCourses()
        {
            page = 1,
            pageSize = 1,

        }, CancellationToken.None);
        Assert.Equal("TestDetail", result.ElementAt(0).Description);
    }
}




