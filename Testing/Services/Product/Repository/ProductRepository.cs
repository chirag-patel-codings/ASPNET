using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Testing.Models;
using ZstdSharp.Unsafe;

namespace Testing.Services.ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnection _conn;
        private readonly IEnumerable<Category> _categories;
        
        public ProductRepository(IDbConnection conn) 
        {
            _conn = conn;
            _categories = GetAllCategories();
        }
        public IEnumerable<Product> GetAllProducts()
        {
            return _conn.Query<Product>("SELECT * FROM Products");
        }

        public Product? GetProduct(int productID)
        {
            Product? product = _conn.QuerySingleOrDefault<Product>("SELECT * FROM Products WHERE ProductID = @id", new { id = productID });
            product.Categories = _categories;
            return product;
        }

        public void UpdateProduct(Product product)
        {
            _conn.Execute("UPDATE Products SET Name = @name, Price = @price WHERE ProductID = @id;", 
                new { id = product.ProductID, name = product.Name, price = product.Price });
        }

        public void InsertProduct(Product product)
        {
            string sql = "INSERT INTO Products (Name, Price, CategoryID, OnSale, StockLevel) " +
                            "VALUES (@name, @price, @categoryID, @onSale, @stockLevel);";
            _conn.Execute(sql,
                new
                {
                    name = product.Name, price = product.Price, categoryID = product.CategoryID,
                    onSale = product.OnSale, stockLevel = product.StockLevel
                });
        }

        public void DeleteProduct(int productID)
        {
            string sql = "DELETE FROM reviews WHERE ProductID = @id; DELETE FROM sales WHERE ProductID = @id; DELETE FROM products WHERE ProductID = @id;";
            _conn.Execute(sql, new { id = productID });
        }
        
        public IEnumerable<Category> Categories 
        {
            get => _categories;
            set
            {
                value = _categories;
            } 
        }
        
        private IEnumerable<Category> GetAllCategories()
        {
            var categories = _conn.Query<Category>("SELECT * FROM Categories;");
            // categories = categories.Prepend(new Category() { CategoryID = 0, Name = "None" });
            return categories;
        }
        
    }
}
