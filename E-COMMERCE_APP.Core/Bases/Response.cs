using System.Net;
using System.Text.Json.Serialization;

namespace E_COMMERCE_APP.Core.Bases
{
    public class Response<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T Data { get; set; }

        public Response() { }
        public Response(HttpStatusCode statusCode, string message = "No message provided")
        {
            StatusCode = statusCode;
            Message = message;
        }

        public Response(T data, HttpStatusCode statusCode, string message = "No message provided")
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }

        public Response(T data, string message = "No message provided")
        {
            Data = data;
            Message = message;
        }
    }
}
