using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorldCities.Models;

namespace WorldCities.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext() : base() { }

        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<City>()
                    .ToTable("Cities");
            modelBuilder.Entity<City>()
                    .HasKey(c => c.CityId);
            modelBuilder.Entity<City>()
                    .Property(c => c.CityId).IsRequired();
            modelBuilder.Entity<Country>()
                    .ToTable("Countries");
            modelBuilder.Entity<Country>()
                    .HasKey(c => c.CountryId);
            modelBuilder.Entity<Country>()
                    .Property(c => c.CountryId).IsRequired();
            modelBuilder.Entity<Country>()
                    .HasMany<City>(c => c.Cities)
                    .WithOne(y => y.Country)
                    .HasForeignKey(p => p.CountryId);
        }*/

    }
}
