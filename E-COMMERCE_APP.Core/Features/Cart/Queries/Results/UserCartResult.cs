using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Core.Features.Cart.Queries.Results
{
    public class UserCartResult
    {
        public string UserId { get; set; }
        public decimal TotalPayment { get; set; }
        public List<CartProduct> Products { get; set; }
    }

    public class CartProduct
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
