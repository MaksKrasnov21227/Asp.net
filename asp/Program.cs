using asp;
using Microsoft.AspNetCore.Server.Kestrel.Core.Features;
using System.Text.RegularExpressions;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
DbContext dbContext = new DbContext();
app.MapGet("/", () => "Hello World!");
app.Run(TestTask);
app.Run();

async Task TestTask(HttpContext httpContext)
{
   var request = httpContext.Request;
   var response = httpContext.Response;
   string path = request.Path;
    string guidExpression = @"^/\w{8}-\w{4}-\w{4}-\w{4}-\w{12}$";
    if (path == "/")
    {
        await response.WriteAsync("Welcome to the Top Academy");
    }
    else if(path =="/all" && request.Method == "GET")
    {
        await dbContext.GetAll(response);
    }
    else if(Regex.IsMatch(path, guidExpression) && request.Method =="GET")
    {
        Guid? id = Guid.Parse(path.Split('/')[1]);
        await dbContext.Get(id, response);
    }
    else if(path=="/add" && request.Method == "POST")
    {
        await dbContext.Insert(request, response);
    }
    else if(path== "/update" && request.Method == "POST") {
        await dbContext.Update(request, response);

    }
    else if (Regex.IsMatch(path, guidExpression) && request.Method == "DELETE" )
    {
        Guid? id = Guid.Parse(path.Split('/')[1]);
        await dbContext.Delete(id, response);
    }

}