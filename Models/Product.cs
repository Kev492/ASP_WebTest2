// Models/Product.cs
namespace AspWebTest2.Models
{
    public class Product
    {
        public string ProductID { get; set; }
        public int Price { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public int TotalStock { get; set; }
    }
}