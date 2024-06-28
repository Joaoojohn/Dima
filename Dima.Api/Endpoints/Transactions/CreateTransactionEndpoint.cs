using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;

namespace Dima.Api.Endpoints.Transactions
{
    public class CreateTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
           => app.MapPost("/", HendleAsync)
                              .WithName("Transactions: create")
                              .WithSummary("Criar uma nova transação")
                              .WithDescription("Criar uma nova transação")
                              .WithOrder(1)
                              .Produces<Response<Transaction?>>();

        private static async Task<IResult> HendleAsync(ITransactionHandler handler, CreateTransactionRequest request)
        {
            request.UserId = "joaoojohn";
            var result = await handler.CreateAsync(request);

            return result.IsSuccess ? Results.Created($"{result}", result.Data) : Results.BadRequest(result);
        }
    }
}
