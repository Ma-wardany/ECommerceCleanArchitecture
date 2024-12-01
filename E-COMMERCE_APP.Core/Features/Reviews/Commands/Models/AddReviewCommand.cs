using E_COMMERCE_APP.Core.Bases;
using E_COMMERCE_APP.Core.Features.Reviews.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Core.Features.Reviews.Commands.Models
{
    public class AddReviewCommand : IRequest<Response<ReviewResult>>
    {
        public string Comment { get; set; }
        public int Rate { get; set; }
        public DateTime dateTime { get; set; } = DateTime.Now;
        public string UserId { get; set; }
        public int ProductId { get; set; }
    }
}
