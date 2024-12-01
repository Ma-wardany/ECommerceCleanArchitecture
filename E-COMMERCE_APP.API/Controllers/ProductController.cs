using E_COMMERCE_APP.API.Bases;
using E_COMMERCE_APP.Core.Features.Products.Command.Models;
using E_COMMERCE_APP.Core.Features.Products.Query.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_COMMERCE_APP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : AppController
    {

        [HttpPost("add-product")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddProoduct([FromForm]AddProductCommand product)
        {

            var response = await Mediator.Send(product);
            return FinalRespons(response);

        }

        [HttpDelete("delete-product")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProoduct([FromQuery]DeleteProductCommand Id)
        {

            var response = await Mediator.Send(Id);
            return FinalRespons(response);

        }

        [HttpPut("update-product")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProoduct([FromForm] UpdateProductCommand product)
        {

            var response = await Mediator.Send(product);
            return FinalRespons(response);

        }

        
        [HttpGet("paginated-products-by-categoryId")]
        public async Task<IActionResult> GetProductByCategoryId([FromQuery] GetProductsByCategoryQuery query)
        {
            var response = await Mediator.Send(query);
            return FinalRespons(response);
        }

        
        [HttpGet("get-product-by-Id/{Id}")]
        public async Task<IActionResult> GetProductById([FromRoute] GetProductByIdQuery request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await Mediator.Send(request);
            return FinalRespons(response);
        }


        [HttpGet("get-product-by-name")]
        public async Task<IActionResult> GetProductByName([FromQuery] GetProductByNameQuery request)
        {
            var response = await Mediator.Send(request);
            return FinalRespons(response);
        }
    }
}
