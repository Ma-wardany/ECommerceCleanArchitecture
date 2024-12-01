using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Data.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string? Country { get; set; }

        [InverseProperty(nameof(UserRefreshToken.applicationUser))]
        public List<UserRefreshToken> UserRefreshTokens { get; set; }
        public virtual List<Cart> Carts { get; set; }
        public virtual List<Order> Orders { get; set; }

        [InverseProperty(nameof(Review.applicationUser))]
        public virtual List<Review> Reviews { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
