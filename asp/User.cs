using Microsoft.AspNetCore.Http;

namespace asp
{
    public class User
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }

        public User()
        {
            Id = Guid.NewGuid();
            Name = string.Empty;
            Password = string.Empty;
        }

        public User(Guid id, string? name, string? password)
        {
            Id = id;
            Name = name;
            Password = password;
        }
    }
    public interface IDbModel<T>
    {
        public Task Get(Guid? id, HttpResponse response);
        public Task GetAll(HttpResponse response);

        public Task Delete(Guid? id, HttpResponse response);
        public Task Update(HttpRequest request, HttpResponse response);
        public Task Insert(HttpRequest request, HttpResponse response);
    }

    public class DbContext : IDbModel<User>
    {
        List<User> models = new()
        {
            new User ( Guid.NewGuid(),"Jhon","qwerty"),
            new User ( Guid.NewGuid(),"Alise","123"),
            new User ( Guid.NewGuid(),"Denis","456"),
            new User ( Guid.NewGuid(),"Gleb","789"),
            new User ( Guid.NewGuid(),"Ivan","qwe75572rty"),
            new User ( Guid.NewGuid(),"Semen","7257dgh"),
            new User ( Guid.NewGuid(),"Sergey","dfhdsh"),
            new User ( Guid.NewGuid(),"Vasya","aefj")
        };
        public async Task Delete(Guid? id, HttpResponse response)
        {
            User? user = models.FirstOrDefault(x => x.Id == id);
            if(user != null) {
                models.Remove(user);

            }
            else
            {
                response.StatusCode = 400;
                await response.WriteAsJsonAsync(new { Error = "Incorrect data" });
            }

        }

        public async Task Get(Guid? id, HttpResponse response)
        {
            User? user=models.FirstOrDefault(x => x.Id == id);
            if(user!=null)
            {
                await response.WriteAsJsonAsync(user);

            }
            else
            {
                response.StatusCode = 404;
                await response.WriteAsJsonAsync(new {Error= "User not found"});
            }
        }

        public async Task GetAll(HttpResponse response)
        {
            await response.WriteAsJsonAsync(models);
        }

        public async Task Insert(HttpRequest request, HttpResponse response)
        {
            try
            {
                var storage = await request.ReadFromJsonAsync<User>();
                if (storage!=null)
                {
                    User user = new()
                    {
                        Id = storage.Id,
                        Name = storage.Name,
                        Password = storage.Password
                    };
                    models.Add(user);
                    await response.WriteAsJsonAsync(user);
                }
                else
                {
                    throw new Exception("Incorrect data");
                }
            }
            catch(Exception ex) { }
            {
                response.StatusCode = 400;
                await response.WriteAsJsonAsync(new { Error = "Incorrect data" });
            }
        }

        public async Task Update(HttpRequest request, HttpResponse response)
        {
           var user= await request.ReadFromJsonAsync<User>();
            if (user!=null)
            {
               User? u= models.FirstOrDefault(x => x.Id == user.Id);
                if (u != null)
                {
                    u.Name = user.Name;
                    u.Password = user.Password;
                }
                //foreach (var item in models)
                //{
                //    if (user.Id == item.Id)
                //    {
                //        item.Name = user.Name;
                //    }
                //}
            }
            else
            {
                response.StatusCode = 400;
                await response.WriteAsJsonAsync(new { Error = "Incorrect data" });
            }
        }
    }
}
