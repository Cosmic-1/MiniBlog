
namespace Repository;

public class BlogDB : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Settings> Settings { get; set; }

    public BlogDB(DbContextOptions<BlogDB> db) : base(db)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Settings>().HasData(new Settings
        {
            Id = 1,
            Title = "My Blog",
            Description = "Welcome to first my blog",
            Keywords = "blog, bloging, myblog",
            ViewSize = 10
        });

        modelBuilder.Entity<Category>().HasData(new Category
        {
            Id = 1,
            Title = "Other",
            MetaTitle = "Other",
            MetaDescription = "Other category",
            MetaKeywords = "Other",
            Slug = "other"
        });

        modelBuilder.Entity<User>().HasData(new User
        {
            Id = 1,
            Nickname = "demo",
            Description = "Hello to my blog",
            FullName = "Demo Demonovskii",
            Email = "Admin@admin.com",
            Password = "BC6D22A4A4C4AC5E1A7C3E0DEA35B7FE1542D384CDDA25357AC5472263859F51",//demo
            ImageAvatarLocal = "/images/avatar/demo.png",
        });

        base.OnModelCreating(modelBuilder);
    }
}
