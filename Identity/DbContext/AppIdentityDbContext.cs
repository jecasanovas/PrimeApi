using System.Linq;
using Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.DbContext
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())
            .ToList()
            .ForEach(x => x.DeleteBehavior = DeleteBehavior.Restrict);


            base.OnModelCreating(builder);

            builder.Entity<AppUser>().Property(a => a.DisplayName)
                .HasMaxLength(20)
                .IsRequired();

            builder.Entity<Address>().Property(a => a.FirstName).IsRequired(); 
            builder.Entity<Address>().Property(a => a.LastName).IsRequired();
            builder.Entity<Address>().Property(a => a.Street).IsRequired();
            builder.Entity<Address>().Property(a => a.City).IsRequired();
            builder.Entity<Address>().Property(a => a.State).IsRequired();
            builder.Entity<Address>().Property(a => a.Zipcode).IsRequired();
        }
    }
}

