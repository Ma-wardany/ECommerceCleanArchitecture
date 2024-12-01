using E_COMMERCE_APP.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Data.Entities
{
    public class Product
    {
        public int Id                                   { get; set; }
        public string Name                              { get; set; }
        public string Description                       { get; set; }
        public decimal Price                            { get; set; }
        public int Quantity                             { get; set; }
        public virtual List<ProductImages> Images       { get; set; }
        public virtual List<Review> Reviews             { get; set; }
        public virtual List<Cart> Carts                 { get; set; }
        public int CategoryId                           { get; set; }
        public virtual Category Category                { get; set; }
        public virtual List<OrderProduct> OrderProducts { get; set; }

        

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Product otherProduct = (Product)obj;

            // Compare based on important properties
            return Id == otherProduct.Id &&
                   Name == otherProduct.Name &&
                   Price == otherProduct.Price &&
                   Quantity == otherProduct.Quantity &&
                   CategoryId == otherProduct.CategoryId &&
                   Description == otherProduct.Description;
        }

        public override int GetHashCode()
        {
            // Combine properties that define equality
            return HashCode.Combine(Id, Name, Price, Quantity, CategoryId, Description);
        }

    }
}
