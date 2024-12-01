using E_COMMERCE_APP.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Core.Features.Authorization.Commands.Models
{
    public class RemoveRoleCommand : IRequest<Response<string>>
    {
        [Required(ErrorMessage = "role id is required")]
        public string RoleId { get; set; }
    }
}
