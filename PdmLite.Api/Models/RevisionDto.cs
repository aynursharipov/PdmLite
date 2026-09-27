namespace PdmLite.Api.Models;

public record CreateRevisionRequest(string Comment);

public record RevisionResponseDto(
    int RevisionNumber,
    string Comment,
    DateTimeOffset CreatedAt
);

public record DocumentDetailsResponseDto(
    Guid Id,
    string Designation,
    string Title,
    int CurrentRevision,
    DateTimeOffset CreatedAt,
    List<RevisionResponseDto> Revisions
);