using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Transactions
{
    public class GetTransactionByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
             => app.MapGet("/{id}", HendleAsync)
                                .WithName("Transactions: Get by id")
                                .WithSummary("Recupera uma transação ")
                                .WithDescription("Recupera uma transação")
                                .WithOrder(4)
                                .Produces<Response<Transaction?>>();

        private static async Task<IResult> HendleAsync(ClaimsPrincipal user, ITransactionHandler handler, long id)
        {
            var request = new GetTransactionByIdRequest()
            {
                UserId = user.Identity?.Name ?? string.Empty,
                Id = id
            };

            var result = await handler.GetTransactionByIdAsync(request);

            return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
        }
    }
}
