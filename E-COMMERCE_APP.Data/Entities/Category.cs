



using E_COMMERCE_APP.Data.Entities;
using System.Collections.Generic;

namespace E_COMMERCE_APP.Data.Entities
{
    public class Category
    {
        public int Id                 { get; set; }
        public string Name            { get; set; }
        public string Description     { get; set; }
        public List<Product> Products { get; set; }
    }
}
