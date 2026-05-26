using Dapper;
using System.Collections.Generic;
using System.Data;
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
            return _conn.Query<Product>("SELECT * FROM Products");
        }

    }
}
