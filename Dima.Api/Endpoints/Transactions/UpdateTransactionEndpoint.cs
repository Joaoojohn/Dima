﻿using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;

namespace Dima.Api.Endpoints.Transactions
{
    public class UpdateTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPut("/{id}", HendleAsync)
                               .WithName("Transactions: update")
                               .WithSummary("Atualiza uma transação")
                               .WithDescription("Atualiza uma transação")
                               .WithOrder(2)
                               .Produces<Response<Transaction?>>();

        private static async Task<IResult> HendleAsync(ITransactionHandler handler, UpdateTransactionRequest request, long id)
        {
            request.UserId = "joaoojohn";
            request.Id = id;

            var result = await handler.UpdateAsync(request);

            return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
        }
    }
}
