using E_COMMERCE_APP.Core.Features.Cart.Commands.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Core.Features.Cart.Commands.Validation
{
    public class UpdateCartItemQuantityCommandValidator : 
                                                  AbstractValidator<UpdateCartItemQuantityCommand>

    {
        public UpdateCartItemQuantityCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotNull()
                .WithMessage("user id is required!")
                .NotEmpty()
                .WithMessage("user id must not be empty")
                .Must(x => x.GetType() == typeof(string))
                .WithMessage("expected string value");

            RuleFor(x => x.ProductId)
                .GreaterThan(0)
                .WithMessage("invalid product id")
                .Must(x => x.GetType() == typeof(int))
                .WithMessage("expected int value");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("quantity must be more than or equal 1")
                .Must(x => x.GetType() == typeof(int))
                .WithMessage("Quantity must be of type integer.");
        }
    }
}
