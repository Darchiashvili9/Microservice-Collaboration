using SpecialOffers.Models;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddScoped<IEventStore,EventStore>();




builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () => "special offers! Version 2.0");

app.UseRouting();
app.MapControllers();

app.Run();
