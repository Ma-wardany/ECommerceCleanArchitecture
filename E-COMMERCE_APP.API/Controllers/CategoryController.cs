using E_COMMERCE_APP.API.Bases;
using E_COMMERCE_APP.Core.Features.Categories.Commands.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_COMMERCE_APP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : AppController
    {

        [HttpPost("add-category")]
        public async Task<IActionResult> AddCategory([FromBody]AddCtegoryCommand category)
        {
            var response = await Mediator.Send(category);
            return FinalRespons(response);
        }

        [HttpPut("update-category")]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryCommand category)
        {
            var response = await Mediator.Send(category);
            return FinalRespons(response);
        }

        [HttpDelete("delete-category")]
        public async Task<IActionResult> DeleteCategory([FromQuery] DeleteCategoryCommand Id)
        {
            var response = await Mediator.Send(Id);
            return FinalRespons(response);
        }
    }
}
