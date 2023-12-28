using BLL.Reposititories;
using BLL.Services;
using Core.DBContext;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public class MyContext : DbContext

{
    public class TestGestionCursosContext : GestionCursosContext
    {
        public TestGestionCursosContext() : base(new DbContextOptionsBuilder<GestionCursosContext>()
            .UseInMemoryDatabase("TestGestionCursos")
            .UseInternalServiceProvider(new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider())
            .ConfigureWarnings(b => b.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.InMemoryEventId.TransactionIgnoredWarning))
            .EnableSensitiveDataLogging()
            .Options)
        {
            this.ChangeTracker.AutoDetectChangesEnabled = true;
            this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
        }
    }

    public class unitTest1
    {

        private readonly TestGestionCursosContext _context;


        public unitTest1()
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

            var _unitOfWork = new UnitOfWork(_context);
            var _courseRepository = new GenericRepository<Course>(_context);
            var _courseDetailRepository = new GenericRepository<CourseDetail>(_context);
            var courseService = new CoursesService(_unitOfWork, _courseRepository, _courseDetailRepository, null);



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
            Assert.True(await _context.Courses.AnyAsync());
            var course = await _context.Courses.FirstAsync();
            Assert.Equal("courseTest", course.Name);
        }
    }
}


