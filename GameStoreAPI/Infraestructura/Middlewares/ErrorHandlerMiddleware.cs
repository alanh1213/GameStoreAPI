using GameStoreAPI.Aplicacion.Excepciones;
using System.Net;
using System.Text.Json;

namespace GameStoreAPI.Infraestructura.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly IConfiguration _iConfiguration;
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(IConfiguration iConfiguration, RequestDelegate next)
        {
            _iConfiguration = iConfiguration;
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

                string _message = string.Empty;
                switch (error)
                {
                    case NotFoundException:
                        _message = "Not Found";
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        _message = "Internal server error";
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new { message = _message });
                await response.WriteAsync(result);
            }
        }
    }
}
}
