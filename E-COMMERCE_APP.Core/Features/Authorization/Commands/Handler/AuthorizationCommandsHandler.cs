using E_COMMERCE_APP.Core.Bases;
using E_COMMERCE_APP.Core.Features.Authorization.Commands.Models;
using E_COMMERCE_APP.Data.Entities.Identity;
using E_COMMERCE_APP.Services.Abstracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Core.Features.Authorization.Commands.Handler
{
    public class AuthorizationCommandsHandler : ResponseHandler,
                   IRequestHandler<AddRoleCommand, Response<Role>>,
                   IRequestHandler<RemoveRoleCommand, Response<string>>,
                   IRequestHandler<UpdateRoleCommand, Response<Role>>
    {
        private readonly IAuthorizationServices authorizationServices;

        public AuthorizationCommandsHandler(IAuthorizationServices authorizationServices)
        {
            this.authorizationServices = authorizationServices;
        }

        public async Task<Response<Role>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await authorizationServices.AddRoleAsync(request.RoleName);

            var role = result.Item1;
            var message = result.Item2;

            if (message != null)
            {
                return message switch
                {
                    "Exist" => BadRequest<Role>("role is already exist!"),
                    "Failed" or _ => BadRequest<Role>("something went wrong")
                };
            }

            return Success<Role>(role);
        }

        public async Task<Response<string>> Handle(RemoveRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await authorizationServices.RemoveRoleAsync(request.RoleId);

            return result switch
            {
                "NotFound" => BadRequest<string>("role not found"),
                "RoleUsed" => BadRequest<string>("you can not remove role, it is used!"),
                "Success" => Success<string>(entity: null, message: "role has been removed successfully!"),
                "Failed" => BadRequest<string>("something went wrong!")
            };
        }

        async Task<Response<Role>> IRequestHandler<UpdateRoleCommand, Response<Role>>.Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await authorizationServices.UpdateRoleAsync(request.RoleId, request.RoleName);

            return result switch
            {
                "NotFound" => BadRequest<Role>("role not found!"),
                "Success" => Success<Role>(new Role { Id = request.RoleId, Name = request.RoleName }),
                "Failed" or _=> BadRequest<Role>("something went wrong!")
            };
        }
    }
}
