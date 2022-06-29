
namespace Repository.Models
{
    public class Settings
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Keywords { get; set; } = string.Empty;
        public int ViewSize { get; set; }
    }
}
