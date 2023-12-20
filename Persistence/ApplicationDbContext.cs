using Domain.Entities;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityConfigurations;

namespace Persistence
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        { }

        /// <summary>
        /// Set tables
        /// </summary>
        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<AppRole> AppRoles { get; set; }
        public virtual DbSet<Example> Examples { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AppUserEntityConfigurations());
            builder.ApplyConfiguration(new ExampleEntityConfigurations());

            //Seeding Data
            builder.Entity<AppRole>().HasData(new AppRole[]
            {
                new AppRole()
                {
                    Id = 1,
                    Name = Application.Consts.UserRoles.Admin,
                    NormalizedName = Application.Consts.UserRoles.Admin.ToUpper()
                },
                new AppRole()
                {
                    Id = 2,
                    Name = Application.Consts.UserRoles.Member,
                    NormalizedName = Application.Consts.UserRoles.Member.ToUpper()
                }
            });

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("");

            optionsBuilder.UseLazyLoadingProxies();

            base.OnConfiguring(optionsBuilder);
        }
    }
}
