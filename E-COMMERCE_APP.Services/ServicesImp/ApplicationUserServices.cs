using E_COMMERCE_APP.Data.Entities.Identity;
using E_COMMERCE_APP.Infrastructure.Context;
using E_COMMERCE_APP.Services.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace E_COMMERCE_APP.Services.ServicesImp
{
    public class ApplicationUserServices : IApplicationUserSrvices
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmailServices emailServices;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IUrlHelper urlHelper;
        private readonly AppDbContext context;

        public ApplicationUserServices(
            UserManager<ApplicationUser> userManager, IEmailServices emailServices,
            IHttpContextAccessor httpContextAccessor, IUrlHelper urlHelper, AppDbContext context)
        {
            this.userManager = userManager;
            this.emailServices = emailServices;
            this.httpContextAccessor = httpContextAccessor;
            this.urlHelper = urlHelper;
            this.context = context;
        }

        public async Task<(ApplicationUser?, string?)> Register(ApplicationUser applicationUser, string password)
        {
            var existingUser = await userManager.FindByEmailAsync(applicationUser.Email!);
            if (existingUser != null) return (null, "UserExist");


            using var Trans = await context.Database.BeginTransactionAsync();
            try
            {
                // Create the new user (applicationUser) here, not existingUser!
                var result = await userManager.CreateAsync(applicationUser, password);
                if (!result.Succeeded)
                {
                    await Trans.RollbackAsync();
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description).ToList());
                    Console.WriteLine($"Errors : {errors}");
                    return (null, errors);
                }

                // add role to this user
                var roleResult = await userManager.AddToRoleAsync(applicationUser, "User");
                if (!roleResult.Succeeded)
                {
                    await Trans.RollbackAsync();
                    return (null, "RoleError");
                }

                // Generate the confirmation code for the newly created user (applicationUser)
                var code = await userManager.GenerateEmailConfirmationTokenAsync(applicationUser);
                var request = httpContextAccessor.HttpContext!.Request;
                var returnUrl = request.Scheme + "://" + request.Host + urlHelper.Action("ConfirmEmail", "Account", new { userId = applicationUser.Id, code = code });
                var message = $"To confirm your email, click this link: <a href='{returnUrl}'>Confirm Email</a>";
                await emailServices.SendEmailAsync(applicationUser.Email!, message, "Confirm Email");

                await Trans.CommitAsync();

                Console.WriteLine(message);
                Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++");
                return (applicationUser, null);
            }
            catch (Exception ex)
            {
                await Trans.RollbackAsync();
                return (null, $"Errors : {ex.Message}");
            }
        }


    public async Task<string> ChangePassword(string UserId, string OldPassword, string NewPassword)
        {
            var user = await userManager.FindByIdAsync(UserId);
            if (user == null)
                return "NotFound";

            try
            {
                var result = await userManager.ChangePasswordAsync(user, OldPassword, NewPassword);
                if (!result.Succeeded)
                    return "WrongPassword";

                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> Delete(string UserId, string Password)
        {
            var user = await userManager.FindByIdAsync(UserId);
            if (user == null)
                return "NotFound";

            try
            {
                var IsPasswordCorrect = await userManager.CheckPasswordAsync(user, Password);
                if (!IsPasswordCorrect)
                    return "WrongPassword";

                var result = await userManager.DeleteAsync(user);
                if (!result.Succeeded)
                    return result.Errors.Select(e => e.Description).ToString()!;

                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
