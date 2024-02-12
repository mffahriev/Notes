using Core.Exceptions;
using FluentValidation;
using System.Net;
using System.Text.Json;

namespace RestNotes.Middlewars
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(
            RequestDelegate next, 
            ILogger<ExceptionHandlerMiddleware> logger
        )
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
            catch(ValidationException ex)
            {
                context.Response.Clear();
                context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(
                        new { messages = ex.Errors }
                    )
                );
            }
            catch (RegistrationException ex)
            {
                context.Response.Clear();
                context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(
                        new { message = ex.Message }
                    )
                );
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
