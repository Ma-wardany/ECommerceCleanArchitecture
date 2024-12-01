using E_COMMERCE_APP.Core.Bases;
using E_COMMERCE_APP.Core.Features.Reviews.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace E_COMMERCE_APP.Core.Features.Reviews.Queries.Models
{
    public class GetProductReviewsQuery : IRequest<Response<List<ReviewResult>>>
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage ="invalid product id"),]
        public int ProductId { get; set; }
    }
}
