using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PdmLite.Domain.Models;

namespace PdmLite.Infrastructure.Configurations;

public class DocumentConfiguration: IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.ToTable("documents");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Designation)
            .HasMaxLength(50).IsRequired();
        builder.HasIndex(x => x.Designation).IsUnique();
        builder.Property(x => x.Title)
            .HasMaxLength(200).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
    }
}