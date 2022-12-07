using E_CommerceAPI.Application.Abstractions.Storage;
using E_CommerceAPI.Application.Abstractions.Token;
using E_CommerceAPI.Infrastructure.Enums;
using E_CommerceAPI.Infrastructure.Services.Storage;
using E_CommerceAPI.Infrastructure.Services.Storage.Azure;
using E_CommerceAPI.Infrastructure.Services.Storage.Local;
using E_CommerceAPI.Infrastructure.Services.Token;
using Microsoft.Extensions.DependencyInjection;



namespace E_CommerceAPI.Infrastructure
{
    public static class ServiceRegistration
    {
  
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IStorageService, StorageService>();
            services.AddScoped<ITokenHandler, TokenHandler>();
        }
        public static void AddStorage <T>(this IServiceCollection services) where T:Storage , IStorage
        {
            services.AddScoped<IStorage, T>();
        }
        public static void AddStorage<T>(this IServiceCollection services, StorageType storageType) 
        {
            switch (storageType)
            {
                case StorageType.Local:
                    services.AddScoped<IStorage, LocalStorage>();
                    break;
                case StorageType.Azure:
                    services.AddScoped<IStorage, AzureStorage>();
                    break;
                case StorageType.AWS:
                    break;
                default:
                    services.AddScoped<IStorage, LocalStorage>();
                    break;

            }
        }

    }
}
