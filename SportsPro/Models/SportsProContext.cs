using Microsoft.EntityFrameworkCore;
using System;

namespace SportsPro.Models
{
    public class SportsProContext : DbContext
    {
        public SportsProContext(DbContextOptions<SportsProContext> options)
            : base(options)
        { }

        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Technician> Technicians { get; set; } = null!;
        public DbSet<Country> Countries { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Incident> Incidents { get; set; } = null!;
        public DbSet<Registration> Registrations { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Registration>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Registrations)
                .HasForeignKey(r => r.CustomerID);
            modelBuilder.Entity<Registration>()
                .HasOne(r => r.Product)
                .WithMany(p => p.Registrations)
                .HasForeignKey(r => r.ProductID);

            // Seed Countries
            modelBuilder.Entity<Country>().HasData(
                new Country { CountryID = "US", Name = "United States" },
                new Country { CountryID = "CA", Name = "Canada" }
            );

            // Seed Customers
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    CustomerID = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Address = "123 Main St",
                    City = "Atlanta",
                    State = "GA",
                    PostalCode = "30301",
                    Phone = "555-1234",
                    Email = "john@example.com",
                    CountryID = "US"
                },
                new Customer
                {
                    CustomerID = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Address = "456 Oak Ave",
                    City = "Toronto",
                    State = "ON",
                    PostalCode = "M4B1B3",
                    Phone = "555-5678",
                    Email = "jane@example.com",
                    CountryID = "CA"
                }
            );

            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductID = 1,
                    ProductCode = "PROD1",
                    Name = "Product One",
                    YearlyPrice = 10.00m,
                    ReleaseDate = new DateTime(2023, 1, 1)
                },
                new Product
                {
                    ProductID = 2,
                    ProductCode = "PROD2",
                    Name = "Product Two",
                    YearlyPrice = 20.00m,
                    ReleaseDate = new DateTime(2023, 2, 1)
                }
            );

            // Seed Technicians
            modelBuilder.Entity<Technician>().HasData(
                new Technician { TechnicianID = 1, Name = "Tech One", Email = "tech1@example.com", Phone = "555-0001" },
                new Technician { TechnicianID = 2, Name = "Tech Two", Email = "tech2@example.com", Phone = "555-0002" }
            );

            // Seed Incidents with valid ProductIDs
            modelBuilder.Entity<Incident>().HasData(
                new Incident
                {
                    IncidentID = 1,
                    CustomerID = 1,
                    TechnicianID = 1,
                    Title = "Issue One",
                    Description = "First issue",
                    DateOpened = new DateTime(2023, 1, 1),
                    ProductID = 1
                },
                new Incident
                {
                    IncidentID = 2,
                    CustomerID = 2,
                    TechnicianID = 2,
                    Title = "Issue Two",
                    Description = "Second issue",
                    DateOpened = new DateTime(2023, 1, 2),
                    ProductID = 2
                }
            );

            // Seed Registrations
            modelBuilder.Entity<Registration>().HasData(
                new Registration { RegistrationID = 1, CustomerID = 1, ProductID = 1 },
                new Registration { RegistrationID = 2, CustomerID = 2, ProductID = 2 }
            );
        }



    }
}
