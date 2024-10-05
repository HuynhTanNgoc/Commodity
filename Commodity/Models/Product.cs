using System.ComponentModel.DataAnnotations.Schema;

namespace Commodity.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int ProductCategoryId { get; set; }
        [ForeignKey("ProductCategoryId")]
        public ProductCategory ProductCategory { get; set; }

        public int SupplierID { get; set; }
        public Supplier Supplier { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }

      //  public byte[] ImageFile { get; set; }
    }
}
