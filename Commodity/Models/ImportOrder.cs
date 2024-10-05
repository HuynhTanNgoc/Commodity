using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Commodity.Models
{
    public class ImportOrder
    {
        [Key]
        public int ImportOrderID { get; set; }
        public string ImportOrderName { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "The ImportDate field is required")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime ImportDate { get; set; }
        //public List<ImportOrderDetail> ImportOrderDetails { get; set; }
    }
}
