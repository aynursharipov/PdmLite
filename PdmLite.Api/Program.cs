using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PdmLite.Exceptions;
using PdmLite.Filters;
using PdmLite.Infrastructure;
using PdmLite.Services;
using PdmLite.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddValidatorsFromAssemblyContaining<CreateDocumentRequestValidator>();
builder.Services.AddScoped<IDocumentService, SqlDocumentService>();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<DocumentActionFilter>();
});
builder.Services.AddExceptionHandler<DocumentNotFoundExceptionHandler>();
builder.Services.AddExceptionHandler<DocumentAlreadyExistsExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddDbContext<PdmDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PdmDbContext"));
});

var app = builder.Build();


app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();
app.Run();