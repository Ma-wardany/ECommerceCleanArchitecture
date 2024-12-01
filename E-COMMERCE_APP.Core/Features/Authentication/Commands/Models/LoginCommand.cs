using E_COMMERCE_APP.Core.Bases;
using E_COMMERCE_APP.Data.Results;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace E_COMMERCE_APP.Core.Features.Authentication.Commands.Models
{
    public class LoginCommand : IRequest<Response<JWTAuthResult>>
    {
       // [Required]
        //[DataType(DataType.EmailAddress)]
        public string Email { get; set; }

       // [Required]
       // [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
