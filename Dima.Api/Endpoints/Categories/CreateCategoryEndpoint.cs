using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using System.Security.Claims;

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

        private static async Task<IResult> HendleAsync(ICategoryHandler handler, ClaimsPrincipal user, CreateCategoryRequest request)
        {
            request.UserId = user.Identity?.Name ?? string.Empty;
            var result = await handler.CreateAsync(request);

            return result.IsSuccess ? Results.Created($"{result}", result.Data) : Results.BadRequest(result);
        }
    }
}
