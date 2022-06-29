namespace Repository;
public class BloggingContextFactory : IDesignTimeDbContextFactory<BlogDB>
{
    public BlogDB CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BlogDB>();
        optionsBuilder.UseSqlite(@"Data Source=blog.db;");

        return new BlogDB(optionsBuilder.Options);
    }
}

