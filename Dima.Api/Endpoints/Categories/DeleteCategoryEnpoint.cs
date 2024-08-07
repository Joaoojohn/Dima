﻿using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Categories
{
    public class DeleteCategoryEnpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapDelete("/{id}", HendleAsync)
                               .WithName("Categories: delete")
                               .WithSummary("Deleta uma categoria")
                               .WithDescription("Deleta uma categoria")
                               .WithOrder(3)
                               .Produces<Response<Category?>>();

        private static async Task<IResult> HendleAsync(ICategoryHandler handler, ClaimsPrincipal user, long id)
        {
            var request = new DeleteCategoryRequest()
            {
                UserId = user.Identity?.Name ?? string.Empty,
                Id = id
            };
            
            var result = await handler.DeleteAsync(request);

            return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
        }
    }
}
