﻿using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;

namespace Dima.Api.Endpoints.Categories
{
    public class GetCategoryByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
             => app.MapGet("/{id}", HendleAsync)
                                .WithName("Categories: Get by id")
                                .WithSummary("Recupera uma categoria pelo id")
                                .WithDescription("Recupera uma categoria pelo id")
                                .WithOrder(4)
                                .Produces<Response<Category?>>();

        private static async Task<IResult> HendleAsync(ICategoryHandler handler, long id)
        {
            var request = new GetCategoryByIdRequest()
            {
                UserId = "joaoojohn",
                Id = id
            };

            var result = await handler.GetByIdAsync(request);

            return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
        }
    }
}
