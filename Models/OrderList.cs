// Models/OrderList.cs
namespace AspWebTest2.Models
{
    public class ORDERLIST
    {
        public int OrderID { get; set; }
        public string CustomerID { get; set; }
        public Customer Customer { get; set; }

        public int TotalAmount { get; set; }
    }
}