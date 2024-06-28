using Dima.Api.Data;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Linq;

namespace Dima.Api.Handers
{
    public class CategoryHandler(AppDbContext context) : ICategoryHandler
    {
        public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest createCategoryRequest)
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

                return new Response<Category?>(category, 201, "Categoria criada com sucesso.");
            }
            catch
            {
                return new Response<Category?>(null, 500, "Erro ao criar categoria.");
            }
        }
        
        public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest updateCategoryRequest)
        {
            try
            {
                var category =  FindCategory(updateCategoryRequest.Id, updateCategoryRequest.UserId);

                if (category is null)
                    return new Response<Category?>(null, 404, "não foi possível encontrar a categoria");

                category.Title = updateCategoryRequest.Title;
                category.Description = updateCategoryRequest.Description;

                context.Categories.Update(category);

                await context.SaveChangesAsync();

                return new Response<Category?>(category, message: "Categoria atualizada com sucesso.");
            }
            catch
            {
                return new Response<Category?>(null, 500, "Erro ao atualizar categoria.");
            }
        }
        
        public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest deleteCategoryRequest)
        {
            try
            {
                var category = FindCategory(deleteCategoryRequest.Id, deleteCategoryRequest.UserId);

                if (category is null)
                    return new Response<Category?>(null, 404, "não foi possível encontrar a categoria");

                context.Categories.Remove(category);
                await context.SaveChangesAsync();

                return new Response<Category?>(category, message: "Categoria escluída com sucesso.");
            }
            catch
            {
                return new Response<Category?>(null, 500, "Erro ao excluir categoria.");
            }
        }
        
        public async Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest getCategoryByIdRequest)
        {
            try
            {
                var category = await context.Categories.AsNoTracking().FirstOrDefaultAsync(i => i.Id == getCategoryByIdRequest.Id && i.UserId == getCategoryByIdRequest.UserId);

                return category is null ? new Response<Category?>(null, 404, "não foi possível encontrar a categoria") : new Response<Category?>(category);
            }
            catch
            {
                return new Response<Category?>(null, 500, "Não foi possível encontrar uma categoria.");
            }
        }

        public async Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoriesRequest getAllCategoryRequest)
        {
            try
            {
                var query = context.Categories.AsNoTracking().Where(i => i.UserId == getAllCategoryRequest.UserId).OrderBy(t => t.Title);

                var categories = await query.Skip((getAllCategoryRequest.PageNumber - 1) * getAllCategoryRequest.PageSize)
                                            .Take(getAllCategoryRequest.PageSize)
                                            .ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Category>>(categories, count, getAllCategoryRequest.PageNumber, getAllCategoryRequest.PageSize);
            }
            catch
            {
                return new PagedResponse<List<Category>>(null, 500, "Não foi possível consultar as categoria.");
            }
        }

        private Category? FindCategory(long idCategory, string userId)
            => context.Categories.FirstOrDefault(i => i.Id == idCategory && i.UserId == userId);
    }
}
