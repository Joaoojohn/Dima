using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using System.Net.Http.Json;

namespace Dima.Web.Handler
{
    public class CategoryHandler(IHttpClientFactory httpClientFactory) : ICategoryHandler
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);

        public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest createCategoryRequest)
        {
            var result = await _client.PostAsJsonAsync("v1/categories", createCategoryRequest);

            return await result.Content.ReadFromJsonAsync<Response<Category?>>() ?? new Response<Category?>(null, 400, "Falha ao criar categoria.");
        }

        public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest deleteCategoryRequest)
        {
            var result = await _client.DeleteAsync($"v1/categories/{deleteCategoryRequest.Id}");

            return await result.Content.ReadFromJsonAsync<Response<Category?>>() ?? new Response<Category?>(null, 400, "Falha ao deletar categoria.");
        }

        public async Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoriesRequest getAllCategoryRequest)
         => await _client.GetFromJsonAsync<PagedResponse<List<Category>>>("v1/categories/") ?? new PagedResponse<List<Category>>(null, 400, "Não foi possível obter as categorias.");

        public async Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest getCategoryByIdRequest)
         => await _client.GetFromJsonAsync<Response<Category?>>($"v1/categories/{getCategoryByIdRequest.Id}") ?? new Response<Category?>(null, 400, "Não foi possível obter a categoria.");

        public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest updateCategoryRequest)
        {
            var result = await _client.PutAsJsonAsync($"v1/categories/{updateCategoryRequest.Id}", updateCategoryRequest);

            return await result.Content.ReadFromJsonAsync<Response<Category?>>() ?? new Response<Category?>(null, 400, "Falha ao atualizar categoria.");
        }
    }
}
