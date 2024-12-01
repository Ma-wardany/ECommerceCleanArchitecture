



using E_COMMERCE_APP.Core.Bases;
using E_COMMERCE_APP.Data.Results;
using MediatR;

namespace E_COMMERCE_APP.Core.Features.Authentication.Commands.Models
{
    public class RefreshTokenCommand : IRequest<Response<JWTAuthResult>>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
