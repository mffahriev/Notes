using Core.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace Core.Entities
{
    public class Catalog : BaseEntity
    {
        public Catalog()
        {
            ChildrenCatalogs = new List<Catalog>();
            ChildrenNotes = new List<Note>();
        }

        [SetsRequiredMembers]
        internal Catalog(string name, User user)
        {
            Name = name;
            User = user;
            ChildrenCatalogs = new List<Catalog>();
            ChildrenNotes = new List<Note>();
        }

        [SetsRequiredMembers]
        public Catalog(string name, User user, Catalog parentCatalog) : this(name, user)
        {
            ParentCatalog = parentCatalog;
        }

        public required string Name { get; set; }

        public required virtual User User { get; set; }

        public virtual Catalog? ParentCatalog { get; set; }

        public virtual IList<Catalog> ChildrenCatalogs { get; set;}

        public virtual IList<Note> ChildrenNotes { get; set; }

        public string FullPath 
        {  
            get
            {
                List<string> listParents = new List<string>();

                Catalog? tempCategory = this;

                while (tempCategory != null) 
                {
                    listParents.Add(Name);
                    tempCategory = tempCategory.ParentCatalog;
                }

                listParents.Reverse();

                return Path.Combine(listParents.ToArray());
            } 
        }
    }
}
