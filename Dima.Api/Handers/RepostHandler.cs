using Dima.Api.Data;
using Dima.Core.Enums;
using Dima.Core.Handlers;
using Dima.Core.Models.Reports;
using Dima.Core.Requests.Reports;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handers
{
    public class RepostHandler(AppDbContext context) : IReportHandler
    {
        public async Task<Response<List<ExpensesByCategory>?>> GetExpensesByCategoryReportAsync(GetExpensesByCategoryRequest request)
        {
            try
            {
                var data = await context.ExpensesByCategory.AsNoTracking().Where(u => u.UserId == request.UserId).OrderByDescending(o => o.Year).ThenBy(t => t.Category).ToListAsync();

                return new Response<List<ExpensesByCategory>?>(data);
            }
            catch (Exception)
            {

                return new Response<List<ExpensesByCategory>?>(null, 400, "Não foi possivel realizar a consulta");
            }
        }

        public async Task<Response<FinancialSummary?>> GetFinancialSummaryReportAsync(GetFinancialSummaryRequest request)
        {
            try
            {
                var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                var data = await context.Transactions.AsNoTracking()
                    .Where(w =>
                            w.UserId == request.UserId &&
                            w.PaidOrReceivedAt >= startDate &&
                            w.PaidOrReceivedAt <= DateTime.Now
                           )
                    .GroupBy(g => true)
                    .Select(s =>
                            new FinancialSummary
                                (
                                    request.UserId,
                                    s.Where(t => t.Type == ETransactionType.Deposit).Sum(d => d.Amount),
                                    s.Where(t => t.Type == ETransactionType.Withdraw).Sum(d => d.Amount)
                                )
                            )
                    .FirstOrDefaultAsync();

                return new Response<FinancialSummary?>(data);

            }
            catch (Exception)
            {
                return new Response<FinancialSummary?>(null, 500, "Não foi possivel realizar a consulta");
            }
        }

        public async Task<Response<List<IncomesAndExpenses>?>> GetIncomesAndExpensesReportAsync(GetIncomesAndExpensesRequest request)
        {
            try
            {
                var data = await context.IncomesAndExpenses.AsNoTracking().Where(u => u.UserId == request.UserId).OrderByDescending(o => o.Year).ThenBy(t => t.Month).ToListAsync();

                return new Response<List<IncomesAndExpenses>?>(data);
            }
            catch (Exception)
            {

                return new Response<List<IncomesAndExpenses>?>(null, 500, "Não foi possivel realizar a consulta");
            }
        }

        public async Task<Response<List<IncomesByCategory>?>> GetIncomesByCategoryReportAsync(GetIncomesByCategoryRequest request)
        {
            try
            {
                var data = await context.IncomesByCategories.AsNoTracking().Where(u => u.UserId == request.UserId).OrderByDescending(o => o.Year).ThenBy(t => t.Category).ToListAsync();

                return new Response<List<IncomesByCategory>?>(data);
            }
            catch (Exception)
            {

                return new Response<List<IncomesByCategory>?>(null, 500, "Não foi possivel realizar a consulta");
            }
        }
    }
}
