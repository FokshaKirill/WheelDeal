using Microsoft.EntityFrameworkCore;
using WheelDeal.Domain.Database.Entities;
using WheelDeal.Domain.Database.ModelsDb;

namespace WheelDeal.Domain.Database;

public class ApplicationDbContext : DbContext
{
    public DbSet<UserDb> UsersDb { get; set; }
    public DbSet<RateDb> RatesDb { get; set; }
    public DbSet<PostDb> PostsDb { get; set; }
    public DbSet<CategoryDb> CategoriesDb { get; set; }
    public DbSet<CarDb> CarsDb { get; set; }
    
    private readonly IConfiguration Configuration;
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    public ApplicationDbContext()
    {
        throw new NotImplementedException();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoryDb>()
            .ToTable("categories");
        modelBuilder.Entity<CarDb>()
            .ToTable("cars");
        modelBuilder.Entity<UserDb>()
            .ToTable("users");
        modelBuilder.Entity<RateDb>()
            .ToTable("rates");
        
        modelBuilder.Entity<PostDb>(entity =>
        {
            entity.ToTable("posts");

            entity.Property(e => e.CarId)
                .HasColumnName("carid"); // Убедитесь, что имя столбца соответствует БД
        });
    }

    public ILoggerFactory CreateLoggerFactory() => 
        LoggerFactory.Create(builder => {builder.AddConsole();});
}