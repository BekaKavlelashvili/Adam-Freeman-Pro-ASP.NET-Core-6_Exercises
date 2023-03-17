using Dependency_Injection.Services;

namespace Dependency_Injection
{
    public class WeatherMiddleware
    {
        private RequestDelegate next;
        //private IResponseFormatter formatter;

        public WeatherMiddleware(RequestDelegate nextDelegate, IResponseFormatter respFormatter)
        {
            this.next = nextDelegate;
            //this.formatter = respFormatter;
        }

        public async Task Invoke(HttpContext context, IResponseFormatter formatter)
        {
            if (context.Request.Path == "/middleware/class")
                await formatter.Format(context, "Middleware Class: it's raining in London");
            else
                await next(context);
        }
    }
}