using WheelDeal.Domain.Database.ModelsDb;
using WheelDeal.Domain.Database.Storage;
using WheelDeal.Domain.Interfaces;
using WheelDeal.Service;
using WheelDeal.Service.Interfaces;

namespace WheelDeal;

public static class Initializer
{
    public static void InitializeRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBaseStorage<UserDb>, UserStorage>();
    }
    
    public static void InitializeServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        
        services.AddControllersWithViews()
            .AddDataAnnotationsLocalization()
            .AddViewLocalization();
    }
}