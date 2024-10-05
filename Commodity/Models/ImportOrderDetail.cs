using Commodity.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Commodity.Models
{
    public class ImportOrderDetail
    {
        public int ImportOrderDetailId { get; set; }

        public int ImportOrderId { get; set; }
        public ImportOrder ImportOrder { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Total { get; set; }
    }
}
