using System.Net;

namespace OnlineShopProject.Middlewares
{
    public class GlobalExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;
        public GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogInformation($"Задача отменена");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }
    }
}
