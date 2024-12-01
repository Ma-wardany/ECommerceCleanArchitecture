using E_COMMERCE_APP.Core.Bases;
using E_COMMERCE_APP.Core.Features.Accounts.Commands.Results;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace E_COMMERCE_APP.Core.Features.Accounts.Commands.CommandModels
{
    public class RegisterCommand : IRequest<Response<RegisterationResultModel>> // Ensure this is correct
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public string Gender { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; } // Ensure this property name matches your usage
        public string? Country { get; set; }
    }
}
