using TechChallenge.Infrastructure.Messaging;
using UpdateContactService.Messaging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddControllers();

builder.Services.AddSingleton<IRabbitMQPublisher, RabbitMQUpdatePublisher>();

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
