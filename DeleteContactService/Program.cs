using DeleteContactService.Messaging;
using Microsoft.AspNetCore.Builder;
using TechChallenge.Infrastructure.Messaging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddControllers();

builder.Services.AddSingleton<IRabbitMQPublisher, RabbitMQDeletePublisher>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Create Contact Service API V1");
    c.RoutePrefix = string.Empty; // Define a rota raiz como o Swagger UI
});

//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

