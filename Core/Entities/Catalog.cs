using Core.Abstractions;

namespace Core.Entities
{
    public class Catalog : BaseEntity
    {
        public required string Name { get; set; }

        public required virtual User User { get; set; }

        public virtual Catalog? ParentCatalog { get; set; }

        public virtual IList<Catalog>? ChildrenCatalogs { get; set;}

        public virtual IList<Node>? ChildrenNodes { get; set; }
    }
}
