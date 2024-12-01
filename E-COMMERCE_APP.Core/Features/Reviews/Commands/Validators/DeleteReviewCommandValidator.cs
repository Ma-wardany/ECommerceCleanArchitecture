using E_COMMERCE_APP.Core.Features.Reviews.Commands.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Core.Features.Reviews.Commands.Validators
{
    public class DeleteReviewCommandValidator : AbstractValidator<DeleteReviewCommand>
    {
        public DeleteReviewCommandValidator()
        {
            RuleFor(r => r.ReviewId)
                .NotNull().WithMessage("review id is required")
                .GreaterThan(0).WithMessage("invalid product id (non-zero id)");

            RuleFor(r => r.UserId)
                .NotEmpty().WithMessage("user id must not be empty")
                .NotNull().WithMessage("user id is required");
        }
    }
}
