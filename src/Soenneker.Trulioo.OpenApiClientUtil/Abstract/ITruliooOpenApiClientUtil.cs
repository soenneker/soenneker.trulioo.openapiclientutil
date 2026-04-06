using Soenneker.Trulioo.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Trulioo.OpenApiClientUtil.Abstract;

/// <summary>
/// Exposes a cached OpenAPI client instance.
/// </summary>
public interface ITruliooOpenApiClientUtil: IDisposable, IAsyncDisposable
{
    ValueTask<TruliooOpenApiClient> Get(CancellationToken cancellationToken = default);
}
