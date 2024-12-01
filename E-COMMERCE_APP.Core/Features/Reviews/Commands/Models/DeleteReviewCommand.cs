using E_COMMERCE_APP.Core.Bases;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace E_COMMERCE_APP.Core.Features.Reviews.Commands.Models
{
    public class DeleteReviewCommand : IRequest<Response<string>>
    {
        public string UserId { get; set; }
        public int ReviewId { get; set; }
    }
}
