using E_COMMERCE_APP.Data.Entities;
using E_COMMERCE_APP.Data.Entities.Identity;
using E_COMMERCE_APP.Infrastructure.Repositories.Implementations;
using E_COMMERCE_APP.Infrastructure.Repositories.Abstracts;
using E_COMMERCE_APP.Services.Abstracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace E_COMMERCE_APP.Services.ServicesImp
{
    public class OrderServices : IOrderServices
    {
        private readonly IOrderRepository orderRepository;
        private readonly ICartRepository cartRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IProductRepository productRepository;

        public OrderServices(IOrderRepository orderRepository,
                             ICartRepository cartRepository,
                             UserManager<ApplicationUser> userManager,
                             IProductRepository productRepository)
        {
            this.orderRepository = orderRepository;
            this.cartRepository = cartRepository;
            this.userManager = userManager;
            this.productRepository = productRepository;
        }


        public async Task<(Order?, string?)> PlaceOrder(string UserId)
        {
            var user = await userManager.FindByIdAsync(UserId);
            if (user == null) return (null, "NotFoundUser");

            var UserCart = await cartRepository
                .GetTableAsTracking()
                .Include(c => c.Product)
                .ThenInclude(p => p.Images)
                .Include(p => p.Product.Category)
                .Where(c => c.UserId == UserId)
                .ToListAsync();

            if (!UserCart.Any()) return (null, "EmptyCart");

            var Products = UserCart.Select(c => c.Product).ToList();

            foreach(var item in UserCart)
            {
                var product = Products.FirstOrDefault(p => p.Id == item.ProductId);

                if (product.Quantity < item.Quantity)
                {
                    return (null, "NoQuantity");
                }
            }

            var order = new Order
            {
                UserId        = UserId,
                Status        = "Pending",
                Date          = DateTime.UtcNow,
                OrderNumber   = new Random().Next(10000000, 99999999).ToString(),
                Price         = UserCart.Sum(c => c.Quantity * c.Product.Price),
                OrderProducts = UserCart.Select(cart => new OrderProduct
                {
                    ProductId = cart.ProductId,
                    Quantity  = cart.Quantity,
                }).ToList()
            };

            

            using var Trans = cartRepository.BeginTransaction();
            try
            {
                await orderRepository.AddAsync(order);

                var updatedProducts = Products.Select(p =>
                {
                    var cartItem = UserCart.FirstOrDefault(c => c.ProductId == p.Id);
                    p.Quantity  -= cartItem.Quantity;
                    return p;
                    
                }).ToList();

                await productRepository.UpdateRangeAsync(updatedProducts);
                await cartRepository.DeleteRangeAsync(UserCart.ToList());
                Trans.Commit();
                
                return (order, null);
            }
            catch (Exception ex)
            {
                
                await Trans.RollbackAsync();
                Console.WriteLine($"Errors : {ex.Message}");
                return (null, "Failed");
            }
        }
    }
}