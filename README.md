[![](https://img.shields.io/nuget/v/soenneker.trulioo.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.trulioo.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.trulioo.openapiclientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.trulioo.openapiclientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.trulioo.openapiclientutil/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.trulioo.openapiclientutil/actions/workflows/codeql.yml)
[![](https://img.shields.io/nuget/dt/soenneker.trulioo.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.trulioo.openapiclientutil/)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Trulioo.OpenApiClientUtil
Provides lazily initialized, cached access to Trulioo's generated Customer API v2.5 client.

## Installation

```bash
dotnet add package Soenneker.Trulioo.OpenApiClientUtil
```

## Configuration

```json
{
  "Trulioo": {
    "ApiKey": "your-license-or-access-token"
  }
}
```

The configured bearer credential is attached by the underlying HTTP provider. Use a client configured with the license for authorization, then a client configured with the returned short-lived access token for customer and transaction calls.

## Registration

```csharp
using Soenneker.Trulioo.OpenApiClientUtil.Registrars;

services.AddTruliooOpenApiClientUtilAsScoped();
```

The scoped utility can be destroyed with its scope while the singleton HTTP provider remains available. Use `AddTruliooOpenApiClientUtilAsSingleton()` only when one generated-client wrapper and credential are intentionally application-wide.

## Usage

```csharp
using Soenneker.Trulioo.OpenApiClient;
using Soenneker.Trulioo.OpenApiClient.Models;
using Soenneker.Trulioo.OpenApiClientUtil.Abstract;

TruliooOpenApiClient client = await truliooClients.Get(cancellationToken);
TransactionResultResponse? transaction =
    await client.Customer.Transactions[transactionId]
        .GetAsync(cancellationToken: cancellationToken);
```

Do not dispose the generated client returned by `Get()`. The utility owns its wrapper and the registered HTTP provider owns the underlying `HttpClient`. Treat credentials, images, and identity payloads as sensitive data and keep them out of logs.
