using Core.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace Core.Entities
{
    public class Note : BaseEntity
    {
        public Note() { }

        [SetsRequiredMembers]
        public Note(string name, Catalog catalog, User user)
        {
            Name = name;
            Catalog = catalog;
            User = user;
        }

        [SetsRequiredMembers]
        public Note(string name, string? text, Catalog catalog, User user) 
        {
            Name = name;
            Text = text;
            Catalog = catalog;
            User = user;
        }

        public required string Name { get; set; }

        public string? Text { get; set; }

        public DateTimeOffset? LastUpdateText { get; set; }

        public required virtual Catalog Catalog { get; set; }

        public required virtual User User { get; set; }
    }
}
