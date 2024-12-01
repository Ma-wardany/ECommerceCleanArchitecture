using E_COMMERCE_APP.Core.Bases;
using E_COMMERCE_APP.Data.Entities.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Core.Features.Authorization.Commands.Models
{
    public class AddRoleCommand : IRequest<Response<Role>>
    {
        [Required(ErrorMessage = "role name is required")]
        public string RoleName { get; set; }
    }
}
