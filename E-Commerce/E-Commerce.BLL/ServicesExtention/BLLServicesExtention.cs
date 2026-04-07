using E_Commerce.Bll;
using E_Commerce.BLL.Manager;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

namespace E_Commerce.BLL
{
    public static class BLLServicesExtention
    {
        public static void AddBLLServices(this IServiceCollection services)
        {
            services.AddScoped<IProductManager, ProductManager>();
            services.AddScoped<ICategoryManager, CategoryManager>();
            services.AddScoped<ICartManager, CartManager>();
            services.AddScoped<IOrderManager, OrderManager>();
            services.AddScoped<IImageManager, ImageManager>();
            services.AddValidatorsFromAssembly(typeof(BLLServicesExtention).Assembly);
            services.AddScoped<IErrorMapper, ErrorMapper>();



           
        }
    }
}
