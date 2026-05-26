using Dapper;
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

        public Product GetProduct(int productID)
        {
            
            return _conn.QuerySingle<Product>("SELECT * FROM Products WHERE ProductID = @id", new {id = productID});
        }
    }
}
