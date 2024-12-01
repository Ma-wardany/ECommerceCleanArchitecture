using E_COMMERCE_APP.Data.Entities.Identity;

namespace E_COMMERCE_APP.Data.Entities
{
    public class Cart
    {
        public string UserId                   { get; set; }
        public ApplicationUser applicationUser { get; set; }
        public int ProductId                   { get; set; }
        public Product Product                 { get; set; }
        public int Quantity                    { get; set; }
        public DateTime Date                   { get; set; }
    }
}
