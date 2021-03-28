using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsEcommerce.Models
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            // get the ApplicationDbContext object through IApplicationBuilder
            ApplicationDbContext context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // if there are any pending migrations then migrate
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product
                    {
                        Name = "Titanic ship",
                        Description = "a boat for millions",
                        Category = "Watersports",
                        Price = 10000
                    },
                    new Product
                    {
                        Name = "Lifejacket",
                        Description = "protective",
                        Category = "Watersports",
                        Price = 99.9m
                    },
                    new Product
                    {
                        Name = "Football ball",
                        Description = "A ball that is approved by Fifa for world cup",
                        Category = "Soccer",
                        Price = 50.8m
                    },
                    new Product
                    {
                        Name = "Chess board",
                        Description = "chess board to play with",
                        Category = "Chess",
                        Price = 12.5m
                    },
                    new Product
                    {
                        Name = "Diamon King player",
                        Description = "Diamon King player",
                        Category = "Chess",
                        Price = 5.6m
                    },
                    new Product
                    {
                        Name = "Stadium",
                        Description = "to play in",
                        Category = "Soccer",
                        Price = 6045.88m
                    }
                    );

                context.SaveChanges();
            }
        }
    }
}
