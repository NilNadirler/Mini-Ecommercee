
using ETicaretAPI.Application.Abstractions;
using ETicaretAPI.Application.Abstractions.Storage;
using ETicaretAPI.Application.Abstractions.Token;
using ETicaretAPI.Insfracture.Enums;
using ETicaretAPI.Insfracture.Services;
using ETicaretAPI.Insfracture.Services.Storage;
using ETicaretAPI.Insfracture.Services.Storage.Azure;
using ETicaretAPI.Insfracture.Services.Storage.Local;
using ETicaretAPI.Insfracture.Services.Token;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Insfracture
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructures(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IStorageService, StorageService>();
            serviceCollection.AddScoped<ITokenHandler, TokenHandler>();
        }

        public static void AddStorage<T>(this IServiceCollection serviceCollection) where T: class, IStorage
        {
            serviceCollection.AddScoped<IStorage,T>();   
        }

        public static void AddStorage(this IServiceCollection serviceCollection, StorageType storageType)
        {
            switch (storageType)
            {
                case StorageType.Local:
                    serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;

                case StorageType.Azure:
                    serviceCollection.AddScoped<IStorage, AzureStorage>();
                    break;

                case StorageType.AWS:
                    break;

                default:
                    serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;
            }
        }
    }
}
