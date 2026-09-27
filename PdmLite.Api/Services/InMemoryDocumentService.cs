using System.Collections.Concurrent;
using PdmLite.Api.Models;
using PdmLite.Domain.Models;

namespace PdmLite.Api.Services;

public class InMemoryDocumentService: IDocumentService
{
    private readonly ConcurrentDictionary<Guid, Document> _documents = new();
    
    public Task<Document> CreateAsync(string designation, string title, CancellationToken ct)
    {
        var document = new Document(Guid.NewGuid(), designation, title, 1, DateTimeOffset.UtcNow);
        _documents[document.Id] = document;
        return Task.FromResult(document);
    }

    public Task<Document> GetByIdAsync(Guid id, CancellationToken ct)
    {
        _documents.TryGetValue(id, out var document);
        return Task.FromResult(document);
    }

    public Task<List<Document>> GetAllAsync(CancellationToken ct)
    {
        return Task.FromResult(_documents.Values.ToList());
    }

    public Task<DocumentDetailsResponseDto?> GetDetailsByIdAsync(Guid id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<RevisionResponseDto> CreateRevisionAsync(Guid documentId, string comment, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}