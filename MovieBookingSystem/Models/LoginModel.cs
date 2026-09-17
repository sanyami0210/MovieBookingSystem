using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieBookingSystem.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string User_Name { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string User_password { get; set; }
    }
}