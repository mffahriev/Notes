using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    public class User : IdentityUser
    {
        public virtual IList<Catalog>? Catalogs { get; set; }

        public virtual IList<Note>? Nodes { get; set; }
    }
}
