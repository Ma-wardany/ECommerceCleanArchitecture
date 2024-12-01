using E_COMMERCE_APP.Core.Bases;
using MediatR;




namespace E_COMMERCE_APP.Core.Features.Cart.Commands.Models
{
    public class AddCartCommand : IRequest<Response<string>>
    {
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public int Quantity { get; set; }
    }
}
