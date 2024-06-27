﻿using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;

namespace Dima.Api.Endpoints.Categories
{
    public class UpdateCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
             => app.MapPut("/{id}", HendleAsync)
                                .WithName("Categories: update")
                                .WithSummary("Atualiza uma categoria")
                                .WithDescription("Atualiza uma categoria")
                                .WithOrder(2)
                                .Produces<Response<Category?>>();

        private static async Task<IResult> HendleAsync(ICategoryHandler handler, UpdateCategoryRequest request, long id)
        {
            request.UserId = "joaoojohn";
            request.Id = id;

            var result = await handler.UpdateAsync(request);

            return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
        }
    }
}
