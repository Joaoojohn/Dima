using Dima.Api.Data;
using Dima.Core.Enums;
using Dima.Core.Extentions;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handers
{
    public class TransactionHandler(AppDbContext context) : ITransactionHandler
    {
        public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
        {

            try
            {
                if (request is { Type: ETransactionType.Withdraw, Amount: > 0 })
                    request.Amount *= -1;

                var transaction = new Transaction
                {
                    UserId = request.UserId,
                    Title = request.Title,
                    Amount = request.Amount,
                    CategoryId = request.CategoryId,
                    PaidOrReceivedAt = request.PaidOrReceivedAt,
                    Type = request.Type,
                };

                await context.Transactions.AddAsync(transaction);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(transaction, 201, message: "Transação criada com sucesso.");
            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Não foi possível criar sua transação");
            }
        }

        public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
        {
            try
            {
                var transaction = await context.Transactions.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (transaction is null)
                    return new Response<Transaction?>(null, 404, "Transação não encontrada.");

                context.Transactions.Remove(transaction);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(transaction, message: "Transação excluída com sucesso.");
            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Não foi possível recuperar sua transação");
            }
        }

        public async Task<Response<Transaction?>> GetTransactionByIdAsync(GetTransactionByIdRequest request)
        {
            try
            {
                var transaction = await context.Transactions.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                return transaction is null ? new Response<Transaction?>(null, 404, "Transação não encontrada") : new Response<Transaction?>(transaction);
            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Não foi possível recuperar sua transação");
            }
        }

        public async Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionsByPeriodRequest request)
        {
            try
            {
                request.StartDate ??= DateTime.Now.GetFirstDay();
                request.EndDate ??= DateTime.Now.GetLastDay();

                var query = context.Transactions.AsNoTracking()
                                .Where(i =>
                                       i.PaidOrReceivedAt >= request.StartDate &&
                                       i.PaidOrReceivedAt <= request.EndDate &&
                                       i.UserId == request.UserId)
                                .OrderBy(o => o.PaidOrReceivedAt);


                var transactions = await query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Transaction>?>(transactions, count, request.PageNumber, request.PageSize);
            }
            catch
            {

                return new PagedResponse<List<Transaction>?>(null, 500, "Não foi possível obter as transações");
            }
        }

        public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
        {
            try
            {
                if (request is { Type: ETransactionType.Withdraw, Amount: > 0 })
                    request.Amount *= -1;

                var transaction = await context.Transactions.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (transaction is null)
                    return new Response<Transaction?>(null, 404, "Transação não encontrada.");

                transaction.CategoryId = request.CategoryId;
                transaction.Amount = request.Amount;
                transaction.Title = request.Title;
                transaction.Type = request.Type;
                transaction.PaidOrReceivedAt = request.PaidOrReceivedAt;

                context.Transactions.Update(transaction);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(transaction);
            }
            catch
            {

                return new Response<Transaction?>(null, 500, "Não foi possível atualizar sua transação");
            }
        }
    }
}
