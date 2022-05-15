using System.Linq;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.DBContext
{
    public partial class GestionCursosContext : DbContext
    {
        public GestionCursosContext()
        {
        }

        public GestionCursosContext(DbContextOptions<GestionCursosContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())
            .ToList()
            .ForEach(x => x.DeleteBehavior = DeleteBehavior.Restrict);
            base.OnModelCreating(modelbuilder);
        }


        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CourseDetail> CourseDetails { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<Technology> Technologies { get; set; }
        public virtual DbSet<TechnologyDetails> TechnologyDetails { get; set; }
        public virtual DbSet<UserInfo> UserInfo { get; set; }
        public virtual DbSet<Adresses> Adresses { get; set; }

        public virtual DbSet<PaymentInfo> PaymentInfo { get; set; }


    }


}