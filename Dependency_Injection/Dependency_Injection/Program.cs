using Dependency_Injection;
using Dependency_Injection.Services;

var builder = WebApplication.CreateBuilder(args);


//IWebHostEnvironment env = builder.Environment;

//if (env.IsDevelopment())
//{
//    builder.Services.AddScoped<IResponseFormatter, TimeResponseFormatter>();
//    builder.Services.AddScoped<ITimeStamping, DefaultTimeStamper>();
//}
//else
//{
//    builder.Services.AddScoped<IResponseFormatter, HtmlResponseFormatter>();
//}

//IConfiguration config = builder.Configuration;
//builder.Services.AddScoped<IResponseFormatter>(serviceProvider =>
//{
//    string? typeName = config["services:IResponseFormatter"];
//    return (IResponseFormatter)ActivatorUtilities
//        .CreateInstance(serviceProvider, typeName == null
//        ? typeof(GuidService) : Type.GetType(typeName, true)!);
//});

//builder.Services.AddScoped<ITimeStamping, DefaultTimeStamper>();

builder.Services.AddScoped<IResponseFormatter, TextResponseFormatter>();
builder.Services.AddScoped<IResponseFormatter, HtmlResponseFormatter>();
builder.Services.AddScoped<IResponseFormatter, GuidService>();

var app = builder.Build();


app.MapGet("single", async context =>
{
    IResponseFormatter formatter = context.RequestServices.GetRequiredService<IResponseFormatter>();
    await formatter.Format(context, "single service");
});

app.MapGet("/", async context =>
{
    IResponseFormatter formatter = context.RequestServices
    .GetServices<IResponseFormatter>().First(f => f.RichOutput);
    await formatter.Format(context, "Multiple Services");
});

//app.UseMiddleware<WeatherMiddleware>();

//IResponseFormatter formatter = new TextResponseFormatter();
//app.MapGet("middleware/function", async (HttpContext context, IResponseFormatter formatter) =>
//{
//    await formatter.Format(context, "Middleware Function: It is snowing in Chicago");
//});

//app.MapGet("endpoint/class", WeatherEndpoint.Endpoint);
//app.MapEndpoint<WeatherEndpoint>("endpoint/class");

//app.MapGet("endpoint/function", async (HttpContext context) =>
//{
//    IResponseFormatter formatter = context.RequestServices.GetRequiredService<IResponseFormatter>();
//    await formatter.Format(context, "Endpoint Function: It is sunny in LA");
//});

app.Run();
