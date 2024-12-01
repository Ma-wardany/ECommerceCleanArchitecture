using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Core.Features.Accounts.Commands.Results
{
    public class RegisterationResultModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email {  get; set; }
        public bool IsConfirmed { get; set; }
    }
}
