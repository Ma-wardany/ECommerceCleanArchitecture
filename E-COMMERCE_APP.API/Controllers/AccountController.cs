using E_COMMERCE_APP.API.Bases;
using E_COMMERCE_APP.API.DTOs.Account;
using E_COMMERCE_APP.Core.Features.Accounts.Commands.CommandModels;
using E_COMMERCE_APP.Core.Features.Authentication.Commands.Models;
using E_COMMERCE_APP.Core.Features.Cart.Commands.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_COMMERCE_APP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : AppController
    {

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(RegisterCommand user)
        {
            var response = await Mediator.Send(user);

            return FinalRespons(response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginCommand user)
        {
            var response = await Mediator.Send(user);

            return FinalRespons(response);
        }


        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshTokenAsync(RefreshTokenCommand tokens)
        {
            var response = await Mediator.Send(tokens);

            return FinalRespons(response);
        }


        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailCommand model)
        {
            var response = await Mediator.Send(model);
            return FinalRespons(response);
        }


        [HttpPost("resend-confirm-email")]
        public async Task<IActionResult> ResendConfirmEmail(ResendConfirmEmailCommand model)
        {
            var response = await Mediator.Send(model);
            return FinalRespons(response);
        }

        [HttpPost("request-resetPassword-OTP")]
        public async Task<IActionResult> RequestResetPasswordOTP(RequestOTPOfResetPassowrdCommand model)
        {
            var response = await Mediator.Send(model);
            return FinalRespons(response);
        }

        [HttpPost("verify-resetPassword-OTP")]
        public async Task<IActionResult> VerfyResetPasswordOTP(VerifyOTPOfResetPasswordCommand model)
        {
            var response = await Mediator.Send(model);
            return FinalRespons(response);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordCommand model)
        {
            var response = await Mediator.Send(model);
            return FinalRespons(response);
        }

        [HttpPut("change-password")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> ChangePassword([FromBody]ChangePasswordDto Dto)
        {
            var UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            var command = new ChangeUserPasswordCommand
            {
                UserId = UserId != null ? UserId.Value : "",
                OldPassword = Dto.OldPassword,
                NewPassword = Dto.NewPassword,
            };

            var response = await Mediator.Send(command);
            return FinalRespons(response);
        }

        [HttpDelete("delete-account")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteAccount([FromQuery]string Password)
        {
            var UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            var command = new DeleteUserCommand
            {
                UserId = UserId != null ? UserId.Value : "",
                Password = Password
            };

            var response = await Mediator.Send(command);
            return FinalRespons(response);
        }

        [HttpPost("SignOut")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> SigningOut()
        {
            var UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            var command = new SignOutCommand
            {
                UserId = UserId != null ? UserId.Value : "",
            };
            Console.WriteLine(UserId.Value+"----------------------");

            var response = await Mediator.Send(command);

            return FinalRespons(response);
        }
    }
}
