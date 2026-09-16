namespace PdmLite.Exceptions;

public class DocumentNotFoundException(Guid guid) : Exception($"Document [{guid}] not found ");