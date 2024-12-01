using E_COMMERCE_APP.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Services.Abstracts
{
    public interface IApplicationUserSrvices
    {
        public Task<(ApplicationUser?, string?)> Register(ApplicationUser user, string password);
        public Task<string> Delete(string UserId, string Password);
        public Task<string> ChangePassword(string UserId, string OldPassword, string NewPassword);
    }
}
