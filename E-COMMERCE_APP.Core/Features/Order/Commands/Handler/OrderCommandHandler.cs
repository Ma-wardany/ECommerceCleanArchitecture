using AutoMapper;
using E_COMMERCE_APP.Core.Bases;
using E_COMMERCE_APP.Core.Features.Order.Commands.Models;
using E_COMMERCE_APP.Core.Features.Order.Results;
using E_COMMERCE_APP.Services.Abstracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Core.Features.Order.Commands.Handler
{
    internal class OrderCommandHandler : ResponseHandler,
                   IRequestHandler<PlaceOrderCommand, Response<PlaceOrderResult>>
    {
        private readonly IMapper mapper;
        private readonly IOrderServices orderServices;

        public OrderCommandHandler(IMapper mapper, IOrderServices orderServices)
        {
            this.mapper = mapper;
            this.orderServices = orderServices;
        }


        public async Task<Response<PlaceOrderResult>> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
        {
            var result = await orderServices.PlaceOrder(request.UserId);

            var order = result.Item1;
            var message = result.Item2;

            if(message != null)
            {
                return message switch
                {
                    "NotFoundUser" => BadRequest<PlaceOrderResult>("user not found!"),
                    "EmptyCart" => BadRequest<PlaceOrderResult>("cart is empty!"),
                    "NoQuantity" => BadRequest<PlaceOrderResult>($"Insufficiect product quantity!"),
                    "Failed" or _ => BadRequest<PlaceOrderResult>("something went wrong!")
                };
            }

            var orderResult = mapper.Map<PlaceOrderResult>(order);

            return Success<PlaceOrderResult>(orderResult);
        }
    }
}
