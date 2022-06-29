namespace MiniBlog.Services.CRUD
{
    public class UserTable : IUserTable
    {
        private readonly BlogDB blog;

        public UserTable(BlogDB blog)
        {
            this.blog = blog;
        }

        public async Task AddUserAsync(User? post)
        {
            if (post is null) return;

            await blog.Users.AddAsync(post);
            await blog.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(User? post)
        {
            if (post is null) return;

            blog.Users.Remove(post);
           await blog.SaveChangesAsync();
        }

        public async Task DeleteUserByIdAsync(int id)
        {
           var post = await blog.Users.FirstOrDefaultAsync(x => x.Id == id);

           await this.DeleteUserAsync(post);
        }

        public async Task<User[]> GetAllUsersAsync() 
            => await blog.Users.ToArrayAsync();

        public async Task<User?> GetUserByIdAsync(int id)
            => await blog.Users.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<User?> GetUserByNicknameAsync(string? nickname) 
            => await blog.Users.FirstOrDefaultAsync(x => x.Nickname == nickname);

        public async Task<User[]> GetUsersByPageAsync(int page, int size) 
            => await blog.Users
            .Skip(page * size)
            .Take(size)
            .ToArrayAsync();

        public async Task UpdateUserAsync(User? post)
        {
            if (post is null) return;

            blog.Users.Update(post);
            await blog.SaveChangesAsync();
        }
    }
}
