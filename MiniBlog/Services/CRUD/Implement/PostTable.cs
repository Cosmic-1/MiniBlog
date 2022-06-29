namespace MiniBlog.Services
{
    public class PostTable : IPostTable
    {
        private readonly BlogDB blog;

        public PostTable(BlogDB blog)
        {
            this.blog = blog;
        }

        public async Task DeletePostAsync(Post? post)
        {
            if (post is null) return;

            blog.Posts.Remove(post);
            await blog.SaveChangesAsync();
        }

        public async Task DeletePostByIdAsync(int id)
        {
            var post = await blog.Posts.SingleOrDefaultAsync(x => x.Id == id);

            if (post is null) return;

            blog.Posts.Remove(post);
            await blog.SaveChangesAsync();
        }

        public async Task<Post?> GetPostByIdAsync(int id)
            => await blog.Posts
            .Include(c => c.Category)
            .Include(c=> c.Comments)
            .SingleOrDefaultAsync(x => x.Id == id);

        public async Task<Post?> GetPostByNameAsync(string? name)
            => name is null
            ? null : await blog.Posts
            .Include(c => c.Category)
            .Include(c => c.Comments)
            .SingleOrDefaultAsync(post => post.Title == name);


        public async Task UpdatePostAsync(Post? post)
        {
            if (post is null) return;
            blog.Posts.Update(post);
            await blog.SaveChangesAsync();
        }

        public async Task<Post[]> GetPostsAsync()
            => await blog.Posts.ToArrayAsync();

        public async Task<Post[]> TakePostsByPageAsync(int page, int size)
            => await blog.Posts
            .Include(c => c.Category)
            .Include(c => c.Comments)
            .Skip(page * size)
            .Take(size)
            .ToArrayAsync();
        public async Task<Post[]> GetPostsByCategoryAsync(int categoryId)
            => await blog.Posts
            .Include(c => c.Category)
            .Include(c => c.Comments)
                .Where(post => post.Category.Id == categoryId)
                .ToArrayAsync();
        public async Task<Post[]> GetPostsByCategoryAsync(int categoryId, int page, int size)
        => await blog.Posts.Include(c=> c.Category)
                .Where(post => post.Category.Id == categoryId)
                .Skip(page * size)
                .Take(size)
                .ToArrayAsync();

        public async Task AddPostAsync(Post? post)
        {
            if (post is null) return;

            await blog.Posts.AddAsync(post);
            await blog.SaveChangesAsync();
        }
    }
}
