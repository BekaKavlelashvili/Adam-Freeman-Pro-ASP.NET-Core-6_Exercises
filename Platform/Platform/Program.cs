//using Microsoft.Extensions.Options;
using Platform;
using System.Runtime.ExceptionServices;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.Configure<MessageOptions>(opts =>
//{
//    opts.CityName = "Albany";
//});

var app = builder.Build();

//app.UseMiddleware<Population>();
//app.UseMiddleware<Capital>();

//app.UseRouting();

//app.MapGet("routing", async context =>
//{
//    await context.Response.WriteAsync("request was routed");
//});


//segment variables in url patterns
app.MapGet("{first:alpha:length(3)}/{second:bool}", async context =>
{
    await context.Response.WriteAsync("Request was Routed \n");
    foreach (var kvp in context.Request.RouteValues)
    {
        await context.Response.WriteAsync($"{kvp.Key}: {kvp.Value}\n");
    }
});

app.MapGet("capital/{country:regex(^uk|france|monaco$)}", Capital.Endpoint);
app.MapGet("size/{city?}", Population.Endpoint).WithMetadata(new RouteNameMetadata("population"));


//app.Run(async (context) =>
//{
//    await context.Response.WriteAsync("Terminal middleware reached");
//});

//app.UseMiddleware<LocationMiddleware>();

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

//app.MapGet("/", () => "Hello World!");

app.Run();
