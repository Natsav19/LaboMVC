using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MVCPlat.Data;
using MVCPlat.Models;
using System;
using System.Linq;

namespace MVCPlat.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MVCPlatContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MVCPlatContext>>()))
            {
                if (context.Cosplay.Any())
                {
                    return;   // DB has been seeded
                }

                context.Cosplay.AddRange(
                    new Cosplays
                    {
                        Title = "Magicien",
                        ReleaseDate = DateTime.Parse("1989-2-12"),
                        Genre = "Magie",
                        Price = 7.99M,
                        Rating = "100%"
                    },

                    new Cosplays
                    {
                        Title = "Fantome",
                        ReleaseDate = DateTime.Parse("1984-3-13"),
                        Genre = "Monstre",
                        Price = 8.99M,
                        Rating = "100%"
                    },

                    new Cosplays
                    {
                        Title = "Fury",
                        ReleaseDate = DateTime.Parse("1986-2-23"),
                        Genre = "Informatique",
                        Price = 9.99M,
                        Rating = "100%"
                    },

                   new Cosplays
                   {
                        Title = "Elvis",
                        ReleaseDate = DateTime.Parse("1959-4-15"),
                        Genre = "Chanteur",
                        Price = 3.99M,
                       Rating = "10%"
                   }
                );
                context.SaveChanges();
            }
        }
    }
}