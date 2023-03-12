//using Microsoft.Extensions.Options;
using Platform;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MessageOptions>(opts =>
{
    opts.CityName = "Albany";
});

var app = builder.Build();

app.UseMiddleware<LocationMiddleware>();

//app.MapGet("/location", async (HttpContext context, IOptions<MessageOptions> msgOpts) =>
//{
//    Platform.MessageOptions opts = msgOpts.Value;
//    await context.Response.WriteAsync($"{opts.CityName}, {opts.CountryName}");
//});

////pipeline branches

//((IApplicationBuilder)app).Map("/branch", branch =>
//{
//    //branch.UseMiddleware<Platform.QueryStringMiddleware>();

//    branch.Run(new Platform.QueryStringMiddleware().Invoke);
//});

//app.Use(async (context, next) =>
//{
//    await next();
//    await context.Response.WriteAsync($"\nStatus Code {context.Response.StatusCode}");
//});


////short-circuite

//app.Use(async (context, next) =>
//{
//    if (context.Request.Path == "/short")
//        await context.Response.WriteAsync($"Request Short Circuited");
//    else
//        await next();
//});

////custom middleware

//app.Use(async (context, next) =>
//{
//    if (context.Request.Method == HttpMethods.Get && context.Request.Query["custom"] == "true")
//    {
//        context.Response.ContentType = "text/plain";
//        await context.Response.WriteAsync("Customer Middleware \n");
//    }
//    await next();
//});

////class-based custom middleware
//app.UseMiddleware<Platform.QueryStringMiddleware>();

app.MapGet("/", () => "Hello World!");

app.Run();
