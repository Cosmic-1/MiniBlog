namespace Repository.Models;

[Index(nameof(Nickname))]
public class User
{
    public int Id { get; set; }
    [StringLength(100)]
    public string Nickname { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    [StringLength(200)]
    public string FullName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageAvatarLocal { get; set; } = string.Empty;
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    public List<Post> Posts { get; set; } = new();
}

