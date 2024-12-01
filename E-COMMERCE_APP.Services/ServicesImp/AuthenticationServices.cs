using E_COMMERCE_APP.Data.Entities.Identity;
using E_COMMERCE_APP.Data.Helpers;
using E_COMMERCE_APP.Data.Results;
using E_COMMERCE_APP.Infrastructure.Repositories.Abstracts;
using E_COMMERCE_APP.Services.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using OneOf;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;



namespace E_COMMERCE_APP.Services.ServicesImp
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly JwtSettings jwtSettings;
        private readonly IRefreshTokenRepository refreshTokenRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IUrlHelper urlHelper;
        private readonly IEmailServices emailServices;
        private readonly IDistributedCache cache;

        public AuthenticationServices(UserManager<ApplicationUser> userManager,
                                      SignInManager<ApplicationUser> signInManager,
                                      JwtSettings jwtSettings,
                                      IRefreshTokenRepository refreshTokenRepository,
                                      IHttpContextAccessor httpContextAccessor,
                                      IUrlHelper urlHelper,
                                      IEmailServices emailServices,
                                      IDistributedCache cache)
        {
            this.userManager            = userManager;
            this.signInManager          = signInManager;
            this.jwtSettings            = jwtSettings;
            this.refreshTokenRepository = refreshTokenRepository;
            this.httpContextAccessor    = httpContextAccessor;
            this.urlHelper              = urlHelper;
            this.emailServices          = emailServices;
            this.cache                  = cache;
        }



        public async Task<OneOf<string, JWTAuthResult>> LoginAsync(ApplicationUser user, string password)
        {
            var existUser = await userManager.FindByEmailAsync(user.Email!);

            if (existUser == null) return "NotFound";

            var result = await signInManager.CheckPasswordSignInAsync(existUser, password, false);
            if(!result.Succeeded)
                return "Failed";

            var AccessToken  = await GetJwtToken(existUser);
            var RefreshToken = GetRefreshToken(existUser.UserName!);

            var refreshToken = new UserRefreshToken
            {
                UserId       = existUser.Id,
                AccessToken  = AccessToken,
                RefreshToken = RefreshToken.TokenString,
                ExpiresOn    = RefreshToken.ExpireAt,
                IsRevoked    = false,
            };

            await refreshTokenRepository.AddAsync(refreshToken);

            return new JWTAuthResult
            {
                UserId       = existUser.Id,
                Name         = existUser.FirstName + " " + existUser.LastName,
                Email        = existUser.Email,
                IsConfirmed  = existUser.EmailConfirmed,
                UserName     = existUser.UserName,
                AccessToken  = AccessToken,
                RefreshToken = RefreshToken,
            };
            
        }

        public async Task<string> GetJwtToken(ApplicationUser user)
        {
            var roles = await userManager.GetRolesAsync(user);  // Get roles from the user
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));  // Add each role as a claim
            }

            var userClaims = await userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            var token = new JwtSecurityToken(
                issuer             : jwtSettings.Issuer,
                audience           : jwtSettings.Audience,
                claims             : claims,
                expires            : DateTimeOffset.UtcNow.AddHours(jwtSettings.TokenExpireDate).UtcDateTime,
                signingCredentials : new SigningCredentials(new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                    SecurityAlgorithms.HmacSha256)
            );

            var finalToken = new JwtSecurityTokenHandler().WriteToken(token);

            return finalToken;
        }


        private RefreshTokenModel GetRefreshToken(string username)
        {
            return new RefreshTokenModel
            {
                UserName    = username,
                ExpireAt    = DateTime.UtcNow.AddDays(jwtSettings.RefreshTokenExpireDate),
                TokenString = GenerateRefreshToken()
            };
        }

        private string GenerateRefreshToken()
        {
            var refreshToken = new byte[32];

            var generator    = RandomNumberGenerator.Create();
            generator.GetBytes(refreshToken);

            return Convert.ToBase64String(refreshToken);
        }

        public async Task<OneOf<string, JWTAuthResult>> RefreshTokenAsync(string accessToken, string refreshToken)
        {
            try
            {
                // Extract claims from access token
                var claims = ReadAccessToken(accessToken);
                if (claims == null) return "UnAuthorized";

                // Find the user based on token claims
                var user = await userManager.FindByNameAsync(claims.Identity!.Name!);
                if (user == null) return "UnAuthorized";

                // Retrieve the single active, non-revoked refresh token
                var storedToken = await refreshTokenRepository.GetTableNoTracking()
                    .FirstOrDefaultAsync(r => r.UserId == user.Id && !r.IsRevoked);

                // Validate the existing refresh token
                if (storedToken == null || storedToken.RefreshToken != refreshToken || storedToken.ExpiresOn <= DateTime.UtcNow)
                {
                    if (storedToken.ExpiresOn <= DateTime.UtcNow)
                    {
                        // Revoke the token if it exists but is expired or invalid
                        storedToken.IsRevoked = true;
                        await refreshTokenRepository.UpdateAsync(storedToken);
                    }
                    return "InvalidRefreshToken";
                }



                // Generate new tokens
                var newAccessToken  = await GetJwtToken(user);

                bool refresh = ((storedToken.ExpiresOn - DateTime.UtcNow).TotalDays) <= 1;

                // Update the token record with the new tokens
                storedToken.AccessToken  = newAccessToken;
                if(refresh)
                {
                    storedToken.RefreshToken = GetRefreshToken(user.UserName).TokenString;
                }
                storedToken.ExpiresOn    = DateTime.UtcNow.AddDays(jwtSettings.RefreshTokenExpireDate);
                storedToken.IsRevoked    = false;

                await refreshTokenRepository.UpdateAsync(storedToken);

                // Return the updated JWTAuthResult
                return new JWTAuthResult
                {
                    UserId       = user.Id,
                    Name         = user.FirstName + " " + user.LastName,
                    UserName     = user.UserName,
                    Email        = user.Email,
                    IsConfirmed  = user.EmailConfirmed,
                    AccessToken  = newAccessToken,
                    RefreshToken = new RefreshTokenModel
                    {
                        UserName    = user.UserName,
                        TokenString = storedToken.RefreshToken,
                        ExpireAt    = storedToken.ExpiresOn
                    }
                };
            }
            catch (Exception ex)
            {
                // Log the exception here
                return "An error occurred while refreshing the token";
            }
        }

        private ClaimsPrincipal ReadAccessToken(string accessToken)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer           = true,
                ValidIssuer              = jwtSettings.Issuer,
                ValidateAudience         = true,
                ValidAudience            = jwtSettings.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey         = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                ValidateLifetime         = false,
            };


            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var claims   = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out SecurityToken securityToken);

                var jwtToken = securityToken as JwtSecurityToken;

                if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))
                    throw new SecurityTokenException("Invalid token");

                return claims;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errors : {ex.Message} -------------------");
                throw new UnauthorizedAccessException("Invalid access token", ex);
            }

        }


        public async Task<string> ConfirmEmail(string? userId, string? code)
        {
            if (userId == null || code == null)
                return "Error";

            var user = await userManager.FindByIdAsync(userId);

            var confirm = await userManager.ConfirmEmailAsync(user, code);

            if (!confirm.Succeeded)
                return string.Join("; ", confirm.Errors.Select(e => e.Description));

            return "Success";
        }

        public async Task<string> ResendConfirmEmail(string email)
        {
            if (email == null)
                return "NotFound";

            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
                return "NotFound";

            if(!user.EmailConfirmed)
            {
                var code      = await userManager.GenerateEmailConfirmationTokenAsync(user);
                var request   = httpContextAccessor.HttpContext.Request;
                var returnUrl = request.Scheme + "://" + request.Host + urlHelper.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code });
                var message   = $"To confirm your email, click this link: <a href='{returnUrl}'>Confirm Email</a>";

                await emailServices.SendEmailAsync(user.Email, message, null);
                return "Success";
            }

            return "Confirmed";
        }

        public async Task<string> RequestOTPOfResetPassword(string email)
        {
            if (email == null) return "NotFound";

            var user = await userManager.FindByEmailAsync(email);
            if (user == null) return "NotFound";

            var otp = new Random().Next(100000, 999999).ToString();

            cache.SetString($"{user.Email}_OTP", otp, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            });

            string message = $"Your OTP Code : {otp}";
            await emailServices.SendEmailAsync(user.Email, message, null);

            return "Success";
        }

        public async Task<string> VerifyOTPOfResetPassword(string email, string otp)
        {
            if (email == null || otp == null) return "EmptyError";

            var cachedOTP = await cache.GetStringAsync($"{email}_OTP");
            Console.WriteLine($"otp : {cachedOTP}");

            if (cachedOTP == null || cachedOTP != otp) return "Invalid";

            await cache.RemoveAsync($"{email}_OTP");

            var verificationToken = Guid.NewGuid().ToString();

            cache.SetString($"{email}_verificationToken", verificationToken, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            });

            return verificationToken;
        }

        public async Task<string> ResetPassword(string email, string verificationToken, string newPassword)
        {
            if (email == null || verificationToken == null || newPassword == null)
                return "EmptyError";

            var user = await userManager.FindByEmailAsync(email);
            if (user == null) return "NotFound";

            var cachedVerificationToken = cache.GetString($"{email}_verificationToken");
            await cache.RemoveAsync($"{email}_verificationToken");

            if (cachedVerificationToken == null || cachedVerificationToken != verificationToken)
                return "Invalid";

            var restToken = await userManager.GeneratePasswordResetTokenAsync(user);
            var result = await userManager.ResetPasswordAsync(user, restToken, newPassword);

            if (!result.Succeeded) return "Failed";

            return "Success";
        }

        public async Task<string> SignOut(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                return "Unauthorized";

            try
            {
                var refreshTokens = await refreshTokenRepository.GetTableAsTracking()
                                                                .Where(r => r.UserId == userId)
                                                                .ToListAsync();

                if (refreshTokens.Any())
                {
                    await refreshTokenRepository.DeleteRangeAsync(refreshTokens);
                }

                await signInManager.SignOutAsync();

                return "Out";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

    }
}
