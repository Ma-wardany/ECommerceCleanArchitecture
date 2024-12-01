using E_COMMERCE_APP.Core.Features.Authentication.Commands.Models;
using FluentValidation;




namespace E_COMMERCE_APP.Core.Features.Authentication.Commands.validations
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("email must not be empty")
                .NotNull().WithMessage("email is required");

            RuleFor(x => x.VerificationToken)
                .NotEmpty().WithMessage("Verification Token must not be empty")
                .NotNull().WithMessage("Verification Token is required");
            
            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New Password must not be empty")
                .NotNull().WithMessage("New Password is required");
        }
    }
}
