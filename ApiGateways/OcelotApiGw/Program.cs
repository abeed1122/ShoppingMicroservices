using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);


//builder.Configuration.AddJsonFile("ocelot.Local.json");
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true);
});
builder.Host.ConfigureLogging((hostingContext, loggingbuilder) =>
{
    loggingbuilder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
    loggingbuilder.AddConsole();
    loggingbuilder.AddDebug();
});
builder.Services.AddOcelot().AddCacheManager(settings => settings.WithDictionaryHandle());

//builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => "Hello World!");
await app.UseOcelot();

app.Run();
