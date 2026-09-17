using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieBookingSystem.Models
{
    public class MovieModel
    {
        public int Movie_ID { get; set; }

        [Required(ErrorMessage = "Movie name is required")]
        [StringLength(100)]
        public string Movie_name { get; set; }

        [Required(ErrorMessage = "Release date is required")]
        [DataType(DataType.Date)]
        public DateTime Release_Date { get; set; }

        [Required(ErrorMessage = "Please select category")]
        public int Cat_ID { get; set; }

        public string Cat_Type { get; set; }

        [Required(ErrorMessage = "Rate is required")]
        [Range(1, 100000,
            ErrorMessage = "Rate must be greater than 0")]
        public decimal rate { get; set; }
        public IEnumerable<SelectListItem> CatList
        {
            get;
            set;
        }
    }
}