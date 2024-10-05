using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Commodity.Models;

namespace Commodity.Data
{
    public class CommodityContext : DbContext
    {
        public CommodityContext (DbContextOptions<CommodityContext> options)
            : base(options)
        {

        }

        public DbSet<Commodity.Models.Product> Product { get; set; } = default!;
        public DbSet<Commodity.Models.ProductCategory> ProductCategory { get; set; } = default!;
        public DbSet<Commodity.Models.Account> Account { get; set; } = default!;
        public DbSet<Commodity.Models.Supplier> Supplier { get; set; } = default!;
        public DbSet<Commodity.Models.ImportOrder> ImportOrder { get; set; } = default!;
        public DbSet<Commodity.Models.ImportOrderDetail> ImportOrderDetail { get; set; } = default!;
      }
}
