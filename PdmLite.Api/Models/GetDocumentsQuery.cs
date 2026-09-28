namespace PdmLite.Api.Models;

public record GetDocumentsQuery(
    string? SearchTerm = null,
    int PageNumber = 1,
    int PageSize = 20
);