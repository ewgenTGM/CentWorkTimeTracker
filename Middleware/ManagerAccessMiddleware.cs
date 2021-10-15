using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentWorkTimeTracker.Middleware
{
    public class ManagerAccessMiddleware
    {
        private readonly RequestDelegate _next;

        public ManagerAccessMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Session.GetInt32("userRole") != 100 && (context.Request.Path.ToString().Contains("approve") || context.Request.Path.ToString().Contains("reject")))
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("No access");
            }
            await _next(context);
        }
    }
}