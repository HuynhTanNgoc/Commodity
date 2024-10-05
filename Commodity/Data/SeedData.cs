using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Commodity.Models;

namespace Commodity.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CommodityContext(
                serviceProvider.GetRequiredService<DbContextOptions<CommodityContext>>()))
            {
                context.Database.EnsureCreated();

                if (context.Supplier.Any())
                {
                    return;
                }

                // Add sample suppliers
                var suppliers = new Supplier[]
                {
                    new Supplier
                    {
                        Name = "Supplier 1",
                        Address = "123 ABC Street",
                        Phone = "1234567890",
                        Email = "supplier1@gmail.com"
                    },
                    new Supplier
                    {
                        Name = "Supplier 2",
                        Address = "456 XYZ Avenue",
                        Phone = "9876543210",
                        Email = "supplier2@gmail.com"
                    }
                };

                try
                {
                    context.Supplier.AddRange(suppliers);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    //
                }
            }
        }
    }
}
