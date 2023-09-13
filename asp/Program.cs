using asp;
using Microsoft.AspNetCore.Server.Kestrel.Core.Features;
using System.Text.RegularExpressions;
using System.Web;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
DbContext dbContext = new DbContext();

//List<Animal> animals = new List<Animal>()
//{
//    new Animal("Monkey","Banana"),
//    new Animal("Monkey","Banana"),

//    new Animal("Monkey","Banana"),

//    new Animal("Monkey","Banana"),
//    new Animal("Monkey","Banana"),
//    new Animal("Monkey","Banana"),


//};
app.MapGet("/", () => "Hello World!");
//app.MapGet("/sum/{numberA:int}/{numberB:int}", (HttpResponse response, int numberA, int numberB) =>
//sum(response,numberA, numberB  ));
//app.MapGet("/minus/{numberA:int}/{numberB:int}", async (HttpResponse response, int numberA, int numberB) =>
//{
//    await response.WriteAsync($"{numberA} - {numberB} ={numberA - numberB} ");
//});

//app.MapPost("/animals/add", async (HttpResponse response, Animal animal) =>
//{
//    animals.Add(animal);
//    await response.WriteAsJsonAsync(animal);
//});
//app.MapGet("/animals", async (HttpResponse response ) =>
//{
    
//    await response.WriteAsJsonAsync(animals);
//});
//app.Run(TestTask);
app.MapGet("/users",(HttpResponse response ) =>dbContext.GetAll(response));
app.MapGet("/users/{id:guid}", (HttpResponse response, Guid id) => dbContext.Get(id, response));
app.MapPost("/users/add",(HttpResponse response, User user) =>dbContext.Insert(user, response));
app.MapPut("/users/update", (HttpResponse response, User user) => dbContext.Update(user, response));
app.MapDelete("/users/delete/{id:guid}", (HttpResponse response, Guid id) => dbContext.Delete(id, response));
app.Run();

//async Task sum(HttpResponse response, int numberA, int numberB)
//{
//    await response.WriteAsync($"{numberA} + {numberB} ={numberA - numberB} ");
//}
//async Task TestTask(HttpContext httpContext)
//{
//   var request = httpContext.Request;
//   var response = httpContext.Response;
//   string path = request.Path;
//    string guidExpression = @"^/\w{8}-\w{4}-\w{4}-\w{4}-\w{12}$";
//    if (path == "/")
//    {
//        await response.WriteAsync("Welcome to the Top Academy");
//    }
//    else if(path =="/all" && request.Method == "GET")
//    {
//        await dbContext.GetAll(response);
//    }
//    else if(Regex.IsMatch(path, guidExpression) && request.Method =="GET")
//    {
//        Guid? id = Guid.Parse(path.Split('/')[1]);
//        await dbContext.Get(id, response);
//    }
//    else if(path=="/add" && request.Method == "POST")
//    {
//        await dbContext.Insert(request, response);
//    }
//    else if(path== "/update" && request.Method == "POST") {
//        await dbContext.Update(request, response);

//    }
//    else if (Regex.IsMatch(path, guidExpression) && request.Method == "DELETE" )
//    {
//        Guid? id = Guid.Parse(path.Split('/')[1]);
//        await dbContext.Delete(id, response);
//    }

//}

      
//class Animal
//{
//    public Animal(string name, string description)
//    {
//        Name = name;
//        Description = description;
//    }

//    public string Name { get; set; }
//    public string Description { get; set; }

//}