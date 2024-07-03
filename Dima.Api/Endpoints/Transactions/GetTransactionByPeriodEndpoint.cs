using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Responses;
using Dima.Core;
using Microsoft.AspNetCore.Mvc;
using Dima.Core.Requests.Transactions;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Transactions
{
    public class GetTransactionByPeriodEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
             => app.MapGet("/", HendleAsync)
                         .WithName("Transactions: Get all")
                         .WithSummary("Recupera todas as transações")
                         .WithDescription("Recupera todas as transações")
                         .WithOrder(5)
                         .Produces<PagedResponse<List<Transaction>?>>();

        private static async Task<IResult> HendleAsync(ClaimsPrincipal user, ITransactionHandler handler, 
            [FromQuery] DateTime? starDate = null, 
            [FromQuery] DateTime? endDate = null, 
            [FromQuery] int pageNumber = Configuration.DefaultPageNumber, 
            [FromQuery] int pageSize = Configuration.DefaultPageSize)
        {
            var request = new GetTransactionsByPeriodRequest
            {
                UserId = user.Identity?.Name ?? string.Empty,
                PageSize = pageSize,
                PageNumber = pageNumber,
                StartDate = starDate,
                EndDate = endDate
            };

            var result = await handler.GetTransactionByPeriodAsync(request);

            return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
        }
    }
}
