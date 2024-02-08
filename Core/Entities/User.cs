using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    public class User : IdentityUser
    {
        public User() 
        {
            Catalogs = new List<Catalog>();
            Notes = new List<Note>();
        }

        public User(string name, string email, string basePath) 
        {
            UserName = name;
            Email = email;
            Catalogs = new List<Catalog> { new(basePath, this)};
            Notes = new List<Note>();
        }

        public virtual IList<Catalog> Catalogs { get; set; }

        public virtual IList<Note> Notes { get; set; }
    }
}
