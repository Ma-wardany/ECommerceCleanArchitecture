using E_COMMERCE_APP.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Core.Features.Authentication.Commands.Models
{
    public class VerifyOTPOfResetPasswordCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }
        public string OTP { get; set; }
    }
}
