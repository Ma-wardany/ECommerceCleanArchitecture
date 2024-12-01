using E_COMMERCE_APP.Core.Features.Authentication.Commands.Models;
using FluentValidation;



namespace E_COMMERCE_APP.Core.Features.Authentication.Commands.validations
{
    public class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommand>
    {
        public ConfirmEmailCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("user id must not empty")
                .NotNull().WithMessage("user id is required");

            RuleFor(x => x.Code)
                .NotNull().WithMessage("code is required")
                .NotEmpty().WithMessage("code must not empty");
        }
    }
}
