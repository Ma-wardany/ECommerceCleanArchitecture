using E_COMMERCE_APP.API.Bases;
using E_COMMERCE_APP.Core.Features.Cart.Commands.Models;
using E_COMMERCE_APP.Core.Features.Order.Commands.Models;
using E_COMMERCE_APP.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_COMMERCE_APP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : AppController
    {
        [HttpPost("place-order")]
        [Authorize(Roles ="User")]
        public async Task<IActionResult> PlaceOrder()
        {
            var UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            var command = new PlaceOrderCommand
            {
                UserId = UserId != null ? UserId.Value : "",
            };
            var response = await Mediator.Send(command);

            return FinalRespons(response);
        }
    }
}
