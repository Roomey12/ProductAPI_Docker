using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPI_Docker.Models
{
    public static class PrepDB
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using(var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<ApplicationContext>());
            }
        }

        public static void SeedData(ApplicationContext context)
        {
            Console.WriteLine("Applying Migrations...");
            context.Database.Migrate();
            if (!context.Products.Any())
            {
                Console.WriteLine("Adding data - seeding...");
                context.Products.AddRange(
                    new Product() { Name = "Milk", Price = 7 },
                    new Product() { Name = "Egg", Price = 1 },
                    new Product() { Name = "Bread", Price = 5 },
                    new Product() { Name = "Tomato", Price = 4 }
                    );
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Already have data - not seeding");
            }
        }
    }
}
