using E_COMMERCE_APP.API.Bases;
using E_COMMERCE_APP.Core.Features.Authorization.Commands.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_COMMERCE_APP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : AppController
    {
        [HttpPost("add-role")]
        public async Task<IActionResult> AddRole(AddRoleCommand command)
        {
            var response = await Mediator.Send(command);
            return FinalRespons(response);
        }

        [HttpDelete("remove-role")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveRole([FromQuery]RemoveRoleCommand command)
        {
            var response = await Mediator.Send(command);
            return FinalRespons(response);
        }

        [HttpPut("update-role")]
        public async Task<IActionResult> UpdateRole(UpdateRoleCommand command)
        {
            var response = await Mediator.Send(command);
            return FinalRespons(response);
        }
    }
}
