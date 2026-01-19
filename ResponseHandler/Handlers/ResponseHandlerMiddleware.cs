using Common.Logging;
using Infrastructure.ResponseHandler.Commons;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Infrastructure.ResponseHandler.Handlers
{
    public class ResponseHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = ApiResponse<string>.Fail(error.Message);

                switch (error)
                {
                    case SomeException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        AppLogging.LogException(error.Message);
                        break;

                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        AppLogging.LogException(error.Message);
                        break;

                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        AppLogging.LogException(error.Message);
                        break;
                }
                responseModel.StatusCode= response.StatusCode;
                var result = JsonSerializer.Serialize(responseModel);
                await response.WriteAsync(result);
            }
        }
    }
}
