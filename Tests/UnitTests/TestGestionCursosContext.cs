using Core.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace Tests.UnitTests;
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
