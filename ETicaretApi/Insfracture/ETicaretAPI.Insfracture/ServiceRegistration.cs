using ETicaretAPI.Application.Services;
using ETicaretAPI.Insfracture.Services;
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
            serviceCollection.AddScoped<IFileService, FileService>();
        }
    }
}
