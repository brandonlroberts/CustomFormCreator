using CustomFormCreator.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CustomFormCreator.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Form> Forms { get; set; }
        public DbSet<FormSection> FormSections { get; set; }
        public DbSet<FormColumn> FormColumns { get; set; }
        public DbSet<FormColumnType> FormColumnTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Form>().ToTable("Form");
            modelBuilder.Entity<FormSection>().ToTable("FormSection");
            modelBuilder.Entity<FormColumn>().ToTable("FormColumn");
            modelBuilder.Entity<FormColumnType>().ToTable("FormColumnTypes");
            modelBuilder.Ignore<IdentityUserLogin<string>>();
            modelBuilder.Ignore<IdentityUserRole<string>>();
            //modelBuilder.Ignore<IdentityUserClaim<string>>();
            modelBuilder.Ignore<IdentityUserToken<string>>();
            modelBuilder.Ignore<IdentityUser<string>>();
        }

        public override int SaveChanges()
        {

            //set default value for IsActive property
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("IsActive") != null))
                {
                    if (entry.State == EntityState.Added)
                    {
                        if (entry.Property("IsActive").CurrentValue == null)
                            entry.Property("IsActive").CurrentValue = true;
                    }
                }

            return base.SaveChanges();
        }
    }
}
