using E_COMMERCE_APP.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Core.Features.Accounts.Commands.CommandModels
{
    public class DeleteUserCommand : IRequest<Response<string>>
    {
        public string UserId { get; set; }
        public string Password { get; set; }
    }
}
