using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentWorkTimeTracker.Middleware
{
    public class ClaimRouteProtectionMiddleware
    {
        private readonly RequestDelegate _next;

        public ClaimRouteProtectionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.Value.Contains("claim") && !context.Session.Keys.Contains("userId"))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("No access");
            }
            await _next(context);
        }
    }
}