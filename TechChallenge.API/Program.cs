using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using TechChallenge.Application.Services;
using TechChallenge.Infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddControllers()
//    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>()); 

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ContactDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoPadrao")));

builder.Services.AddTransient<IContactService, ContactService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
