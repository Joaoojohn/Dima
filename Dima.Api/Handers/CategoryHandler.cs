using Dima.Api.Data;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using System.Data.Common;

namespace Dima.Api.Handers
{
    public class CategoryHandler(AppDbContext context) : ICategoryHandler
    {
        public async Task<Response<Category>> CreateAsync(CreateCategoryRequest createCategoryRequest)
        {
            try
            {
                var category = new Category
                {
                    UserId = createCategoryRequest.UserId,
                    Description = createCategoryRequest.Description,
                    Title = createCategoryRequest.Title
                };

                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();

                return new Response<Category>(category);
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
                throw new Exception("Erro ao criar categoria");
            }
        }


        public Task<Response<Category>> DeleteAsync(DeleteCategoryRequest deleteCategoryRequest)
        {
            throw new NotImplementedException();
        }

        public Task<Response<List<Category>>> GetAllAsync(GetAllCategoriesRequest getAllCategoryRequest)
        {
            throw new NotImplementedException();
        }

        public Task<Response<Category>> GetByIdAsync(GetCategoryByIdRequest getCategoryByIdRequest)
        {
            throw new NotImplementedException();
        }

        public Task<Response<Category>> UpdateAsync(UpdateCategoryRequest updateCategoryRequest)
        {
            throw new NotImplementedException();
        }
    }
}
