using Dima.Api.Data;
using Dima.Api.Models;
using Dima.Core.Enums;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace Dima.Api.Handers
{
    public class OrderHandler(AppDbContext context, UserManager<User> userManager, SignInManager<User> signInManager) : IOrderHandler
    {
        public async Task<Response<Order?>> ConfirmOrderAsync(ConfirmOrderRequest request)
        {
            var order = await context.Orders.FirstOrDefaultAsync(f => f.UserId == request.UserId && f.Number == request.Number);

            if (order is null)
                return new Response<Order?>(null, 404, "Pedido não encontrado.");

            else if (order.Status == EOrderStatus.Paid)
                return new Response<Order?>(null, 400, "Este pedido já está confirmado.");

            var options = new ChargeSearchOptions
            {
                Query = $"metadata:['order']:'{request.Number}'",
                Limit = 1
            };

            var service = new ChargeService();

            var result = await service.SearchAsync(options);

            if (result is null)
                return new Response<Order?>(null, 404, "Pagamento não encontrado");

            var charge = result.Data.FirstOrDefault();

            if (charge is null)
                return new Response<Order?>(null, 404, "Pagamento não encontrado");

            if (charge.Paid is false)
                return new Response<Order?>(null, 404, "Este pedido ainda não foi pago");

            order.Status = EOrderStatus.Paid;
            order.UpdatedAt = DateTime.Now;
            order.ExternalReference = charge.Id;


            context.Orders.Update(order);
            await context.SaveChangesAsync();

            var user = await userManager.FindByEmailAsync(request.UserId);

            if (user is null)
                return new Response<Order?>(null, 500, "Perfil não encontrado");


            var addRoleResult = await userManager.AddToRoleAsync(user, "premium");

            if (!addRoleResult.Succeeded)
                return new Response<Order?>(null, 500, "Erro interno");

            await signInManager.SignOutAsync();
            await signInManager.SignInAsync(user, isPersistent: true);

            return new Response<Order?>(order);
        }

        public async Task<Response<Order?>> CreateOrderAsync(CreateOrderRequest request)
        {
            try
            {
                var order = new Order
                {
                    ExternalReference = "",
                    Amount = 199.99M,
                    UserId = request.UserId,
                    Getaway = EPaymentGetaway.Stripe,
                    Status = EOrderStatus.WaitPayment,
                };

                await context.AddAsync(order);
                await context.SaveChangesAsync();

                return new Response<Order?>(order);
            }
            catch
            {
                return new Response<Order?>(null, 500, "Não foi possível realizar o pedido");
            }
        }
    }
}
