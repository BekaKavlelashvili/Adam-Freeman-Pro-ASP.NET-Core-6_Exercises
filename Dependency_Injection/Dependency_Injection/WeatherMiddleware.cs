namespace Dependency_Injection
{
    public class WeatherMiddleware
    {
        private RequestDelegate next;

        public WeatherMiddleware(RequestDelegate nextDelegate)
        {
            this.next = nextDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path == "/middleware/class")
                await context.Response.WriteAsync("Middleware Class: it's raining in London");
            else
                await next(context);
        }
    }
}