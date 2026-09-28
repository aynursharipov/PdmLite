using PdmLite.Api.Models;
using PdmLite.Domain.Models;

namespace PdmLite.Api.Services;

public interface IDocumentService
{
    Task<Document> CreateAsync(string designation, string title, CancellationToken ct);
    Task<Document?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<PagedResult<Document>> GetPagedAsync(GetDocumentsQuery query, CancellationToken ct);
    Task<DocumentDetailsResponseDto?> GetDetailsByIdAsync(Guid id, CancellationToken ct);
    Task<RevisionResponseDto> CreateRevisionAsync(Guid documentId, string comment, CancellationToken ct);
}