using Soenneker.Trulioo.OpenApiClientUtil.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Trulioo.OpenApiClientUtil.Tests;

[Collection("Collection")]
public sealed class TruliooOpenApiClientUtilTests : FixturedUnitTest
{
    private readonly ITruliooOpenApiClientUtil _openapiclientutil;

    public TruliooOpenApiClientUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _openapiclientutil = Resolve<ITruliooOpenApiClientUtil>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
