using Microsoft.EntityFrameworkCore;
using PdmLite.Domain.Models;

namespace PdmLite.Infrastructure;

public class PdmDbContext: DbContext
{
    public DbSet<Document> Documents => Set<Document>();
    public DbSet<DocumentRevision> DocumentRevisions => Set<DocumentRevision>();
    
    public PdmDbContext(DbContextOptions<PdmDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(PdmDbContext).Assembly);
    }
}