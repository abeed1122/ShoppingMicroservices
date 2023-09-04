using Ordering.Infrastructure;
using Ordering.Application;
using Microsoft.Extensions.Configuration;
using Ordering.API.Extensions;
using Ordering.Infrastructure.Persistence;
using System.Reflection;
using MassTransit;
using EventBus.Messages.Common;
using Ordering.API.EventBusConsumer;

var builder = WebApplication.CreateBuilder(args);

//public IConfiguration? Configuration { get; }

var Configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(Configuration);
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<BasketCheckoutConsumer>();

// MassTransit-RabbitMQ Configuration
builder.Services.AddMassTransit(config => {
    config.AddConsumer<BasketCheckoutConsumer>();
    config.UsingRabbitMq((ctx, cfg) => {
       cfg.Host(Configuration["EventBusSettings:HostAddress"]);
        cfg.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue, c=>
        {
            c.ConfigureConsumer<BasketCheckoutConsumer>(ctx);
        });

    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MigrateDatabase<OrderContext>((context, services) =>
{
    var logger = services.GetService<ILogger<OrderContextSeed>>();
    OrderContextSeed.SeedAsync(context, logger).Wait();
}
);
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
