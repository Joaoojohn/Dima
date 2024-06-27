using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;

namespace Dima.Api.Endpoints.Categories
{
    public class CreateCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPost("/", HendleAsync)
                               .WithName("Categories: create")
                               .WithSummary("Criar uma nova categoria")
                               .WithDescription("Criar uma nova categoria")
                               .WithOrder(1)
                               .Produces<Response<Category?>>();

        private static async Task<IResult> HendleAsync(ICategoryHandler handler, CreateCategoryRequest request)
        {
            request.UserId = "joaoojohn";
            var result = await handler.CreateAsync(request);

            return result.IsSuccess ? Results.Created($"{result}", result.Data) : Results.BadRequest(result);
        }
    }
}
