using Dima.Core.Requests.Account;
using Dima.Core.Responses;

namespace Dima.Core.Handlers
{
    public interface IAccountHandler
    {
        Task<Response<LoginRequest>> LoginAsync(LoginRequest request);
        Task<Response<RegisterRequest>> RegisterAsync(RegisterRequest request);
        Task LogoutAsync();
    }
}
