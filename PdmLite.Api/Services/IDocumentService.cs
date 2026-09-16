using PdmLite.Domain.Models;

namespace PdmLite.Services;

public interface IDocumentService
{
    Task<Document> CreateAsync(string designation, string title, CancellationToken ct);
    Task<Document> GetByIdAsync(Guid id, CancellationToken ct);
    Task<List<Document>> GetAllAsync(CancellationToken ct);
}