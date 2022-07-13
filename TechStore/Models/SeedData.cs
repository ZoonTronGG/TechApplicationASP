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
                    Price = 70000.00m
                },
                new Product
                {
                    Name = "(92015LC)/P7-C0",
                    Description = "One of the top gaming PCs we offer",
                    Category = "PC",
                    Price = 18990000.00m
                },
                new Product
                {
                    Name = "Aurus Gaming",
                    Description = "One of the top gaming PCs we offer",
                    Category = "PC",
                    Price = 70000.00m
                },
                new Product
                {
                    Name = "LEGION LAPTOP",
                    Description = "One of the top gaming Laptops we offer",
                    Category = "Laptop",
                    Price = 40000.00m
                },
                new Product
                {
                    Name = "Acer Nitro",
                    Description = "One of the top gaming Laptops we offer",
                    Category = "Laptop",
                    Price = 42000000.00m
                });
            }
            context.SaveChanges();
        }
    }
}
