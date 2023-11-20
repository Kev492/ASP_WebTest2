using AspWebTest2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace AspWebTest2.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductController> _logger;

        public ProductController(ApplicationDbContext context, ILogger<ProductController> logger)
        {
            _context = context;
            _logger = logger;
        }


        public IActionResult Index(string customerId)
        {
            var customer = _context.CUSTOMER.FirstOrDefault(c => c.CustomerID == customerId);

            if (customer != null)
            {
                var products = _context.PRODUCT.ToList();
                var model = Tuple.Create(products, customer);

                return View("~/Views/Product/Products.cshtml", model);
            }
            else
            {
                // Handle the case where the customer is not found
                return View("~/Views/Product/CustomerNotFound.cshtml");
            }
        }
        [HttpPost]
        public IActionResult Buy(int totalAmount, string customerId)
        {
            try
            {
                if (string.IsNullOrEmpty(customerId))
                {
                    return BadRequest("Invalid customerId");
                }

                int orderId = GenerateOrderId(); // Use a larger data type for OrderID

                // Create a new ORDERLIST object
                var order = new ORDERLIST
                {
                    OrderID = orderId,
                    CustomerID = customerId,
                    TotalAmount = totalAmount
                };

                // Add the new order to the context
                _context.ORDERLIST.Add(order);

                // Save changes to the database
                _context.SaveChanges();

                // Return the OrderID to the client
                return Json(new { orderId = order.OrderID });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during purchase");

                // Provide a more detailed error message to the client
                return BadRequest($"An error occurred during purchase. Details: {ex.Message}");
            }
        }

        private int GenerateOrderId()
        {
            // Use a larger data type for OrderID (long) and include milliseconds for additional uniqueness
            return (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }

    }
}