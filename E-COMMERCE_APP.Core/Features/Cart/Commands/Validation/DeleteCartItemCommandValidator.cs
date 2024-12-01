using E_COMMERCE_APP.Core.Features.Cart.Commands.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Core.Features.Cart.Commands.Validation
{
    public class DeleteCartItemCommandValidator : AbstractValidator<DeleteCartItemCommand>
    {
        public DeleteCartItemCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotNull().WithMessage("user id is required!")
                .NotEmpty().WithMessage("user id must not be empty")
                .Must(x => x.GetType() == typeof(string))
                .WithMessage("expected string value");

            RuleFor(x => x.ProductId)
                .GreaterThan(0)
                .WithMessage("invalid product id")
                .Must(x => x.GetType() == typeof(int))
                .WithMessage("expected int value");
        }
    }
}
