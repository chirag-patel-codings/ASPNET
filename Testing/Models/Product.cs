using System;
using System.ComponentModel.DataAnnotations;


namespace Testing.Models
{
    public class Product
    {
        public Product()
        {
        }


        public int ProductID { get; set; }

        [Required(ErrorMessage = "Product Name is required!!!" )]
        public string Name { get; set; }

        [Required(ErrorMessage = "Product Price is required!!!")]
        public double? Price { get; set; }  // made nullable to display above error message...
        public int CategoryID { get; set; }
        public int OnSale { get; set; }
        public int StockLevel { get; set; }
    }
}