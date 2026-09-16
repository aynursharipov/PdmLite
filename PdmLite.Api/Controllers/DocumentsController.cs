using Microsoft.AspNetCore.Mvc;
using PdmLite.Exceptions;
using PdmLite.Models;
using PdmLite.Services;

namespace PdmLite.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentsController(ILogger<DocumentsController> logger, IDocumentService documentService)
    : ControllerBase
{
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateAsync([FromBody] CreateDocumentRequest request, CancellationToken ct)
    {
        var document = await documentService.CreateAsync(request.Designation, request.Title, ct);

        return Created($"/api/documents/{document.Id}", document);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("{id:guid}")]
    public async Task<ActionResult> GetById([FromRoute] Guid id, CancellationToken ct)
    {
        var document = await documentService.GetByIdAsync(id, ct);
        
        return document == null 
            ? throw new DocumentNotFoundException(id)
            : Ok(document);
    }
}