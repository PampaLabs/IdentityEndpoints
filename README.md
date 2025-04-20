# IdentityEndpoints

This package enabled identity manager endpoints in applications using `ASP.NET Core Identity`.

## Installation

To use this extension with `ASP.NET Core`, you will first need to install the package.

```
dotnet add package IdentityEndpoints
```

## Usage

Configure the endpoints for manage users and roles by chaining the methods to the `MapIdentityManagerApi`.

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityCore<ApplicationUser>();

var app = builder.Build();

app.MapGroup("account").MapIdentityApi<ApplicationUser>();
app.MapGroup("iam").MapIdentityManagerApi<ApplicationUser>();

app.Run();
```

## Contributing

Contributions are welcome! Please open an issue or submit a pull request on GitHub.
