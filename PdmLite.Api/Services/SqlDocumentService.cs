using Microsoft.EntityFrameworkCore;
using PdmLite.Api.Exceptions;
using PdmLite.Api.Models;
using PdmLite.Domain.Models;
using PdmLite.Infrastructure;

namespace PdmLite.Api.Services;

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

    public async Task<PagedResult<Document>> GetPagedAsync(GetDocumentsQuery query, CancellationToken ct)
    {
        var pageNumber = query.PageNumber < 1 ? 1 : query.PageNumber;
        var pageSize = query.PageSize switch
        {
            < 1 => 10,
            > 100 => 100,
            _ => query.PageSize
        };

        var documentsQuery = context.Documents.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(query.SearchTerm))
        {
            var pattern = $"%{query.SearchTerm.Trim()}%";
            documentsQuery = documentsQuery.Where(x =>
                EF.Functions.ILike(x.Designation, pattern) ||
                EF.Functions.ILike(x.Title, pattern));
        }

        var count = await documentsQuery.CountAsync(ct);

        var documents = await documentsQuery
            .OrderByDescending(x => x.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return new PagedResult<Document>(documents, pageNumber, pageSize, count);
    }

    public Task<List<Document>> GetAllAsync(CancellationToken ct)
    {
        return context.Documents.AsNoTracking().ToListAsync(ct);
    }

    public async Task<DocumentDetailsResponseDto?> GetDetailsByIdAsync(Guid id, CancellationToken ct)
    {
        return await context.Documents
            .AsNoTracking()
            .Where(d => d.Id == id)
            .Select(d => new DocumentDetailsResponseDto(
                d.Id,
                d.Designation,
                d.Title,
                d.Revision,
                d.CreatedAt,
                d.Revisions
                    .OrderByDescending(r => r.RevisionNumber)
                    .Select(r => new RevisionResponseDto(r.RevisionNumber, r.Comment, r.CreatedAt))
                    .ToList()
            ))
            .FirstOrDefaultAsync(ct);
    }

    public async Task<RevisionResponseDto> CreateRevisionAsync(Guid documentId, string comment, CancellationToken ct)
    {
        var document  = await context.Documents.FirstOrDefaultAsync(d => d.Id == documentId, ct);
        
        if (document is null)
        {
            throw new DocumentNotFoundException(documentId);
        }
        
        var revision = document.AddRevision(comment);

        await context.AddAsync(revision, ct);
        
        await context.SaveChangesAsync(ct); 
        
        return new RevisionResponseDto(revision.RevisionNumber, revision.Comment, revision.CreatedAt);
    }
}