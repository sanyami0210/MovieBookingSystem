using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieBookingSystem.Models
{
    public class BookingModel
    {    
            public int booking_ID { get; set; }

            public int User_ID { get; set; }

            [Required(ErrorMessage = "Select category")]
            public int Cat_ID { get; set; }

            [Required(ErrorMessage = "Select movie")]
            public int Movie_ID { get; set; }

            [Required(ErrorMessage = "Enter number of tickets")]
            [Range(1, 10,
                ErrorMessage = "Tickets must be between 1 and 10")]
            public int no_of_Tickets { get; set; }

            public decimal amount { get; set; }

            public IEnumerable<SelectListItem> CatList
            {
                get;
                set;
            }

            public IEnumerable<SelectListItem> MovieList
            {
                get;
                set;
            }
        }
    }