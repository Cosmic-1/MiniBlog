

namespace Repository.Models
{
    public abstract class ModelBase
    {
        public virtual int Id { get; set; }
        [StringLength(1000)]
        public virtual string Title { get; set; } = string.Empty;
        public virtual string Content { get; set; } = string.Empty;
        [StringLength(500)]
        public virtual string MetaTitle { get; set; } = string.Empty;
        [StringLength(500)]
        public virtual string MetaDescription { get; set; } = string.Empty;
        [StringLength(1000)]
        public virtual string MetaKeywords { get; set; } = string.Empty;
        [StringLength(1000)]
        public virtual string Slug { get; set; } = string.Empty;
    }
}

