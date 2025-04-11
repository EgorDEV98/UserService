using UserService.Application.Interfaces;
using UserService.Application.Mappers;
using UserService.Application.Services;

namespace UserService.WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUsersService, UsersService>();
        serviceCollection.AddScoped<IAuthsService, AuthService>();
        
        return serviceCollection;
    }
    
    public static IServiceCollection AddMappers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<UsersServiceMapper>();
        
        return serviceCollection;
    }
}