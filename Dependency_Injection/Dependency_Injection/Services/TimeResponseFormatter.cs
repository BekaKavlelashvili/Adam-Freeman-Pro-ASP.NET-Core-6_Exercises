namespace Dependency_Injection.Services
{
    public class TimeResponseFormatter : IResponseFormatter
    {
        private ITimeStamping stamper;

        public TimeResponseFormatter(ITimeStamping stamper)
        {
            this.stamper = stamper;
        }

        public async Task Format(HttpContext context, string content)
        {
            await context.Response.WriteAsync($"{stamper.TimeStamp}: {content}");
        }
    }
}
