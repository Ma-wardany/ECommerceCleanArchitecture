using E_COMMERCE_APP.Data.Entities;
using E_COMMERCE_APP.Data.Entities.Identity;
using E_COMMERCE_APP.Infrastructure.Repositories.Abstracts;
using E_COMMERCE_APP.Services.Abstracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Services.ServicesImp
{
    public class CartServices : ICartServices
    {
        private readonly ICartRepository cartRepository;
        private readonly IProductRepository productRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public CartServices(ICartRepository cartRepository,
                            IProductRepository productRepository,
                            UserManager<ApplicationUser> userManager)
        {
            this.cartRepository    = cartRepository;
            this.productRepository = productRepository;
            this.userManager       = userManager;
        }

        public async Task<string> AddCartItem(Cart cart)
        {
            if (cart == null) return "NotFound";

            var product = await productRepository.GetTableNoTracking()
                .SingleOrDefaultAsync(p => p.Id == cart.ProductId);

            if (product == null) return "NotFoundProduct";
            if (product.Quantity < cart.Quantity) return "NoQuatity";

            var existCartItem = await cartRepository.GetTableAsTracking()
                .SingleOrDefaultAsync(c => c.ProductId == cart.ProductId && c.UserId == cart.UserId);


            using var Trans = cartRepository.BeginTransaction();
            try
            {
                if (existCartItem != null)
                {
                    if ((cart.Quantity + existCartItem.Quantity) > product.Quantity)
                        return "NoQuatity";

                    existCartItem.Quantity += cart.Quantity;

                    await cartRepository.UpdateAsync(existCartItem);
                    await Trans.CommitAsync();
                    return "Updated";
                }

                await cartRepository.AddAsync(cart);

                await Trans.CommitAsync();
                return "Added";
            }
            catch (Exception ex)
            {
                await Trans.RollbackAsync();
                Console.WriteLine($"Errors : {ex.Message}---------------------");
                return "Failed";
            }
        }

        public async Task<string> DeleteCartItem(int productId, string userId)
        {
            var cart = await cartRepository.GetTableAsTracking()
                .FirstOrDefaultAsync(c => c.ProductId == productId && c.UserId == userId);

            if (cart == null) return "NotFound";

            using var Trans = cartRepository.BeginTransaction();
            try
            {
                await cartRepository.DeleteAsync(cart);
                await Trans.CommitAsync();
                return "Success";
            }
            catch(Exception ex)
            {
                await Trans.RollbackAsync();
                Console.WriteLine($"Error deleting cart item: {ex.Message}");
                return "Failed";
            }
        }

        public async Task<string> UpdateCartItemQuantity(Cart cart)
        {
            var existCart = await cartRepository.GetTableAsTracking()
                .SingleOrDefaultAsync(c => c.ProductId == cart.ProductId && c.UserId == cart.UserId);

            var product = await productRepository.GetTableNoTracking()
                .SingleOrDefaultAsync(p => p.Id == cart.ProductId);

            if (existCart == null) 
                return "NotFound";

            if (existCart.Quantity == cart.Quantity) 
                return "NoUpdates";

            existCart.Quantity = cart.Quantity;
            if (existCart.Quantity > product.Quantity) return "NoQuatity";

            using var Trans = cartRepository.BeginTransaction();
            try
            {
                await cartRepository.UpdateAsync(existCart);
                await Trans.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await Trans.RollbackAsync();
                Console.WriteLine($"Erors : {ex.Message}-------------");
                return "Failed";
            }
        }

        public async Task<(List<Cart>?, string?)> GetUserCarts(string UserId)
        {
            var user = await userManager.FindByIdAsync(UserId);

            if (user == null) return (null, "NotFoundUser");
            try
            {
                var Carts = await cartRepository.GetTableNoTracking()
                .Include(c => c.Product)
                    .ThenInclude(p => p.Images)
                .Include(c => c.Product.Category)
                .Include(c => c.applicationUser)
                .Where(c => c.UserId == user.Id)
                .ToListAsync();

                if (!Carts.Any()) return (null, "EmptyCart");

                return (Carts, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error : {ex.Message} --------------------------");
                return (null, ex.Message);
            }
            


        }
    }
}
