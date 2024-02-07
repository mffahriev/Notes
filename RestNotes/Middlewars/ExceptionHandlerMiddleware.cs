using System.Net;

namespace RestNodes.Middlewars
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Внутренняя ошибка сервера.");
                
                context.Response.Clear();
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }
    }
}
