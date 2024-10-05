using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Commodity.Models
{
    public class ProductIndexViewModel
    {
        public List<Product> Products { get; set; }
        public SelectList ProductCategories { get; set; }
        public string? ProductCategory { get; set; }
        public string? SearchString { get; set; }
    }
}
