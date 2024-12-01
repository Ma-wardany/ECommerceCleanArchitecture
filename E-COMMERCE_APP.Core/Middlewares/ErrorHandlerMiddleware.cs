using E_COMMERCE_APP.Core.Bases;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;


namespace E_COMMERCE_APP.Core.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch(Exception ex)
            {
                var httpResponse = context.Response;
                var responseModel = new Response<string>();

                switch(ex)
                {
                    case UnauthorizedAccessException e:
                        
                        break;

                    case ValidationException e:
                        responseModel.Message = e.Message;
                        responseModel.StatusCode = HttpStatusCode.UnprocessableEntity;
                        httpResponse.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                        break;
                       
                    case Exception e:
                        if (e.GetType().ToString() == "ApiException")
                        {
                            responseModel.Message += e.Message;
                            responseModel.Message += e.InnerException == null ? "" : "\n" + e.InnerException.Message;
                            responseModel.StatusCode = HttpStatusCode.BadRequest;
                            httpResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                        }
                        responseModel.Message = e.Message;
                        responseModel.Message += e.InnerException == null ? "" : "\n" + e.InnerException.Message;

                        responseModel.StatusCode = HttpStatusCode.InternalServerError;
                        httpResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;

                    default:
                        responseModel.Message = ex.Message;
                        responseModel.StatusCode = HttpStatusCode.UnprocessableEntity;
                        httpResponse.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                        break;
                }

                var result = JsonSerializer.Serialize(responseModel);
                await httpResponse.WriteAsync(result);
            }
        }
    }
}
