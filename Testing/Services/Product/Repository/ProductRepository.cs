using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Testing.Models;

namespace Testing.Services.ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnection _conn;
        
        public ProductRepository(IDbConnection conn) 
        {
            _conn = conn;
        }
        public IEnumerable<Product> GetAllProducts()
        {
            return _conn.Query<Product>("SELECT * FROM Products"); ;
        }

        public Product? GetProduct(int productID)
        {
            
            Product? product = _conn.QuerySingleOrDefault<Product>("SELECT * FROM Products WHERE ProductID = @id", new { id = productID });
            return product;
        }

        void IProductRepository.UpdateProduct(Product product)
        {
            _conn.Execute("UPDATE Products SET Name = @name, Price = @price WHERE ProductID = @id;", 
                new { id = product.ProductID, name = product.Name, price = product.Price });
        }
    }
}
