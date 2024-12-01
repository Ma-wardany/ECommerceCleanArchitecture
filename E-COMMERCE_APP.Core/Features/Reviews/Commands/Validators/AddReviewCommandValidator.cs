using E_COMMERCE_APP.Core.Features.Reviews.Commands.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Core.Features.Reviews.Commands.Validators
{
    public class AddReviewCommandValidator : AbstractValidator<AddReviewCommand>
    {
        public AddReviewCommandValidator()
        {
            RuleFor(r => r.Comment)
                .MaximumLength(1000).WithMessage("limited number of characters!");

            RuleFor(r => r.Rate)
                .InclusiveBetween(1, 10).WithMessage("you can rate between 1 and 10")
                .NotNull().WithMessage("rate is required!");

            RuleFor(r => r.ProductId)
                .NotNull().WithMessage("product id is required")
                .GreaterThan(0).WithMessage("invalid product id (non-zero id)");

            RuleFor(r => r.UserId)
                .NotEmpty().WithMessage("user id must not be empty")
                .NotNull().WithMessage("user id is required");
        }
    }
}
