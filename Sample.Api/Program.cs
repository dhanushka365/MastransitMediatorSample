using MassTransit;
using Sample.Components.Consumers;
using Sample.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMassTransit(
    x =>
    {
        x.AddConsumer<SubmitOrderConsumer>();
        x.AddMediator(m => m.AddConsumersFromNamespaceContaining<SubmitOrderConsumer>());
        x.AddRequestClient<SubmitOrder>();

        x.UsingRabbitMq((context, cfg) =>
        {
            cfg.Host(new Uri("rabbitmq://localhost"), h => { /* additional settings */ });
            cfg.ConfigureEndpoints(context);
        });
    });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

// Enable MassTransit logging
var logger = app.Services.GetRequiredService<ILogger<Program>>();
var busControl = app.Services.GetRequiredService<IBusControl>();

try
{
    // Start MassTransit bus
    await busControl.StartAsync();

    logger.LogInformation("MassTransit bus started successfully.");
}
catch (Exception ex)
{
    logger.LogError(ex, "Error starting MassTransit bus");
}

app.Run();