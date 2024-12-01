using E_COMMERCE_APP.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Core.Features.Order.Results
{
    public class PlaceOrderResult
    {
        public int Id { get; set; }
        public string OrderNumber {  get; set; }
        public List<ProductOrderDTO> Products { get; set; }
        public decimal Price { get; set; }
    }

    public class ProductOrderDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
        public int Qunatity { get; set; }
        public List<ProductImages>? Images { get; set; }
    }
}
