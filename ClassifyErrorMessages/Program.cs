using ClassifyErrorMessages.Application.Requests;
using ClassifyErrorMessages.Application.Services;
using ClassifyErrorMessages.Infrastructure.Localizer;
using Microsoft.Extensions.Localization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLocalization();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
builder.Services.AddTransient<CompanyService>();

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

app.MapGet("/company/{id}", (CompanyService service, Guid id) => Results.Ok(service.Get(id)));

app.MapPost("/company", (CompanyService service, CreateCompanyRequest request) => Results.Ok(service.Create(request)));

app.Run();