var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () => "Loyality Program - UsersController API!  Version 2.0");



app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();
