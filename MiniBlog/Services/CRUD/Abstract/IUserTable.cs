namespace MiniBlog.Services.CRUD
{
    public interface IUserTable
    {
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByNicknameAsync(string? nickname);
        Task AddUserAsync(User? post);
        Task DeleteUserByIdAsync(int id);
        Task DeleteUserAsync(User? post);
        Task UpdateUserAsync(User? post);
        Task<User[]> GetAllUsersAsync();
        Task<User[]> GetUsersByPageAsync(int page, int size);
    }
}
