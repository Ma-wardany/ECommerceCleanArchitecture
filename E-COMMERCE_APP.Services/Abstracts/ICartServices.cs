using E_COMMERCE_APP.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Services.Abstracts
{
    public interface ICartServices
    {
        public Task<string>                 AddCartItem(Cart cart);
        public Task<string>                 DeleteCartItem(int productId, string userId);
        public Task<string>                 UpdateCartItemQuantity(Cart cart);
        public Task<(List<Cart>?, string?)> GetUserCarts(string UserId);
    }
}
