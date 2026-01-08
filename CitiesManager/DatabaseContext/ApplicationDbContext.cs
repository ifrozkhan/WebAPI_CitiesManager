using CitiesManager.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace CitiesManager.WebAPI.DatabaseContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected ApplicationDbContext()
        {
        }

        public virtual DbSet<Employee_Details> Employee_Details { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee_Details>().HasData(
                new Employee_Details
                {
                    CityId = Guid.Parse("0AD84AF6-6664-4BDF-8841-E98B73CC8838"),
                    Cityname = "New York"
                },
                new Employee_Details
                {
                    CityId = Guid.Parse("C90857E2-8A36-4D90-B2FF-9F2F4EF58463"),
                    Cityname = "London"
                }
            );
        }
    }
}
