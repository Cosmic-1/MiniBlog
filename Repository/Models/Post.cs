

namespace Repository.Models
{
    [Index(nameof(Title))]
    public class Post : ModelBase
    {
        [StringLength(400)]
        public string ShortContent { get; set; } = string.Empty;
        public bool IsPublished { get; set; } = true;
        public DateTime DateAddPost { get; set; } = DateTime.UtcNow;
        public Category Category { get; set; }
        public List<Comment> Comments { get; set; } = new();
        public User User { get; set; }
    }
}
