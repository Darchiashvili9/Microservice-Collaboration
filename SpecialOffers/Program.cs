var builder = WebApplication.CreateBuilder(args);



builder.Services.Scan(selector => selector.FromAssemblyOf<IStartup>()
    .AddClasses((c => c.Where(t => t.GetMethods().All(m => m.Name != "<Clone>$"))))
    .AsImplementedInterfaces());





builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseRouting();
app.MapControllers();

app.Run();
