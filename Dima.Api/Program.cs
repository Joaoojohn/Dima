using Dima.Api.Common.Api;
using Dima.Api.Endpoints;
using Dima.Api.Models;

var builder = WebApplication.CreateBuilder(args);

builder.AddConnectionString();
builder.AddSegurity();
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.ConfigureDevEnvironment();

app.UseSecurity();
app.MapEndpoints();

app.Run();
