using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaretDbContext.Persistance.Concrete;
using ETicaretDbContext.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using ETicaretAPI.Persistance;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Persistance.Repositories;
using ETicaretAPI.Domain.Entities.Identifier;

namespace ETicaretDbContext.Persistance
{
    public static class ServiceRegistration
    {

        public static void AddPersistanceServices(this IServiceCollection services)
        {
            

            services.AddDbContext<ETicaretAPIDbContext>(options=> options.UseNpgsql(Configuration.ConnectionString));


            services.AddIdentity<AppUser, AppRole>(options =>

            {
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;  
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;  

            })
            .AddEntityFrameworkStores<ETicaretAPIDbContext>();

            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
           
            services.AddScoped<IFileReadRepository, FileReadRepository>();
            services.AddScoped<IFileWriteRepository, FileWriteRepository>();
            
            services.AddScoped<IInvoiceFileReadRepository, InvoiceFileReadRepository>();
            services.AddScoped<IInvoiceFileWriteRepository, InvoiceFileWriteRepository>();
            
            services.AddScoped<IProductImageFileReadRepository, ProductImageFileReadRepository>();
            services.AddScoped<IProductImageFileWriteRepository, ProductImageFileWriteRepository>();





        }

    }
}
