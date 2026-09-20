namespace PdmLite.Exceptions;

public class DocumentAlreadyExistsException(string designation)
    : Exception($"Document with designation {designation} already exists");