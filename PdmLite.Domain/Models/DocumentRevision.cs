namespace PdmLite.Domain.Models;

public class DocumentRevision
{
    public Guid Id { get; init; }
    public Guid DocumentId { get; init; }
    public int RevisionNumber { get; init; }
    public string Comment { get; private set; } = null!;
    public DateTimeOffset CreatedAt { get; init; }
    public Document Document { get; private set; } = null!;

    public DocumentRevision()
    {
    }

    public DocumentRevision(Guid id, Guid documentId, int revisionNumber, string comment, DateTimeOffset createdAt)
    {
        Id = id;
        DocumentId = documentId;
        RevisionNumber = revisionNumber;
        Comment = comment;
        CreatedAt = createdAt;
    }
}