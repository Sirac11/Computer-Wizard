using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.WebUI.Entity
{
    public class Category
    {
        public int Id { get; set; }

        [DisplayName("Category Name")]
        [StringLength(maximumLength:50, ErrorMessage = "You can add up to 50 characters.")]
        public string Name { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }

        public List<Product> Products { get; set; }
    }
}