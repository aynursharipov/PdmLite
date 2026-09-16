namespace PdmLite.Domain.Models;

public class Document
{
    public Guid Id { get; set; }
    public string Designation { get; set; }
    public string Title { get; set; }
    public int Revision { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    
    private Document() { }
    
    public Document(Guid id, string designation, string title, int revision, DateTimeOffset createdAt)
    {
        Id = id;
        Designation = designation;
        Title = title;
        Revision = revision;
        CreatedAt = createdAt;
    }
}