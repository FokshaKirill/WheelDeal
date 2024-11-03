using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using WheelDeal.Entities;

namespace WheelDeal.Database;

public class ApplicationDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public ApplicationDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(_configuration.GetConnectionString("Database"))
            .UseLoggerFactory(CreateLoggerFactory())
            .EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>(carBuilder =>
        {
            carBuilder.ToTable("Cars").HasKey(c => c.Id);
            carBuilder.Property(c => c.Id).HasColumnName("CarID");
            carBuilder.ComplexProperty(c => c.Brand, brandBuilder =>
            {
                brandBuilder.Property(b => b.First()).HasColumnName("firstName").HasMaxLength(100);
                brandBuilder.Property(b => b.Last()).HasColumnName("lastName").HasMaxLength(100);
            });
            carBuilder.HasOne(c => c.Model)
                .WithMany();
        });
    }

    public ILoggerFactory CreateLoggerFactory() => 
        LoggerFactory.Create(builder => {builder.AddConsole();});
}