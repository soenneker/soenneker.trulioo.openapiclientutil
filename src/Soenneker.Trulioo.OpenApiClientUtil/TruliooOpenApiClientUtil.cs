using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.ValueTask;
using Soenneker.Trulioo.HttpClients.Abstract;
using Soenneker.Trulioo.OpenApiClientUtil.Abstract;
using Soenneker.Trulioo.OpenApiClient;
using Soenneker.Kiota.GenericAuthenticationProvider;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Trulioo.OpenApiClientUtil;

///<inheritdoc cref="ITruliooOpenApiClientUtil"/>
public sealed class TruliooOpenApiClientUtil : ITruliooOpenApiClientUtil
{
    private readonly AsyncSingleton<TruliooOpenApiClient> _client;

    public TruliooOpenApiClientUtil(ITruliooOpenApiHttpClient httpClientUtil, IConfiguration configuration)
    {
        _client = new AsyncSingleton<TruliooOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var apiKey = configuration.GetValueStrict<string>("Trulioo:ApiKey");
            string authHeaderValueTemplate = configuration["Trulioo:AuthHeaderValueTemplate"] ?? "Bearer {token}";
            string authHeaderValue = authHeaderValueTemplate.Replace("{token}", apiKey, StringComparison.Ordinal);

            var requestAdapter = new HttpClientRequestAdapter(new GenericAuthenticationProvider(headerValue: authHeaderValue), httpClient: httpClient);

            return new TruliooOpenApiClient(requestAdapter);
        });
    }

    public ValueTask<TruliooOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}
