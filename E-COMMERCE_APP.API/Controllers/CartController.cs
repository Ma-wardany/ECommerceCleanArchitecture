using E_COMMERCE_APP.API.Bases;
using E_COMMERCE_APP.API.DTOs;
using E_COMMERCE_APP.API.DTOs.Cart;
using E_COMMERCE_APP.Core.Features.Cart.Commands.Models;
using E_COMMERCE_APP.Core.Features.Cart.Queries.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_COMMERCE_APP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : AppController
    {
        [HttpPost("add-cart-item")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddToCart([FromBody]AddToCartDto Dto)
        {
            var UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            var command = new AddCartCommand
            {
                UserId    = UserId != null ? UserId.Value : "",
                ProductId = Dto.ProductId,
                Quantity  = Dto.Quantity
            };

            var response = await Mediator.Send(command);
            return FinalRespons(response);
        }

        [HttpPut("update-cart-item-quantity")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateCartItemQuantity([FromBody]UpdateCartItemQuantityDto Dto)
        {
            var UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            var command = new UpdateCartItemQuantityCommand
            {
                UserId    = UserId != null ? UserId.Value : "",
                ProductId = Dto.ProductId,
                Quantity  = Dto.Quantity
            };

            var response = await Mediator.Send(command);
            return FinalRespons(response);
        }

        [HttpDelete("delete-cart-item")]
        [Authorize(Roles ="User")]
        public async Task<IActionResult> DeleteCartItem([FromQuery] int ProductId)
        {
            var UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            var command = new DeleteCartItemCommand
            {
                UserId = UserId != null ? UserId.Value : "",
                ProductId = ProductId,
            };

            var response = await Mediator.Send(command);
            return FinalRespons(response);
        }


        [HttpGet("get-cart-items")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetCartItems()
        {
            var UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            var query = new GetUserCartQuery
            {
                UserId = UserId != null ? UserId.Value : "",
            };
            var response = await Mediator.Send(query);
            return FinalRespons(response);
        }
    }
}
