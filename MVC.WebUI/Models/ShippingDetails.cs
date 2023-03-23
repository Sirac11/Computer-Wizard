using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.WebUI.Models
{
    public class ShippingDetails
    {
        
        public string Username { get; set; }
        [Required(ErrorMessage= "Please do not leave this field blank.")]
        public string AddressTitle { get; set; }
        [Required(ErrorMessage = "Please enter your address in this field.")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please enter your city in this field.")]
        public string City { get; set; }
        [Required(ErrorMessage = "Please enter your neighborhood in this field.")]
        public string District { get; set; }
        [Required(ErrorMessage = "Please enter your postal code in this field.")]
        public string PostCode { get; set; }
        
    }
}