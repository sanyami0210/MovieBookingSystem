using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieBookingSystem.Models
{
    public class CategoryModel
    {
        public int Cat_ID { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [StringLength(50,
            ErrorMessage = "Maximum 50 characters allowed")]
        public string Cat_Type { get; set; }
    }
}