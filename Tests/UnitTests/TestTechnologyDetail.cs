
using BLL.Reposititories;
using BLL.SearchParams;
using BLL.Services;
using Core.DBContext;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Testing;


public class TestTechnologyDetail
{
    public readonly TestGestionCursosContext _context;
    public TestTechnologyDetail()
    {
        _context = new TestGestionCursosContext();

    }

    [Fact]
    public async Task CanIsertTechnologyDetail()
    {
        var technologyService = new TechnologyService(new UnitOfWork(_context), null, new GenericRepository<TechnologyDetail>(_context));

        await technologyService.InsertTechnologyDetailAsync(new TechnologyDetail()
        {
            TechnologyId = 1,
            Description = "Angular"
        }, CancellationToken.None);


        Assert.True(await _context.TechnologyDetails.AnyAsync());
        var technology = (await _context.TechnologyDetails.FirstAsync()).Description;
        Assert.Equal("Angular", technology);
        _context.Database.EnsureDeleted();
    }

    [Fact]
    public async Task CanUpdateTechnologyDetail()
    {
        var technologyService = new TechnologyService(new UnitOfWork(_context), null, new GenericRepository<TechnologyDetail>(_context));

        await _context.AddAsync(new TechnologyDetail()
        {
            TechnologyId = 1,
            Description = "Angular"

        });
        _context.SaveChanges();
        _context.ChangeTracker.Clear();


        var result = await technologyService.UpdateTechnologyDetailAsync(new TechnologyDetail()
        {
            TechnologyId = 1,
            Id = 1,
            Description = "AngularChanged"
        }, CancellationToken.None);

        var technology = await _context.TechnologyDetails.FirstAsync();
        Assert.Equal("AngularChanged", technology.Description);
        Assert.True(result > 0);
        _context.Database.EnsureDeleted();
    }

    [Fact]
    public async Task CanDeleteTechologyDetail()
    {

        var technologyService = new TechnologyService(new UnitOfWork(_context), null, new GenericRepository<TechnologyDetail>(_context));

        await _context.AddAsync(new TechnologyDetail()
        {
            TechnologyId = 1,
            Description = "Angular"

        });
        _context.SaveChanges();
        _context.ChangeTracker.Clear();

        var result = await technologyService.DeleteTechnologyDetailAsync(1, CancellationToken.None);
        Assert.True(result);
    }
    [Fact]
    public async Task GetTechnlogyDetail()
    {
        var technologyService = new TechnologyService(new UnitOfWork(_context), null, new GenericRepository<TechnologyDetail>(_context));

        await _context.AddAsync(new TechnologyDetail()
        {
            TechnologyId = 1,
            Description = "Angular"

        });
        _context.SaveChanges();

        var result = await technologyService.GetTechnologyDetailsAsync(new SearchParam()
        {
            page = 1,
            pageSize = 1,

        }, CancellationToken.None);
        Assert.Equal("Angular", result.ElementAt(0).Description);
    }
}



