using AspWebTest2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AspWebTest2.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string customerId)
        {
            // Assuming there is a relationship between Customer and Product tables.
            var products = _context.PRODUCT.ToList();
            return View("~/Views/Product/Products.cshtml", products);
        }

        [HttpPost]
        public IActionResult CalculateTotalAmount(string productId, int quantity)
        {
            // Perform the calculation on the server for the specific product
            var product = _context.PRODUCT.FirstOrDefault(p => p.ProductID == productId);
            var totalAmount = quantity * product.Price;

            // Return the result to the client
            return Json(totalAmount);
        }
    }
}