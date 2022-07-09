using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace TechStore.Models
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            StoreDbContext context = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<StoreDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!context.Products.Any())
            {
                context.Products.AddRange(
                new Product
                {
                    Name = "PC LEGION",
                    Description = "One of the top gaming PCs we offer",
                    Category = "PC",
                    Price = 700000
                },
                new Product
                {
                    Name = "(92015LC)/P7-C0",
                    Description = "One of the top gaming PCs we offer",
                    Category = "PC",
                    Price = 1899000
                },
                new Product
                {
                    Name = "Aurus Gaming",
                    Description = "One of the top gaming PCs we offer",
                    Category = "PC",
                    Price = 700000
                },
                new Product
                {
                    Name = "LEGION LAPTOP",
                    Description = "One of the top gaming Laptops we offer",
                    Category = "Laptop",
                    Price = 400000
                },
                new Product
                {
                    Name = "Acer Nitro",
                    Description = "One of the top gaming Laptops we offer",
                    Category = "Laptop",
                    Price = 420000
                });
            }
            context.SaveChanges();
        }
    }
}
