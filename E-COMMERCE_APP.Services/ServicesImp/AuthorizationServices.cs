using E_COMMERCE_APP.Data.Entities.Identity;
using E_COMMERCE_APP.Services.Abstracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Services.ServicesImp
{
    public class AuthorizationServices : IAuthorizationServices
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<Role> roleManager;

        public AuthorizationServices(UserManager<ApplicationUser> userManager, RoleManager<Role> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<(Role?, string?)> AddRoleAsync(string RoleName)
        {
            var role = await roleManager.FindByNameAsync(RoleName);

            if (role != null) return (null, "Exist");

            var result = await roleManager.CreateAsync(new Role { Name = RoleName });
            var storedRole = await roleManager.FindByNameAsync(RoleName);

            if (result.Succeeded) return (storedRole, null);

            return (null, "Failed");
        }

        public async Task<Role?> GetRoleByIdAsync(string RoleId)
        {
            var role = await roleManager.FindByIdAsync(RoleId);
            
            return role;
        }

        public async Task<List<Role>> GetRolesAsync()
        {
            List<Role> roles = await roleManager.Roles.ToListAsync();

            return roles;
        }

        public async Task<bool> IsRoleExist(string RoleId)
        {
            return await roleManager.RoleExistsAsync(RoleId);
        }

        public async Task<string> RemoveRoleAsync(string RoleId)
        {
            var role = await roleManager.FindByIdAsync(RoleId);

            if (role == null) return "NotFound";

            // check for users already have this role
            var users = await userManager.GetUsersInRoleAsync(role.Name!);

            if (!(users.IsNullOrEmpty())) return "RoleUsed";

            var result = await roleManager.DeleteAsync(role);
            if (result.Succeeded) return "Success";

            return "Failed";
        }

        public async Task<string> UpdateRoleAsync(string RoleId, string RoleName)
        {
            var role = await roleManager.FindByIdAsync(RoleId);

            if (role == null) return "NotFound";

            role.Name = RoleName;
            role.NormalizedName = RoleName.ToUpper();

            var result = await roleManager.UpdateAsync(role);

            if (result.Succeeded) return "Success";

            return "Failed";
        }
    }
}
