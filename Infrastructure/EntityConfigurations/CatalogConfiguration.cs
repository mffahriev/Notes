using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations
{
    public class CatalogConfiguration : BaseEntityConfiguration<Catalog>
    {
        public override void Configure(EntityTypeBuilder<Catalog> builder)
        {
            base.Configure(builder);

            builder.ToTable("Catalogs");

            builder.Property(x => x.Name).IsRequired();
            builder.HasOne(x => x.User).WithMany(x => x.Catalogs);
            builder.HasOne(x => x.ParentCatalog).WithMany(x => x.ChildrenCatalogs);

        }
    }
}
