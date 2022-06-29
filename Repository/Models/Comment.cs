

namespace Repository.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [StringLength(50)]
        public string Nickname { get; set; } = string.Empty;
        public DateTime DateAdd { get; set; } = DateTime.UtcNow;
        public Post Post { get; set; }
    }
}
