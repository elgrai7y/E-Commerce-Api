using System;
using System.Collections.Generic;

namespace E_Commerce.DAL
{
    public class SeedDataProvider
    {
        // Seeding
        public static List<Category> GetCategories()
        {
            var createdDate = new DateTime(2026, 3, 1, 10, 30, 0);

            var c1 = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var c2 = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var c3 = Guid.Parse("33333333-3333-3333-3333-333333333333");

            return new List<Category>()
            {
                new Category { Id = c1, Name = "Electronics", Description = "Electronic devices and accessories", CreatedAt = createdDate },
                new Category { Id = c2, Name = "Accessories", Description = "Computer and phone accessories", CreatedAt = createdDate },
                new Category { Id = c3, Name = "Audio", Description = "Headphones and audio equipment", CreatedAt = createdDate },
            };
        }

        public static List<Product> GetProducts()
        {
            var createdDate = new DateTime(2026, 3, 1, 10, 30, 0);

            var c1 = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var c2 = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var c3 = Guid.Parse("33333333-3333-3333-3333-333333333333");

            var p1 = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            var p2 = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
            var p3 = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc");
            var p4 = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd");
            var p5 = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee");
            var p6 = Guid.Parse("66666666-1111-1111-1111-666666666666");
            var p7 = Guid.Parse("11111111-2222-3333-4444-555555555555");

            return new List<Product>
            {
                new Product { Id = p1, Title = "Laptop", Description = "High-performance gaming laptop", Price = 1299.99m, Stock = 15, CategoryId = c1, CreatedAt = createdDate, Reviews = 10, Rate = 4.6f, Unit = "pcs" },
                new Product { Id = p2, Title = "Wireless Mouse", Description = "Ergonomic wireless mouse with USB receiver", Price = 29.99m, Stock = 50, CategoryId = c2, CreatedAt = createdDate, Reviews = 45, Rate = 4.2f, Unit = "pcs" },
                new Product { Id = p3, Title = "Mechanical Keyboard", Description = "RGB backlit mechanical keyboard", Price = 89.99m, Stock = 30, CategoryId = c2, CreatedAt = createdDate, Reviews = 25, Rate = 4.4f, Unit = "pcs" },
                new Product { Id = p4, Title = "USB-C Hub", Description = "7-in-1 USB-C hub with HDMI and SD card reader", Price = 45.50m, Stock = 25, CategoryId = c1, CreatedAt = createdDate, Reviews = 8, Rate = 4.1f, Unit = "pcs" },
                new Product { Id = p5, Title = "Noise Cancelling Headphones", Description = "Over-ear wireless headphones with active noise cancellation", Price = 199.99m, Stock = 12, CategoryId = c3, CreatedAt = createdDate, Reviews = 30, Rate = 4.7f, Unit = "pcs" },
                new Product { Id = p6, Title = "Webcam 1080p", Description = "Full HD webcam with built-in microphone", Price = 69.99m, Stock = 20, CategoryId = c1, CreatedAt = createdDate, Reviews = 14, Rate = 4.0f, Unit = "pcs" },
                new Product { Id = p7, Title = "Portable SSD 1TB", Description = "External solid-state drive with USB 3.2", Price = 149.99m, Stock = 18, CategoryId = c2, CreatedAt = createdDate, Reviews = 22, Rate = 4.5f, Unit = "pcs" }
            };
        }

        //public static List<ApplicationRole> GetRoles()
        //{
        //    var r1 = Guid.Parse("aaaaaaaa-1111-2222-3333-aaaaaaaaaaaa");
        //    var r2 = Guid.Parse("bbbbbbbb-1111-2222-3333-bbbbbbbbbbbb");

        //    return new List<ApplicationRole>
        //    {
        //        new ApplicationRole { Id = r1, Name = "Admin", NormalizedName = "ADMIN", Description = "Administrator role" },
        //        new ApplicationRole { Id = r2, Name = "User", NormalizedName = "USER", Description = "Regular user role" }
        //    };
        //}

        public static List<ApplicationUser> GetUsers()
        {
            var u1 = Guid.Parse("99999999-9999-9999-9999-999999999999");
            var u2 = Guid.Parse("88888888-8888-8888-8888-888888888888");

            return new List<ApplicationUser>
            {
                new ApplicationUser { Id = u1, UserName = "alice", NormalizedUserName = "ALICE", Email = "alice@example.com",City="cairo",Country="Egypt", EmailConfirmed = true },
                new ApplicationUser { Id = u2, UserName = "bob", NormalizedUserName = "BOB", Email = "bob@example.com",City="Portsaid",Country="Egypt", EmailConfirmed = true }
            };
        }

        public static List<Cart> GetCarts()
        {
            var createdDate = new DateTime(2026, 3, 1, 10, 30, 0);
            var cart1 = Guid.Parse("77777777-7777-7777-7777-777777777777");
            var user1 = Guid.Parse("99999999-9999-9999-9999-999999999999");

            return new List<Cart>
            {
                new Cart { Id = cart1, UserId = user1, CreatedAt = createdDate }
            };
        }

        public static List<CartItem> GetCartItems()
        {
            var createdDate = new DateTime(2026, 3, 1, 10, 30, 0);
            var cart1 = Guid.Parse("77777777-7777-7777-7777-777777777777");
            var p1 = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            var p2 = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

            return new List<CartItem>
            {
                new CartItem { Id = Guid.NewGuid(), CartId = cart1, ProductId = p1, Quantity = 1, CreatedAt = createdDate },
                new CartItem { Id = Guid.NewGuid(), CartId = cart1, ProductId = p2, Quantity = 2, CreatedAt = createdDate }
            };
        }

        public static List<Order> GetOrders()
        {
            var createdDate = new DateTime(2026, 3, 2, 12, 0, 0);
            var order1 = Guid.Parse("66666666-6666-6666-6666-666666666666");
            var user1 = Guid.Parse("99999999-9999-9999-9999-999999999999");

            return new List<Order>
            {
                new Order { Id = order1, OrderDate = createdDate, TotalAmount = 1529.98m, Status = "Processing", ShippingAddress = "123 Main St", City = "City", Country = "Country", UserId = user1, CreatedAt = createdDate }
            };
        }

        public static List<OrderProduct> GetOrderProducts()
        {
            var createdDate = new DateTime(2026, 3, 2, 12, 0, 0);
            var order1 = Guid.Parse("66666666-6666-6666-6666-666666666666");
            var p1 = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            return new List<OrderProduct>
            {
                new OrderProduct { Id = Guid.NewGuid(), OrderId = order1, ProductId = p1, Quantity = 1, TotalPrice = 1299.99m, CreatedAt = createdDate }
            };
        }
    }
}
