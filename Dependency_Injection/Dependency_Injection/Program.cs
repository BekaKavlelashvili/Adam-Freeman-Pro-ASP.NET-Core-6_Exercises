using Dependency_Injection;
using Dependency_Injection.Services;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddSingleton(typeof(ICollection<>), typeof(List<>));

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

//builder.Services.AddScoped<IResponseFormatter, TextResponseFormatter>();
//builder.Services.AddScoped<IResponseFormatter, HtmlResponseFormatter>();
//builder.Services.AddScoped<IResponseFormatter, GuidService>();

var app = builder.Build();

app.MapGet("string", async context => {
    ICollection<string> collection
    = context.RequestServices.GetRequiredService<ICollection<string>>();
    collection.Add($"Request: {DateTime.Now.ToLongTimeString()}");
    foreach (string str in collection)
    {
        await context.Response.WriteAsync($"String: {str}\n");
    }
});

app.MapGet("int", async context => {
    ICollection<int> collection
    = context.RequestServices.GetRequiredService<ICollection<int>>();
    collection.Add(collection.Count() + 1);
    foreach (int val in collection)
    {
        await context.Response.WriteAsync($"Int: {val}\n");
    }
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
