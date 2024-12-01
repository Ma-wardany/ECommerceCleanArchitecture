using E_COMMERCE_APP.Core.Features.Authentication.Commands.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Core.Features.Authentication.Commands.validations
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("email must not be empty!")
                .NotNull().WithMessage("email must not be null!")
                .MaximumLength(50);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("password must not be empty!")
                .MaximumLength(50);
        }
    }
}
