using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations
{
    public class NoteConfiguration : BaseEntityConfiguration<Note>
    {
        public override void Configure(EntityTypeBuilder<Note> builder)
        {
            base.Configure(builder);

            builder.ToTable("Notes");
            builder.HasIndex(x => new { x.User, x.Catalog, x.Name }).IsUnique();
            builder.Property(x => x.Name).IsRequired();
            builder.HasOne(x => x.User).WithMany(x => x.Notes);
            builder.HasOne(x => x.Catalog).WithMany(x => x.ChildrenNotes);
        }
    }
}
