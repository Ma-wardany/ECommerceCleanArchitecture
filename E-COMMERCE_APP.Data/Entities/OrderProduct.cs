using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Data.Entities
{
    public class OrderProduct
    {
        public int Id          { get; set; }
        public int ProductId   { get; set; }
        public Product product { get; set; }
        public int OrderId     { get; set; }
        public Order order     { get; set; }
        public int Quantity    { get; set; }
    }
}
