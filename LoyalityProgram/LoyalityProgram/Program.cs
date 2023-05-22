var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () => "Loyality Program - UsersController API!");



app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();
