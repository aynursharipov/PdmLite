using Microsoft.EntityFrameworkCore;
using PdmLite.Domain.Models;
using PdmLite.Exceptions;
using PdmLite.Infrastructure;

namespace PdmLite.Services;

public class SqlDocumentService(PdmDbContext context) : IDocumentService
{
    public async Task<Document> CreateAsync(string designation, string title, CancellationToken ct)
    {
        var document = new Document(Guid.NewGuid(), designation, title, 1, DateTimeOffset.UtcNow);
        
        await context.Documents.AddAsync(document, ct);
        
        try
        {
            await context.SaveChangesAsync(ct);
        }
        catch (DbUpdateException)
        {
            throw new DocumentAlreadyExistsException(designation);
        }
       
        return document;
    }

    public async Task<Document?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var document  = await context.Documents.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id, ct);
        
        return document;
    }

    public Task<List<Document>> GetAllAsync(CancellationToken ct)
    {
        return context.Documents.AsNoTracking().ToListAsync(ct);
    }
}