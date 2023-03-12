using Microsoft.Extensions.Options;
using System.Runtime.CompilerServices;

namespace Platform
{
    public class QueryStringMiddleware
    {
        private RequestDelegate? next;

        public QueryStringMiddleware()
        {
        }

        public QueryStringMiddleware(RequestDelegate nextDelegate)
        {
            next = nextDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Method == HttpMethods.Get && context.Request.Query["custom"] == "true")
            {
                if (!context.Response.HasStarted)
                {
                    context.Response.ContentType = "text/plain";
                }
                await context.Response.WriteAsync("Class-based Middleware \n");
            }

            if (next != null)
                await next(context);
        }
    }

    public class LocationMiddleware
    {
        private RequestDelegate next;
        private MessageOptions options;

        public LocationMiddleware(IOptions<MessageOptions> opts, RequestDelegate nextDelegate)
        {
            options = opts.Value;
            next = nextDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path == "/location")
                await context.Response.WriteAsync($"{options.CityName}, {options.CountryName}");
            else
                await next(context);
        }
    }
}
