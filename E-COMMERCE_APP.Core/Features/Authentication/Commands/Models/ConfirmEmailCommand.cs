﻿using E_COMMERCE_APP.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Core.Features.Authentication.Commands.Models
{
    public class ConfirmEmailCommand : IRequest<Response<string>>
    {
        public string UserId { get; set; }
        public string Code   { get; set; }
    }
}
