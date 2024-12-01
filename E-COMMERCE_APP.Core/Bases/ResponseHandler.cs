

using System.Net;

namespace E_COMMERCE_APP.Core.Bases
{
    public class ResponseHandler
    {
        public static Response<T> Success<T>(T? entity, string message = "Successfully")
        {
            return new Response<T>()
            {

                Data = entity!,
                Message = message,
                StatusCode = HttpStatusCode.OK
            };
        }

        public Response<T> UnProcessable<T>(string message = null)
        {
            return new Response<T>()
            {
                StatusCode = HttpStatusCode.UnprocessableEntity,
                Message = message ?? "Can not process"
            };
        }

        public Response<T> Created<T>(T entity)
        {
            return new Response<T>()
            {
                Data = entity,
                StatusCode = HttpStatusCode.Created,
                Message = "1 record added successfully"
            };
        }

        public Response<T> BadRequest<T>(string message = null)
        {
            return new Response<T>()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = message ?? "Bad request"
            };
        }

        public Response<T> NotFound<T>(string message = null)
        {
            return new Response<T>()
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = message ?? "Not Found",
                
            };
        }

        public Response<T> UnAuthorized<T>(string message = null)
        {
            return new Response<T>()
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Message = message ?? "UnAuthorized",

            };
        }
    }
}
