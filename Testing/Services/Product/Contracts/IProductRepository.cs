using System;
using System.Collections.Generic;
using Testing.Models;


namespace Testing.Services.ProductRepository
{
    public interface IProductRepository
    {
        public IEnumerable<Product> GetAllProducts();
        public Product GetProduct(int ProductID);
    }
}
