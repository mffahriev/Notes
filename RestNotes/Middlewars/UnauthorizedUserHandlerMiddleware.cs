using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using System.Net;

namespace RestNodes.Middlewars
{
    public class UnauthorizedUserHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<UnauthorizedUserHandlerMiddleware> _logger;

        public UnauthorizedUserHandlerMiddleware(
            RequestDelegate next, 
            ILogger<UnauthorizedUserHandlerMiddleware> logger
        )
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            bool isAuthorizedUser = context.Request.Headers.ContainsKey("Authorization");
            
            bool? hasAuthorizeAttribute = context
                .Features
                .Get<IEndpointFeature>()?
                .Endpoint?
                .Metadata
                .Any(x => x is AuthorizeAttribute
                     || x is AllowAnonymousAttribute
                );

            if(hasAuthorizeAttribute == true && !isAuthorizedUser)
            {
                context.Response.Clear();
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

                return;
            }

            await _next(context);
        }
    }
}
