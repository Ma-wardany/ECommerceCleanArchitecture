using E_COMMERCE_APP.Data.Entities.Identity;
using System;
using System.Collections.Generic;

namespace E_COMMERCE_APP.Data.Entities
{
    public class Review
    {
        public int Id                          { get; set; }
        public string Comment                  { get; set; }
        public int Rate                        { get; set; }
        public DateTime dateTime               { get; set; } = DateTime.Now;
        public string UserId                   { get; set; }
        public ApplicationUser applicationUser { get; set; }
        public int ProductId                   { get; set; }
        public Product Product                 { get; set; }
    }
}
