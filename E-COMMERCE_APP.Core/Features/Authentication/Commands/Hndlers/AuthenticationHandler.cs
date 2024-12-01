using AutoMapper;
using E_COMMERCE_APP.Core.Bases;
using E_COMMERCE_APP.Core.Features.Authentication.Commands.Models;
using E_COMMERCE_APP.Data.Entities.Identity;
using E_COMMERCE_APP.Data.Results;
using E_COMMERCE_APP.Services.Abstracts;
using MediatR;


namespace E_COMMERCE_APP.Core.Features.Authentication.Commands.Hndlers
{
    public class AuthenticationHandler : ResponseHandler,
                                         IRequestHandler<LoginCommand, Response<JWTAuthResult>>,
                                         IRequestHandler<RefreshTokenCommand, Response<JWTAuthResult>>,
                                         IRequestHandler<ConfirmEmailCommand, Response<string>>,
                                         IRequestHandler<ResendConfirmEmailCommand, Response<string>>,
                                         IRequestHandler<RequestOTPOfResetPassowrdCommand, Response<string>>,
                                         IRequestHandler<VerifyOTPOfResetPasswordCommand, Response<string>>,
                                         IRequestHandler<ResetPasswordCommand, Response<string>>,
                                         IRequestHandler<SignOutCommand, Response<string>>
    {
        private readonly IMapper mapper;
        private readonly IAuthenticationServices authenticationServices;

        public AuthenticationHandler(IMapper mapper, IAuthenticationServices authenticationServices)
        {
            this.mapper = mapper;
            this.authenticationServices = authenticationServices;
        }


        public async Task<Response<JWTAuthResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = mapper.Map<ApplicationUser>(request);

            var result = await authenticationServices.LoginAsync(user, request.Password);

            return result.Match(
                loginResult => loginResult switch
                {
                    "NotFound" => NotFound<JWTAuthResult>($"{user.Email} not registered"),
                    "Failed"   => BadRequest<JWTAuthResult>("Email or password went wrong!"),
                    _          => BadRequest<JWTAuthResult>("An unexpected error occurred.") 
                },

                jwtResult => Success<JWTAuthResult>(jwtResult, "Signing in successfully")
            );
        }

        public async Task<Response<JWTAuthResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var result = await authenticationServices.RefreshTokenAsync(request.AccessToken, request.RefreshToken);

            return result.Match(
                failed => failed switch
                {
                    "UnAuthorized"         => UnAuthorized<JWTAuthResult>(),
                    "InvalidRefreshToken"  => UnAuthorized<JWTAuthResult>("Invalid Refresh Token"),
                    "RefreshTokenNotFound" => UnAuthorized<JWTAuthResult>("Refresh Token NotFound"),
                    _ => UnAuthorized<JWTAuthResult>("An unexpected error occurred") // Handle unexpected errors
                },
                newToken => Success<JWTAuthResult>(newToken)
            );
        }

        public async Task<Response<string>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            string result = await authenticationServices.ConfirmEmail(request.UserId, request.Code);

            switch(result)
            {
                case "Success":
                    return Success<string>("email has been confirmed successfully");

                case "Error":
                    return BadRequest<string>("something went wrong!");

                default:
                    return BadRequest<string>(result);
            }
        }

        public async Task<Response<string>> Handle(ResendConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            string result = await authenticationServices.ResendConfirmEmail(request.Email);

            switch(result)
            {
                case "NotFound":
                    return NotFound<string>("there is no user has this email!");

                case "Success":
                    return Success("email has been sent successfully!");

                case "Confirmed":
                    return BadRequest<string>("email is already confirmed!");

                default:
                    return BadRequest<string>(result);

            }
        }

        public async Task<Response<string>> Handle(RequestOTPOfResetPassowrdCommand request, CancellationToken cancellationToken)
        {
            var result = await authenticationServices.RequestOTPOfResetPassword(request.Email);

            return result switch
            {
                "NotFound" => NotFound<string>("no user has this email!"),
                "Success"  => Success<string>("OTP sent, check your inbox!"),
                _          => BadRequest<string>(result)
        };
        }

        public async Task<Response<string>> Handle(VerifyOTPOfResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await authenticationServices.VerifyOTPOfResetPassword(request.Email, request.OTP);

            return result switch
            {
                "EmptyError" => BadRequest<string>("non complete data"),
                "Invalid"    =>    BadRequest<string>("invalid or expired OTP"),
                _            =>            Success<string>(result),
                
            };
        }

        public async Task<Response<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await authenticationServices.ResetPassword(request.Email, request.VerificationToken, request.NewPassword);

            return result switch
            {
                "EmptyError" => BadRequest<string>("non complete data!"),
                "NotFound"   => BadRequest<string>("user not found!"),
                "Invalid"    => BadRequest<string>("Invaild verification token!"),
                "Failed"     => BadRequest<string>("someting went wrong!"),
                "Success"    => Success<string>("password reset successfully!")

            };
        }

        public async Task<Response<string>> Handle(SignOutCommand request, CancellationToken cancellationToken)
        {
            var result = await authenticationServices.SignOut(request.UserId);

            return result switch
            {
                "Unauthorized" => UnAuthorized<string>("unauthorized user"),
                "Out"          => Success<string>(null, message: "you has been signed out!"),
                _              => BadRequest<string>(result)
            };
        }
    }
}
