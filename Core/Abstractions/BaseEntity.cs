namespace Core.Abstractions
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            Id = Guid.NewGuid();
            Created = DateTime.UtcNow;
            Updated = default;
        }

        public required Guid Id { get; set; }

        public required DateTimeOffset Created { get; set; }

        public DateTimeOffset? Updated { get; set; }
    }
}
