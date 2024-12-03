using WheelDeal.Domain.Database.ModelsDb;
using WheelDeal.Domain.Database.Storage;
using WheelDeal.Domain.Interfaces;
using WheelDeal.Service.Interfaces;
using WheelDeal.Service.Realizations;

namespace WheelDeal;

public static class Initializer
{
    public static void InitializeRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBaseStorage<UserDb>, UserStorage>();
        services.AddScoped<IBaseStorage<CategoryDb>, CategoryStorage>();
        services.AddScoped<IBaseStorage<PostDb>, PostStorage>();
    }
    
    public static void InitializeServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IAutoParkService, AutoParkService>();        
        services.AddScoped<IPostService, PostService>();

        services.AddControllersWithViews()
            .AddDataAnnotationsLocalization()
            .AddViewLocalization();
    }
}