using SpecialOffers.Models;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddScoped<IEventStore,EventStore>();




builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () => "special offers!");

app.UseRouting();
app.MapControllers();

app.Run();
