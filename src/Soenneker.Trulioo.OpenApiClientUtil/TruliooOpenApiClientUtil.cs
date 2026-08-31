using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.ValueTask;
using Soenneker.Trulioo.HttpClients.Abstract;
using Soenneker.Trulioo.OpenApiClientUtil.Abstract;
using Soenneker.Trulioo.OpenApiClient;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Trulioo.OpenApiClientUtil;

public sealed class TruliooOpenApiClientUtil : ITruliooOpenApiClientUtil
{
    private readonly AsyncSingleton<TruliooOpenApiClient> _client;

    public TruliooOpenApiClientUtil(ITruliooOpenApiHttpClient httpClientUtil)
    {
        _client = new AsyncSingleton<TruliooOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var requestAdapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider(), httpClient: httpClient);

            return new TruliooOpenApiClient(requestAdapter);
        });
    }

    public ValueTask<TruliooOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    /// <summary>
    /// Releases resources used by the current instance.
    /// </summary>
    public void Dispose()
    {
        _client.Dispose();
    }

    /// <summary>
    /// Asynchronously releases resources used by the current instance.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}
