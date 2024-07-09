﻿using Dima.Core.Handlers;
using Dima.Core.Requests.Account;
using Dima.Core.Responses;
using System.Net.Http.Json;

namespace Dima.Web.Handler
{
    public class AccountHandler(IHttpClientFactory httpClientFactory) : IAccountHandler
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);

        public async Task<Response<string>> LoginAsync(LoginRequest request)
        {
            var result = await _client.PostAsJsonAsync("v1/identity/login?useCookies=true", request);
            
            return result.IsSuccessStatusCode 
            ? new Response<string>("Login realizado com sucesso.", 200, "Login realizado com sucesso.")
            : new Response<string>(null, 400, "Não foi possível realizar o login");
        }

        public Task LogoutAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Response<string>> RegisterAsync(RegisterRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
