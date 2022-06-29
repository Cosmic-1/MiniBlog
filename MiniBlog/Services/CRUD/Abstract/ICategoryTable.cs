namespace MiniBlog.Services
{
    public interface ICategoryTable
    {
        Task AddCategoryAsync(Category? category);
        Task<Category?> GetCategoryByIdAsync(int id);
        Task<Category?> GetCategoryByNameAsync(string? name);
        Task DeleteCategoryAsync(int id);
        Task DeleteCategoryAsync(Category? category);
        Task UpdateCategoryAsync(Category? category);
        Task<Category[]> GetAllCategoriesAsync();
        Task<Category[]> TakeCategoriesByPageAsync(int page, int size);
    }
}
