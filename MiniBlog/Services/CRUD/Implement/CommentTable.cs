namespace MiniBlog.Services.CRUD
{
    public class CommentTable : ICommentTable
    {
        private readonly BlogDB blog;

        public CommentTable(BlogDB blog)
        {
            this.blog = blog;
        }

        public async Task DeleteCommentByIdAsync(int id)
        {
            var comment = await blog.Comments
                 .Where(comm => comm.Id == id)
                 .SingleOrDefaultAsync();

            if (comment is null) return;

            blog.Comments.Remove(comment);
            await blog.SaveChangesAsync();
        }

        public async Task<Comment[]> GetAllCommentsAsync()
            => await blog.Comments.ToArrayAsync();

        public async Task<Comment[]> GetCommentsByPageAsync(int page, int size)
            => await blog.Comments
                .Skip(page * size)
                .Take(size).ToArrayAsync();

        public async Task<Comment?> GetCommentByIdAsync(int id)
            => await blog.Comments
            .SingleOrDefaultAsync(comm => comm.Id == id);

        public async Task UpdateCommentAsync(Comment? comment)
        {
            if (comment is null) return;

            blog.Comments.Update(comment);

            await blog.SaveChangesAsync();
        }

        public async Task AddCommentAsync(Comment? comment)
        {
            if (comment is null) return;

            await blog.Comments.AddAsync(comment);

            await blog.SaveChangesAsync();
        }

        public async Task DeleteCommentAsync(Comment? comment)
        {
            if (comment is null) return;

            blog.Comments.Remove(comment);

            await blog.SaveChangesAsync();
        }
    }
}
