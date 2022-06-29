

namespace Repository.Models
{
    [Index(nameof(Title))]
    public class Category:ModelBase
    {
        public List<Post> Posts { get; set; } = new();
    }
}
