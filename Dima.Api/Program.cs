using Dima.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>( d => d.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s => s.CustomSchemaIds(n => n.FullName));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Hello World!");

app.Run();
