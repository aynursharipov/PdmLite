using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PdmLite.Domain.Models;

namespace PdmLite.Infrastructure.Configurations;

public class DocumentRevisionConfiguration: IEntityTypeConfiguration<DocumentRevision>
{
    public void Configure(EntityTypeBuilder<DocumentRevision> builder)
    {
        builder.ToTable("document_revisions");
        builder.HasKey(pk => pk.Id);
        
        builder.Property(pk => pk.Comment)
            .HasMaxLength(500)
            .IsRequired();
        
        builder.Property(x => x.CreatedAt)
            .IsRequired();
        
        builder.HasIndex(x => new { x.DocumentId, x.RevisionNumber })
            .IsUnique();
    }
}