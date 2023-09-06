var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/test", (HttpContext context) => TestTask(context));
app.Run();

async Task TestTask(HttpContext httpContext)
{
    var msg = "Test for asp net core message";
    await httpContext.Response.WriteAsync(msg);
}