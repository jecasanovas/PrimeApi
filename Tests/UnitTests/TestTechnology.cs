using BLL.Reposititories;
using BLL.SearchParams;
using BLL.Services;
using Core.DBContext;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Testing;


public class TestTechnology
{
    public readonly TestGestionCursosContext _context;
    public TestTechnology()
    {
        _context = new TestGestionCursosContext();

    }

    [Fact]
    public async Task CanIsertTechnology()
    {
        var technologyService = new TechnologyService(new UnitOfWork(_context), new GenericRepository<Technology>(_context), null);

        await technologyService.InsertTechnologyAsync(new Technology()
        {
            Description = "FrontEnd"
        }, CancellationToken.None);


        Assert.True(await _context.Technologies.AnyAsync());
        var technology = (await _context.Technologies.FirstAsync()).Description;
        Assert.Equal("FrontEnd", technology);
        _context.Database.EnsureDeleted();
    }

    [Fact]
    public async Task CanUpdateTechnology()
    {

        var technologyService = new TechnologyService(new UnitOfWork(_context), new GenericRepository<Technology>(_context), null);

        await _context.AddAsync(new Technology()
        {
            Description = "FrontEnd"

        });
        _context.SaveChanges();
        _context.ChangeTracker.Clear();


        var result = await technologyService.UpdateTechnologyAsync(new Technology()
        {
            Id = 1,
            Description = "FrontEndChanged"
        }, CancellationToken.None);

        var technology = await _context.Technologies.FirstAsync();
        Assert.Equal("FrontEndChanged", technology.Description);
        Assert.True(result > 0);
        _context.Database.EnsureDeleted();
    }

    [Fact]
    public async Task CanDeleteTechology()
    {
        var technologyService = new TechnologyService(new UnitOfWork(_context), new GenericRepository<Technology>(_context), null);

        await _context.AddAsync(new Technology()
        {
            Description = "FrontEnd"

        });
        _context.SaveChanges();
        _context.ChangeTracker.Clear();
        var result = await technologyService.DeleteTechnologyAsync(1, CancellationToken.None);
        Assert.True(result);
    }
    [Fact]
    public async Task GetTechnlogy()
    {
        var technologyService = new TechnologyService(new UnitOfWork(_context), new GenericRepository<Technology>(_context), null);

        await _context.AddAsync(new Technology()
        {
            Description = "FrontEnd"

        });
        _context.SaveChanges();


        _context.SaveChanges();
        var result = await technologyService.GetTechnologyAsync(new SearchParam()
        {
            page = 1,
            pageSize = 1,

        }, CancellationToken.None);
        Assert.Equal("FrontEnd", result.ElementAt(0).Description);
    }
}



