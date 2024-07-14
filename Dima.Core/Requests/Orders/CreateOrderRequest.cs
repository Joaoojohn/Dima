namespace Dima.Core.Requests.Orders
{
    public class CreateOrderRequest : Request
    {
        public string Number { get; set; } = string.Empty;
    }
}
