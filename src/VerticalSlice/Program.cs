using Carter;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using VerticalSlice.Infrastructure.Persistence;

var assembly = typeof(Program).Assembly;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ProductsDbContext>(_ =>
{
    _.UseSqlServer(builder.Configuration.GetConnectionString("MsSql"));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(_ => _.RegisterServicesFromAssembly(assembly));
builder.Services.AddCarter();
builder.Services.AddValidatorsFromAssembly(assembly);

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapCarter();

app.UseHttpsRedirection();

app.Run();