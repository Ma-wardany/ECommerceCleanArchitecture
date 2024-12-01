using E_COMMERCE_APP.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Services.Abstracts
{
    public interface IAuthorizationServices
    {
        public Task<(Role, string)> AddRoleAsync(string RoleName);
        public Task<string>         UpdateRoleAsync(string RoleId, string RoleName);
        public Task<string>         RemoveRoleAsync(string RoleId);
        public Task<bool>           IsRoleExist(string RoleId);
        public Task<List<Role>>     GetRolesAsync();
        public Task<Role>           GetRoleByIdAsync(string RoleId);
    }
}
