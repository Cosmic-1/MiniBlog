namespace MiniBlog.Services
{
    public class CategoryTable : ICategoryTable
    {
        private readonly BlogDB blog;

        public CategoryTable(BlogDB blog)
        {
            this.blog = blog;
        }

        public async Task AddCategoryAsync(Category? category)
        {
            if (category is null) return;

            blog.Categories.Add(category);
            await blog.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var categoryFind = await blog.Categories
                .SingleOrDefaultAsync(cat => cat.Id == id);

            if (categoryFind is null) return;

            blog.Categories.Remove(categoryFind);
            await blog.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(Category? category)
        {
            if (category is null) return;

            blog.Categories.Remove(category);
            await blog.SaveChangesAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
            => await blog.Categories
            .FirstOrDefaultAsync(cat => cat.Id == id);

        public async Task<Category?> GetCategoryByNameAsync(string? name)
            => name is null
            ? null : await blog.Categories.SingleOrDefaultAsync(cat => cat.Title == name);

        public async Task<Category[]> GetAllCategoriesAsync()
            => await blog.Categories
            .ToArrayAsync();

        public async Task<Category[]> TakeCategoriesByPageAsync(int page, int size)
            => await blog.Categories
            .Skip(page * size)
            .Take(size)
            .ToArrayAsync();

        public async Task UpdateCategoryAsync(Category? category)
        {
            if (category is null) return;
            blog.Categories.Update(category);
            await blog.SaveChangesAsync();
        }
    }
}
