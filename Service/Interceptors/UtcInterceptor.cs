
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace WheelDeal.Service.Interceptors;

public class UtcDateInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        ConvertDatesToUtc(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    private void ConvertDatesToUtc(DbContext? context)
    {
        if (context == null) return;

        foreach (var entry in context.ChangeTracker.Entries())
        {
            if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
            {
                foreach (var property in entry.Properties)
                {
                    if (property.Metadata.ClrType == typeof(DateTime) && property.CurrentValue is DateTime dt)
                    {
                        property.CurrentValue = DateTime.SpecifyKind(dt, DateTimeKind.Utc).ToUniversalTime();
                    }
                }
            }
        }
    }
}