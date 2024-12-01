using AutoMapper;
using E_COMMERCE_APP.Core.Bases;
using E_COMMERCE_APP.Core.Features.Accounts.Commands.CommandModels;
using E_COMMERCE_APP.Core.Features.Accounts.Commands.Results;
using E_COMMERCE_APP.Data.Entities.Identity;
using E_COMMERCE_APP.Services.Abstracts;
using MediatR;


namespace E_COMMERCE_APP.Core.Features.Accounts.Commands.Handlers
{
    public class AccountsHandler : ResponseHandler, 
                                   IRequestHandler<RegisterCommand, Response<RegisterationResultModel>>,
                                   IRequestHandler<ChangeUserPasswordCommand, Response<string>>,
                                   IRequestHandler<DeleteUserCommand, Response<string>>
    {
        private readonly IMapper mapper;
        private readonly IApplicationUserSrvices applicationUserSrvices;

        public AccountsHandler(IMapper mapper, IApplicationUserSrvices applicationUserSrvices)
        {
            this.mapper = mapper;
            this.applicationUserSrvices = applicationUserSrvices;
        }

        public async Task<Response<RegisterationResultModel>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var applicationUser = mapper.Map<ApplicationUser>(request);

            var result = await applicationUserSrvices.Register(applicationUser, request.Password);

            var user = result.Item1;
            var message = result.Item2;

            if(message != null)
            {
                return message switch
                {
                    "UserExist" => BadRequest<RegisterationResultModel>("user is already exist!"),
                    "RoleError" => BadRequest<RegisterationResultModel>("can not assign role!"),
                    _ => BadRequest<RegisterationResultModel>(message),
                };
            }

            var registerResult = mapper.Map<RegisterationResultModel>(user);

            return Success(registerResult);
        }

        public async Task<Response<string>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await applicationUserSrvices.ChangePassword(request.UserId, request.OldPassword, request.NewPassword);

            return result switch
            {
                "NotFound"      => BadRequest<string>("user not found!"),
                "WrongPassword" => BadRequest<string>("old password is wrong!"),
                "Success"       => Success<string>(null, message: "Password has been changed successfully!"),
                _ =>               BadRequest<string>(result)
            };
        }

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var result = await applicationUserSrvices.Delete(request.UserId, request.Password);

            return result switch
            {
                "NotFound"      => BadRequest<string>("user not found!"),
                "WrongPassword" => BadRequest<string>("old password is wrong!"),
                "Success"       => Success<string>(null, message: "account has been deleted successfully!"),
                _               => BadRequest<string>(result)
            };
        }
    }
}
