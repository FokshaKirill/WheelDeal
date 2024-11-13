using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using WheelDeal.Entities;
using WheelDeal.Models;

namespace WheelDeal.Database;

public class ApplicationDbContext : DbContext
{
    // public DbSet<ItemCard> ItemCards { get; set; }
    //
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //     {
    //     if (!optionsBuilder.IsConfigured)
    //     {
    //         IConfigurationRoot configuration = new ConfigurationBuilder()
    //             .SetBasePath(Directory.GetCurrentDirectory())
    //             .AddJsonFile("appsettings.json")
    //             .Build();
    //         var connectionString = configuration.GetConnectionString("DBContextConnection");
    //         optionsBuilder.UseSqlServer(connectionString);
    //     }
    // }
    //
    // private readonly IConfiguration _configuration;
    //
    // public ApplicationDbContext(IConfiguration configuration)
    // {
    //     _configuration = configuration;
    // }
    //
    // public ApplicationDbContext()
    // {
    //     throw new NotImplementedException();
    // }
    //
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder
    //         .UseNpgsql(_configuration.GetConnectionString("Database"))
    //         .UseLoggerFactory(CreateLoggerFactory())
    //         .EnableSensitiveDataLogging();
    // }
    //
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<Car>(carBuilder =>
    //     {
    //         carBuilder.ToTable("Cars").HasKey(c => c.Id);
    //         carBuilder.Property(c => c.Id).HasColumnName("CarID");
    //         carBuilder.ComplexProperty(c => c.Brand, brandBuilder =>
    //         {
    //             brandBuilder.Property(b => b.First()).HasColumnName("firstName").HasMaxLength(100);
    //             brandBuilder.Property(b => b.Last()).HasColumnName("lastName").HasMaxLength(100);
    //         });
    //         carBuilder.HasOne(c => c.Model)
    //             .WithMany();
    //     });
    // }
    
    public ILoggerFactory CreateLoggerFactory() => 
        LoggerFactory.Create(builder => {builder.AddConsole();});
}