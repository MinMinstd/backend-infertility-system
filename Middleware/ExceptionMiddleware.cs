using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace infertility_system.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }catch (CustomHttpException ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)ex.StatusCode;

                var result = JsonSerializer.Serialize(new
                {
                    status = ex.StatusCode,
                    message = ex.Message,
                });

                await context.Response.WriteAsync(result);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var result = JsonSerializer.Serialize(new
                {
                    status = HttpStatusCode.InternalServerError,
                    message = "Internal Server Error",
                });

                await context.Response.WriteAsync(result);
            }
        }
    }
}
