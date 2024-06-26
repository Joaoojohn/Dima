using Dima.Api.Data;
using Dima.Api.Handers;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>( d => d.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty));
builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s => s.CustomSchemaIds(n => n.FullName));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.MapPost("/v1/categories", async (CreateCategoryRequest request, ICategoryHandler handler) =>
     await handler.CreateAsync(request)).WithName("Categorias: create").WithSummary("Criar uma nova categoria").Produces<Response<Category?>>();


app.MapPut("/v1/categories/{id}", async (long id, UpdateCategoryRequest request, ICategoryHandler handler) =>
    {
        request.Id = id;
        return await handler.UpdateAsync(request);
    })
    .WithName("Categorias: update")
    .WithSummary("Atualiza uma nova categoria")
    .Produces<Response<Category?>>();
                                                                 

app.MapDelete("/v1/categories{id}", async (long id, ICategoryHandler handler) =>
    {
        var request = new DeleteCategoryRequest{ Id = id };
        return await handler.DeleteAsync(request);
    })
    .WithName("Categorias: delete")
    .WithSummary("Exclui uma nova categoria")
    .Produces<Response<Category?>>();


app.MapGet("/v1/categories{id}", async (long id, ICategoryHandler handler) =>
    {
        var request = new GetCategoryByIdRequest { Id = id };
        return await handler.GetByIdAsync(request);
    })
    .WithName("Categorias: Get by id")
    .WithSummary("Retorna uma nova categoria")
    .Produces<Response<Category?>>();


app.MapGet("/v1/categories", async (ICategoryHandler handler) =>
{
    var request = new GetAllCategoriesRequest { UserId = "teste" };
    return await handler.GetAllAsync(request);
})
    .WithName("Categorias: Get all")
    .WithSummary("Retorna todas as categorias de um usuário")
    .Produces<PagedResponse<List<Category>?>>();


app.Run();
