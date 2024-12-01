using E_COMMERCE_APP.Core.Bases;
using MediatR;

namespace E_COMMERCE_APP.Core.Features.Authentication.Commands.Models
{
    public class SignOutCommand : IRequest<Response<string>>
    {
        public string UserId { get; set; }
    }
}
