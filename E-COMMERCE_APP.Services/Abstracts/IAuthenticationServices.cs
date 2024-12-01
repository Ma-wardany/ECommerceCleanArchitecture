using E_COMMERCE_APP.Data.Entities.Identity;
using E_COMMERCE_APP.Data.Results;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Services.Abstracts
{
    public interface IAuthenticationServices
    {
        public Task<OneOf<string, JWTAuthResult>> LoginAsync(ApplicationUser user, string password);
        public Task<string>                       GetJwtToken(ApplicationUser user);
        public Task<OneOf<string, JWTAuthResult>> RefreshTokenAsync(string AccessToken, string RefreshToken);
        public Task<string>                       ConfirmEmail(string? userId, string? code);
        public Task<string>                       ResendConfirmEmail(string email);
        public Task<string>                       RequestOTPOfResetPassword(string email);
        public Task<string>                       VerifyOTPOfResetPassword(string email, string otp);
        public Task<string>                       ResetPassword(string email, string verificationToken, string newPassword);
        public Task<string>                       SignOut(string UserId);
    }
}
