namespace Dependency_Injection
{
    public class WeatherEndpoint
    {
        public static async Task Endpoint(HttpContext context)
        {
            await context.Response.WriteAsync("Enpoint Class: It is cloudy in Milan");
        }
    }
}
