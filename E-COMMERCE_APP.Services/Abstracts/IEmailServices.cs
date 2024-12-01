using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Services.Abstracts
{
    public interface IEmailServices
    {
        public Task<string> SendEmailAsync(string email, string message, string? reason);
    }
}
