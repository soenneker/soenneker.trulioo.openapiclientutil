using Soenneker.Trulioo.OpenApiClientUtil.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Trulioo.OpenApiClientUtil.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class TruliooOpenApiClientUtilTests : HostedUnitTest
{
    private readonly ITruliooOpenApiClientUtil _openapiclientutil;

    public TruliooOpenApiClientUtilTests(Host host) : base(host)
    {
        _openapiclientutil = Resolve<ITruliooOpenApiClientUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
