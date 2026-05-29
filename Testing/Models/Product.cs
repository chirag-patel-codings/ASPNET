using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Testing.Models
{
    public class Product
    {
        public Product()
        {
        }


        public int? ProductID { get; set; }

        [Required(ErrorMessage = "* Required: Product Name is required!!!" )]
        public string Name { get; set; }

        [Required(ErrorMessage = "* Required: Product Price is required!!!")]
        public double? Price { get; set; }  // made nullable to display above error message...
        
        [Required(ErrorMessage = "* Required: CategoryID is Required!!!")]
        public int? CategoryID { get; set; }  // made nullable to display above error message...
        
        public int OnSale { get; set; }
        public int StockLevel { get; set; }

        public IEnumerable<Category>? Categories { get; set; } = null;
    }
}