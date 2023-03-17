using Dependency_Injection.Services;

namespace Dependency_Injection
{
    public class WeatherEndpoint
    {
        //private IResponseFormatter formatter;

        //public WeatherEndpoint(IResponseFormatter formatter)
        //{
        //    this.formatter = formatter;
        //}

        public async Task Endpoint(HttpContext context, IResponseFormatter formatter)
        {
            await formatter.Format(context, "Enpoint Class: It is cloudy in Milan");
        }
    }
}
