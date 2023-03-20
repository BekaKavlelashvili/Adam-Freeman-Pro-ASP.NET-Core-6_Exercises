//using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.FileProviders;
using Platform;
using System.Runtime.ExceptionServices;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.Configure<RouteOptions>(opts =>
//{
//    opts.ConstraintMap.Add("countryName", typeof(CountryRouteConstrait));
//});

//builder.Services.Configure<MessageOptions>(opts =>
//{
//    opts.CityName = "Albany";
//});


//app.Use(async (context, next) =>
//{
//    Endpoint? end = context.GetEndpoint();
//    if (end != null)
//    {
//        await context.Response.WriteAsync($"{end.DisplayName} Selected \n");
//    }
//    else
//    {
//        await context.Response.WriteAsync("No endpoint Selected \n");
//    }
//    await next();
//});

//app.UseMiddleware<Population>();
//app.UseMiddleware<Capital>();

//app.UseRouting();

//app.MapGet("routing", async context =>
//{
//    await context.Response.WriteAsync("request was routed");
//});


//segment variables in url patterns
//app.MapGet("{first:alpha:length(3)}/{second:bool}", async context =>
//{
//    await context.Response.WriteAsync("Request was Routed \n");
//    foreach (var kvp in context.Request.RouteValues)
//    {
//        await context.Response.WriteAsync($"{kvp.Key}: {kvp.Value}\n");
//    }
//});

//app.MapGet("capital/{country:countryName}", Capital.Endpoint);

//app.MapGet("capital/{country:regex(^uk|france|monaco$)}", Capital.Endpoint);
//app.MapGet("size/{city?}", Population.Endpoint).WithMetadata(new RouteNameMetadata("population"));

//app.Map("{number:int}", async context =>
//{
//    await context.Response.WriteAsync("Routed to the int endpoint");
//}).WithDisplayName("Int Endpoint").Add(b => ((RouteEndpointBuilder)b).Order = 1);

//app.Map("{number:double}", async context =>
//{
//    await context.Response.WriteAsync("Routed to the double endpoint");
//}).WithDisplayName("Double Endpoint").Add(b => ((RouteEndpointBuilder)b).Order = 2);

//app.MapFallback(async context =>
//{
//    await context.Response.WriteAsync("Routed to fallback endpoint");
//});

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

//var servicesConfig = builder.Configuration;
//builder.Services.Configure<MessageOptions>(servicesConfig.GetSection("Location"));

//var serviceEnv = builder.Environment;

var app = builder.Build();

//var pipeline = app.Configuration;
//var pipelineEnv = app.Environment;

//app.UseMiddleware<LocationMiddleware>();

//app.MapGet("config", async (HttpContext context, IConfiguration config, IWebHostEnvironment env) =>
//{
//    string defaultDebug = config["Logging:LogLevel:Default"];
//    await context.Response.WriteAsync($"The config setting is: {defaultDebug}");
//    await context.Response.WriteAsync($"\n The env setting is: {env.EnvironmentName}");
//    string wsID = config["WebService:Id"];
//    string wsKey = config["WebService:Key"];
//    await context.Response.WriteAsync($"\n The secret Id is:{wsID}");
//    await context.Response.WriteAsync($"\n The secret Key is:{wsKey}");
//});

//app.MapGet("/", async context =>
//{
//    await context.Response.WriteAsync("Hello World!");
//});

builder.Services.AddHttpLogging(opts =>
{
    opts.LoggingFields = HttpLoggingFields.RequestMethod 
    | HttpLoggingFields.RequestPath | HttpLoggingFields.ResponseStatusCode;
});

app.UseHttpLogging();

//var logger = app.Services.GetRequiredService<ILoggerFactory>().CreateLogger("Pipeline");

//logger.LogDebug("Pipeline configuration starting");

app.UseStaticFiles();

var env = app.Environment;
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider($"{env.ContentRootPath}/staticFiles"),
    RequestPath="/files"
});

app.MapGet("population/{city?}", Population.Endpoint);

//logger.LogDebug("Pipeline configuration complete");

app.Run();
