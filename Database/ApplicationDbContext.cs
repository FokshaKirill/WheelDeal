using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using WheelDeal.Database.ModelsDb;
using WheelDeal.Entities;
using WheelDeal.Models;

namespace WheelDeal.Database;

public class ApplicationDbContext : DbContext
{
    public DbSet<UserDb> UsersDb { get; set; }
    public DbSet<RateDb> RatesDb { get; set; }
    public DbSet<PostDb> PostsDb { get; set; }
    public DbSet<CarDb> CarsDb { get; set; }
    
    private readonly IConfiguration Configuration;
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    public ApplicationDbContext()
    {
        throw new NotImplementedException();
    }
    
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