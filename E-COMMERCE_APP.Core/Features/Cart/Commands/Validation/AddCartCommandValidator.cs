using E_COMMERCE_APP.Core.Features.Cart.Commands.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Core.Features.Cart.Commands.Validation
{
    public class AddCartCommandValidator : AbstractValidator<AddCartCommand>
    {

        public AddCartCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotNull().WithMessage("User ID is required.")
                .NotEmpty().WithMessage("User ID must not be empty.");

            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("Product ID must be greater than zero.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than or equal to 1.");
        }


    }
}
