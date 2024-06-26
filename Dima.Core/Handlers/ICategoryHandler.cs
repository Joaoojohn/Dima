using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;

namespace Dima.Core.Handlers
{
    public interface ICategoryHandler
    {
        Task<Response<Category>> CreateAsync(CreateCategoryRequest createCategoryRequest);
        Task<Response<Category>> UpdateAsync(UpdateCategoryRequest updateCategoryRequest);
        Task<Response<Category>> DeleteAsync(DeleteCategoryRequest deleteCategoryRequest);
        Task<Response<Category>> GetByIdAsync(GetCategoryByIdRequest getCategoryByIdRequest);
        Task<Response<List<Category>>> GetAllAsync(GetAllCategoriesRequest getAllCategoryRequest);
    }
}
