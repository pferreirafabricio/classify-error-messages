using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;

namespace ClassifyErrorMessages.Infrastructure.Localizer;

public class JsonStringLocalizerFactory(IDistributedCache cache) : IStringLocalizerFactory
{
    private readonly IDistributedCache _cache = cache;

    public IStringLocalizer Create(Type resourceSource) =>
        new JsonStringLocalizer(_cache);

    public IStringLocalizer Create(string baseName, string location) =>
        new JsonStringLocalizer(_cache);
}
