using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Testing.Models;
using Testing.Services.ProductRepository;


namespace Testing.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _repo;

        public ProductController(IProductRepository repo)
        {
            this._repo = repo;
        }

        [Route("Product/{id:int?}")]
        // [HttpGet("Product/{id:int?}")]   -- ALSO WORKS
        public IActionResult Index(string id)
        {
            if (id == null)
            {
                var products = _repo.GetAllProducts();
                return View(products);
            }
            else
            {
                return RedirectToAction("ViewProduct", "Product", new { id });
            }
        }

        // Displays individual product if product details found in the database otherwise navigates to the "Errors" View with error details. 
        public IActionResult ViewProduct(string id)
        {
            
            var productQuery = GetProduct(id);

            if (productQuery.Item1 != null)
            {
                return View(productQuery.Item1);
            }
            else
            {
                string message = $"Product with ProductID: {id} NOT found!!!\n{productQuery.Item2}";
                return View("Errors", message);
            }
            
        }

        // Displays the Product details in the "UpdateProduct" that can be modified by the user, if product details found in the database otherwise navigates to the "Errors" View with error details.
        public IActionResult UpdateProduct(string id)
        {

            var productQuery = GetProduct(id);

            if (productQuery.Item1 != null)
            {
                return View(productQuery.Item1);
            }
            else
            {
                string message = $"Product with ProductID: {id} NOT found!!!\n{productQuery.Item2}";
                return View("Errors", message);
            }

        }

        // Saves the Product details to the database and returns to the "ViewProduct" if no validation errors otherwise returns to the "UpdateProduct" View with errors.
        public IActionResult SaveProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Name = product.Name.Trim();
                _repo.UpdateProduct(product);
                return RedirectToAction("ViewProduct", new { id = product.ProductID });
            }
            else
            {
                return View("UpdateProduct", product);
            }
            
        }

        /// <summary>
        /// This function returns the Product details by it's supplied 'id' if no errors in this function and Retruns 'null' for the 'string' argument.
        /// Incase of any errors (like productid parsing, or product details not found in database), it returns the 'null' for its Product parameter and Error Details for it's string argument.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private (Product?, string) GetProduct(string id)
        {
            int productID = -1;
            Product? product = null;

            try
            {
                productID = int.Parse(id);
                product = _repo.GetProduct(productID);

            }
            catch (Exception ex)
            {
                return (product, ex.Message);

            }
            return (product, null);
        }

    }
}
