using ClassifyErrorMessages.Application.Services;
using ClassifyErrorMessages.Core.ErrorHandling;
using ClassifyErrorMessages.Core.Requests;
using ClassifyErrorMessages.Extensions;
using ClassifyErrorMessages.Infrastructure.Localizer;
using Microsoft.Extensions.Localization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLocalization();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
builder.Services.AddSingleton<CompanyService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRequestLocalization(options =>
{
    var supportedCultures = new[] { "en", "pt", "pt-BR", "pt-PT", "pt-BR-SP" };

    options.SetDefaultCulture(supportedCultures[0])
        .AddSupportedCultures(supportedCultures)
        .AddSupportedUICultures(supportedCultures);

    options.ApplyCurrentCultureToResponseHeaders = true;
    options.FallBackToParentUICultures = true;
});

app.MapGet("/company/{id}", (CompanyService service, Guid id) =>
{
    var result = service.Get(id);

    if (result.IsSuccess)
        return Results.Ok(result.Value);

    if (result.Error.Code == ErrorConstants.CompanyNotFound)
        return result.ToProblem(StatusCodes.Status400BadRequest);

    return result.ToProblem();
});

app.MapPost("/company", (CompanyService service, CreateCompanyRequest request) =>
{
    var result = service.Create(request);

    if (result.IsSuccess)
        return Results.Created($"/company/{result.Value.Id}", result.Value);

    if (result.Error.Code == ErrorConstants.CompanyInvalidDocument || result.Error.Code == ErrorConstants.CompanyInvalidName)
        return result.ToProblem(StatusCodes.Status400BadRequest);

    return result.ToProblem();
});

app.Run();