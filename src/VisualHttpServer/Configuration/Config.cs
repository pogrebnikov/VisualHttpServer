using Microsoft.Extensions.Configuration;
using VisualHttpServer.Core.Configuration;

namespace VisualHttpServer.Configuration;

internal class Config(IConfiguration configuration) : IConfig
{
    public IReadOnlyCollection<IHttpStatusCodeConfig> HttpStatusCodes { get; } = CreateStatusCodesConfig(configuration);

    private static IReadOnlyCollection<IHttpStatusCodeConfig> CreateStatusCodesConfig(IConfiguration configuration)
    {
        var section = configuration.GetSection("HttpStatusCodes");
        if (!section.Exists())
        {
            return [];
        }

        var statusCodeSettings = section.Get<StatusCodeSetting[]>();

        if (statusCodeSettings is null || statusCodeSettings.Length == 0)
        {
            return [];
        }

        return statusCodeSettings
            .Select(statusCodeSetting => new HttpStatusCodeConfig
            {
                Code = statusCodeSetting.Code,
                ReasonPhrase = statusCodeSetting.ReasonPhrase ?? string.Empty
            })
            .ToList();
    }
}