using E_COMMERCE_APP.Core.Features.Accounts.Commands.CommandModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Core.Features.Accounts.Commands.Validations
{
    public class RegisterCommandValidators : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidators()
        {
            RuleFor(rc => rc.FirstName)
                .NotEmpty().WithMessage("first name must not be empty!")
                .MaximumLength(50).MinimumLength(3)
                .NotNull();

            RuleFor(rc => rc.LastName)
                .NotEmpty().WithMessage("last name must not be empty!")
                .MaximumLength(50).MinimumLength(3)
                .NotNull();

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("user name must not be empty!")
                .NotNull().WithMessage("must not null")
                .MaximumLength(100).WithMessage("long user name");

            RuleFor(x => x.Email)
                 .NotEmpty().WithMessage("user name must not be empty!")
                 .NotNull().WithMessage("must not null");

            RuleFor(x => x.Password)
                 .NotEmpty().WithMessage("password must not be empty!")
                 .NotNull().WithMessage("must not null");

            RuleFor(x => x.ConfirmPassword)
                 .Equal(x => x.Password).WithMessage("passowrd not match");
        }
    }
}
