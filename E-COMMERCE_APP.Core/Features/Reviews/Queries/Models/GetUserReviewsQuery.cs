using E_COMMERCE_APP.Core.Bases;
using E_COMMERCE_APP.Core.Features.Reviews.Results;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;



namespace E_COMMERCE_APP.Core.Features.Reviews.Queries.Models
{
    public class GetUserReviewsQuery : IRequest<Response<List<ReviewResult>>>
    {
        public string? UserId { get; set; }
    }
}
