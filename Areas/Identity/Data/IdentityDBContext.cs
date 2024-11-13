using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WheelDeal.Areas.Identity.Data;

namespace WheelDeal.Areas.Identity.Data;

public class IdentityDBContext : IdentityDbContext<IdentityUser>
{
    public IdentityDBContext(DbContextOptions<IdentityDBContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}