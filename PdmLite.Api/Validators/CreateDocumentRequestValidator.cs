using FluentValidation;
using PdmLite.Models;

namespace PdmLite.Validators;

public class CreateDocumentRequestValidator: AbstractValidator<CreateDocumentRequest>
{
    public CreateDocumentRequestValidator()
    {
        RuleFor(d => d.Designation).NotEmpty().Length(3, 50);
        RuleFor(d => d.Title).NotEmpty().Length(2, 200);
    }
}