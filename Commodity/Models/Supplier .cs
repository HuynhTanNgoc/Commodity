using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Commodity.Models
{
    public class Supplier
    {
        public int SupplierID { get; set; }

        [Required(ErrorMessage = "Supplier name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Supplier address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Supplier phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Supplier email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
    }
}