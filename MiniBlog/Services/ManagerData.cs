namespace MiniBlog.Services
{
    public class ManagerData
    {
        public ManagerData(ICategoryTable categories, 
            IPostTable posts, 
            ICommentTable comments, 
            IUserTable  users,
            ISettingsTable settings)
        {
            Categories = categories;
            Posts = posts;
            Comments = comments;
            Users = users;
            Settings = settings;
        }

        public ICategoryTable Categories { get; }
        public IPostTable Posts { get; }
        public ICommentTable Comments { get; }
        public IUserTable Users { get; }
        public ISettingsTable Settings { get; }
    }
}
