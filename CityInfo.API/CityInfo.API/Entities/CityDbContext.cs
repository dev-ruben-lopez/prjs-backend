using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Entities
{
    public class CityDbContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<PointOfInterest> PointOfInterest { get; set; }


        public CityDbContext(DbContextOptions<CityDbContext> options) : base(options)
        {
            Database.EnsureCreated();
            //Database.Migrate(); //this is for using Migration tools, and it will create the DB if none, or update 
        }


        /// <summary>
        /// Override the default behavior by specifying singular table names in the DbContext if required !!! as the
        /// DbSet's are name in plural, but we dont want that in the DB.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().ToTable("City");
            modelBuilder.Entity<PointOfInterest>().ToTable("PointOfInterest");
        }


        
        /*
         * This part is not longer required here, its equivalent goes on Startup.cs ConfigureServices for CORE 2.x
        protected override void OnCOnfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("connectionstring");
            base.OnConfiguring(optionsBuilder);

        }*/
    }
}
