namespace PdmLite.Domain.Models;

public class Document
{
    public Guid Id { get; init; }
    public string Designation { get; set; }
    public string Title { get; set; }
    public int Revision { get; set; }
    public DateTimeOffset CreatedAt { get; init; }
    
    private readonly List<DocumentRevision> _revisions = new();
    public IReadOnlyCollection<DocumentRevision> Revisions => _revisions.AsReadOnly();
    
    private Document() { }
    
    public Document(Guid id, string designation, string title, int revision, DateTimeOffset createdAt)
    {
        Id = id;
        Designation = designation;
        Title = title;
        Revision = revision;
        CreatedAt = createdAt;
    }
    
    public DocumentRevision AddRevision(string comment)
    {
        var nextRevisionNumber = _revisions.Count == 0 ? Revision + 1 : _revisions.Max(r => r.RevisionNumber) + 1;
        var revision = new DocumentRevision(Guid.NewGuid(), Id, nextRevisionNumber, comment, DateTimeOffset.UtcNow);
    
        _revisions.Add(revision);
        Revision = nextRevisionNumber;
    
        return revision;
    }
}