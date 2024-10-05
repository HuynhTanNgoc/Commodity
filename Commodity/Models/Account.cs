using Microsoft.CodeAnalysis.Scripting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System;
using BCrypt.Net;

namespace Commodity.Models
{
    public class Account
    {
        public int AccountID { get; set; }

        [Required(ErrorMessage = "Full name cannot be left blank")]
        public string FullName { get; set; }

        public string Gender { get; set; }

        [Required(ErrorMessage = "Password can not be blank")]
        [MinLength(8, ErrorMessage = "Password must have at least 8 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [NotMapped]
        [Compare("Password", ErrorMessage = "Confirm passwords do not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Phone number can not be left blank")]
        public int Phone { get; set; }

        [Required(ErrorMessage = "Email cannot be blank")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Date of birth cannot be left blank")]
        public DateTime Birth { get; set; }

        [Required(ErrorMessage = "Address cannot be left blank")]
        public string Address { get; set; }

        public DateTime LastLogin { get; set; }
        public DateTime CreateDate { get; set; }

        public Account()
        {
            CreateDate = DateTime.Now;
        }

        public void HashPassword()
        {
            Password = BCrypt.Net.BCrypt.HashPassword(Password, BCrypt.Net.BCrypt.GenerateSalt());
        }

        // Verify password against hashed password
        public bool VerifyPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, Password);
        }
    }
}
