namespace MiniBlog.Services
{
    public interface IPostTable
    {
        Task<Post?> GetPostByIdAsync(int id);
        Task<Post?> GetPostByNameAsync(string? name);
        Task AddPostAsync(Post? post);
        Task DeletePostAsync(Post? post);
        Task DeletePostByIdAsync(int id);
        Task UpdatePostAsync(Post? post);
        Task<Post[]> GetPostsAsync();
        Task<Post[]> TakePostsByPageAsync(int page, int size);
        Task<Post[]> GetPostsByCategoryAsync(int categoryId);
        Task<Post[]> GetPostsByCategoryAsync(int categoryId, int page, int size);
    }
}
