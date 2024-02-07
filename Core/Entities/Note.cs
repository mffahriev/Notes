using Core.Abstractions;

namespace Core.Entities
{
    public class Note : BaseEntity
    {
        public required string Name { get; set; }

        public string? Text { get; set; }

        public virtual Catalog? Catalog { get; set; }

        public required virtual User User { get; set; }
    }
}
