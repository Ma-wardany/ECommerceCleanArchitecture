using E_COMMERCE_APP.Core.Bases;
using E_COMMERCE_APP.Core.Features.Reviews.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Core.Features.Reviews.Commands.Models
{
    public class UpdateReviewCommad : IRequest<Response<ReviewResult>>
    {
        public string UserId { get; set; }
        public int ReviewId { get; set; }
        public string Comment { get; set; }
        public int Rate { get; set; }
        public DateTime dateTime { get; set; } = DateTime.Now;
    }
}
