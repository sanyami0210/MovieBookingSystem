using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieBookingSystem.Models
{
    public class UserModel
    {
            public int User_ID { get; set; }

            [Required(ErrorMessage = "Username is required")]
            [StringLength(50)]
            public string User_Name { get; set; }

            [Required(ErrorMessage = "Email is required")]
            [EmailAddress(ErrorMessage = "Enter a valid email")]
            public string Email_ID { get; set; }

            public string User_password { get; set; }

            [Required(ErrorMessage = "City is required")]
            public string City { get; set; }

            [Required(ErrorMessage = "Phone number is required")]
            [RegularExpression(
                @"^[0-9]{10}$",
                ErrorMessage = "Phone must contain 10 digits")]
            public string PhoneNo { get; set; }
        }
    }