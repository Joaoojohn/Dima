using Dima.Api.Common.Api;
using Dima.Core;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Categories
{
    public class GetAllCategoriesEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
              => app.MapGet("/", HendleAsync)
                                 .WithName("Categories: Get all")
                                 .WithSummary("Recupera todas as categorias de um usuário")
                                 .WithDescription("Recupera todas as categorias de um usuário")
                                 .WithOrder(5)
                                 .Produces<PagedResponse<List<Category>?>>();

        private static async Task<IResult> HendleAsync(ClaimsPrincipal user, ICategoryHandler handler, [FromQuery] int pageNumber = Configuration.DefaultPageNumber, [FromQuery] int pageSize = Configuration.DefaultPageSize)
        {
            var request = new GetAllCategoriesRequest
            {
                UserId = user.Identity?.Name ?? string.Empty,
                PageSize = pageSize,
                PageNumber = pageNumber
            };

            var result = await handler.GetAllAsync(request);

            return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
        }
    }
}
