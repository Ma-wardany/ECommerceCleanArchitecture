using E_COMMERCE_APP.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Core.Features.Products.Query.Results
{
    public class GetProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public int Quantity {  get; set; }
        public List<ProductImages>? Images { get; set; }
    }
}
