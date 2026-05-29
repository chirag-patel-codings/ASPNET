using System;
using System.Collections.Generic;
using Testing.Models;


namespace Testing.Services.ProductRepository
{
    public interface IProductRepository
    {
        public IEnumerable<Product> GetAllProducts();
        public Product? GetProduct(int ProductID);
        public void UpdateProduct(Product product);

        public void InsertProduct(Product product);
        public void DeleteProduct(int productID);
        public IEnumerable<Category> Categories { get; set; }

    }
}
