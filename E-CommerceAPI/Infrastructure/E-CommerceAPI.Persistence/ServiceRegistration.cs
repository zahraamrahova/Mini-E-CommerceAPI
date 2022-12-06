using E_CommerceAPI.Application.Repositories;
using E_CommerceAPI.Domain.Entities.Identity;
using E_CommerceAPI.Persistence.Contexts;
using E_CommerceAPI.Persistence.Repositories;
using E_CommerceAPI.Persistence.Repositories.File;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace E_CommerceAPI.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<E_CommerceAPIDbContext>(options => options.UseSqlServer(Configuration.ConnectionString));
            services.AddIdentity<AppUser, AppRole>(options=>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<E_CommerceAPIDbContext>();
            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
            services.AddScoped<IFileWriteRepository, FileWriteRepository>();
            services.AddScoped<IFileReadRepository, FileReadRepository>();
            services.AddScoped<IProductImageFileWriteRepository, ProductImageFileWriteRepository>();
            services.AddScoped<IProductImageFileReadRepository, ProductImageFileReadRepository>();
            services.AddScoped<IInvoiceFileReadRepository, InvoiceFileReadRepository>();
            services.AddScoped<IInvoiceFileWriteRepository, InvoiceFileWriteRepository>();

        }
    }
}
