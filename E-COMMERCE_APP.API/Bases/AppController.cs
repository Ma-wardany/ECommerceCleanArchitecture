using E_COMMERCE_APP.Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace E_COMMERCE_APP.API.Bases
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppController : ControllerBase
    {
        private IMediator _mediatorInstance;
        protected IMediator Mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();

        public ObjectResult FinalRespons<T>(Response<T> response)
        {
            return response.StatusCode switch
            {
                HttpStatusCode.OK                  => new OkObjectResult(response),
                HttpStatusCode.Unauthorized        => new UnauthorizedObjectResult(response),
                HttpStatusCode.Created             => new CreatedResult(string.Empty, response),
                HttpStatusCode.BadRequest          => new BadRequestObjectResult(response),
                HttpStatusCode.NotFound            => new NotFoundObjectResult(response),
                HttpStatusCode.UnprocessableEntity => new UnprocessableEntityObjectResult(response),
                _                                  => new BadRequestObjectResult(response)
            };
        }
    }
}
