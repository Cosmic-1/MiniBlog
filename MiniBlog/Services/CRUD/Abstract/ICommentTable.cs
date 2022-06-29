namespace MiniBlog.Services
{
    public interface ICommentTable
    {
        Task<Comment?> GetCommentByIdAsync(int id);
        Task DeleteCommentByIdAsync(int id);
        Task DeleteCommentAsync(Comment? comment);
        Task UpdateCommentAsync(Comment? comment);
        Task AddCommentAsync(Comment? comment);
        Task<Comment[]> GetAllCommentsAsync();
        Task<Comment[]> GetCommentsByPageAsync(int page, int size);
    }
}
