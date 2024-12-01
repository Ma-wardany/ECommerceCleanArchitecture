using E_COMMERCE_APP.API.Bases;
using E_COMMERCE_APP.Core.Features.Reviews.Commands.Models;
using E_COMMERCE_APP.Core.Features.Reviews.Queries.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_COMMERCE_APP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : AppController
    {
        [HttpPost("add-review")]
        [Authorize(Roles ="User")]
        public async Task<IActionResult> AddReview(AddReviewCommand command)
        {
            var UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            command.UserId = UserId != null ? UserId.Value : "";

            var response = await Mediator.Send(command);

            return FinalRespons(response);
        }

        [HttpPut("update-review")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateReview(UpdateReviewCommad command)
        {
            var UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            command.UserId = UserId.Value;

            var response = await Mediator.Send(command);

            return FinalRespons(response);
        }

        [HttpDelete("delete-review")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteReview([FromQuery]int ReviewId)
        {
            var UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var command = new DeleteReviewCommand
            {
                UserId   = UserId != null ? UserId.Value : "",
                ReviewId = ReviewId,
            };

            var response = await Mediator.Send(command);

            return FinalRespons(response);
        }


        [HttpGet("get-product-reviews")]
        public async Task<IActionResult> GetProductReviews([FromQuery]GetProductReviewsQuery query)
        {
            var response = await Mediator.Send(query);

            return FinalRespons(response);
        }

        [HttpGet("get-user-reviews")]
        [Authorize(Roles ="User")]
        public async Task<IActionResult> GetUserReviews()
        {
            var UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            GetUserReviewsQuery query = new GetUserReviewsQuery
            {
                UserId = UserId != null ? UserId.Value : ""
            };

            var response = await Mediator.Send(query);

            return FinalRespons(response);
        }
    }
}
