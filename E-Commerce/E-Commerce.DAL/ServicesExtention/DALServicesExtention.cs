using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace E_Commerce.DAL

{
    public static class DALServicesExtention
    {
        public static void AddDALServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString)
                .UseAsyncSeeding(async (context, _, _) =>
                {
                    if (await context.Set<Product>().AnyAsync())
                    {
                        return;
                    }

                    if (await context.Set<Category>().AnyAsync())
                    {
                        return;
                    }

                    if (await context.Set<Cart>().AnyAsync())
                    {
                        return;
                    }
                    if (await context.Set<CartItem>().AnyAsync())
                    {
                        return;
                    }
                    if (await context.Set<Order>().AnyAsync())
                    {
                        return;
                    }
                    if (await context.Set<OrderProduct>().AnyAsync())
                    {
                        return;
                    }

                    var categories = SeedDataProvider.GetCategories();
                    var products = SeedDataProvider.GetProducts();
                    var users = SeedDataProvider.GetUsers();
                    var orders = SeedDataProvider.GetOrders();
                    var Cart = SeedDataProvider.GetCarts();
                    var CartItem = SeedDataProvider.GetCartItems();
                    var OrderProduct = SeedDataProvider.GetOrderProducts();

                    // insert categories before products to satisfy FK
                    await context.Set<Category>().AddRangeAsync(categories);
                    await context.Set<Product>().AddRangeAsync(products);

                    // ensure users exist before carts/orders
                    await context.Set<ApplicationUser>().AddRangeAsync(users);

                    await context.Set<Cart>().AddRangeAsync(Cart);
                    await context.Set<CartItem>().AddRangeAsync(CartItem);
                    await context.Set<Order>().AddRangeAsync(orders);
                    await context.Set<OrderProduct>().AddRangeAsync(OrderProduct);





                    await context.SaveChangesAsync();

                }

                )
                .UseSeeding((context, _) =>
                {
                    if (context.Set<Product>().Any())
                    {
                        return;
                    }

                    if (context.Set<Category>().Any())
                    {
                        return;
                    }

                    if (context.Set<Cart>().Any())
                    {
                        return;
                    }
                    if (context.Set<CartItem>().Any())
                    {
                        return;
                    }
                    if (context.Set<Order>().Any())
                    {
                        return;
                    }
                    if (context.Set<OrderProduct>().Any())
                    {
                        return;
                    }

                    var categories = SeedDataProvider.GetCategories();
                    var products = SeedDataProvider.GetProducts();
                    var users = SeedDataProvider.GetUsers();
                    var orders = SeedDataProvider.GetOrders();
                    var Cart = SeedDataProvider.GetCarts();
                    var CartItem = SeedDataProvider.GetCartItems();
                    var OrderProduct = SeedDataProvider.GetOrderProducts();

                    if (!context.Set<Category>().Any())
                    {
                        context.Set<Category>().AddRange(categories);
                    }

                    if (!context.Set<Product>().Any())
                    {
                        context.Set<Product>().AddRange(products);
                    }

                    if (!context.Set<ApplicationUser>().Any())
                    {
                        context.Set<ApplicationUser>().AddRange(users);
                    }

                    if (!context.Set<Cart>().Any())
                    {
                        context.Set<Cart>().AddRange(Cart);
                    }

                    if (!context.Set<CartItem>().Any())
                    {
                        context.Set<CartItem>().AddRange(CartItem);
                    }

                    if (!context.Set<Order>().Any())
                    {
                        context.Set<Order>().AddRange(orders);
                    }

                    if (!context.Set<OrderProduct>().Any())
                    {
                        context.Set<OrderProduct>().AddRange(OrderProduct);
                    }

                    context.SaveChanges();

                })
                );


            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IOrderProductRepository, OrderProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICartItemRepository, CartItemRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();





        }
    }
}

