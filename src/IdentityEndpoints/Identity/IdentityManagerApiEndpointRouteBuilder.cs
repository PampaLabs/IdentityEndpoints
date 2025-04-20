namespace Microsoft.AspNetCore.Routing;

/// <summary>
/// Provides methods to build API endpoint routes related to identity management for users.
/// </summary>
/// <typeparam name="TUser">The type representing a user in the identity system.</typeparam>
public partial class IdentityManagerApiEndpointRouteBuilder<TUser>
    where TUser : class, new()
{
    /// <summary>
    /// The underlying endpoint route builder used to register API routes.
    /// </summary>
    protected readonly IEndpointRouteBuilder _endpoints;

    /// <summary>
    /// Initializes a new instance of the <see cref="IdentityManagerApiEndpointRouteBuilder{TUser}"/> class.
    /// </summary>
    /// <param name="endpoints">The route builder used to configure endpoint routes.</param>
    public IdentityManagerApiEndpointRouteBuilder(IEndpointRouteBuilder endpoints)
    {
        _endpoints = endpoints;
    }
}

/// <summary>
/// Provides methods to build API endpoint routes related to identity management for both users and roles.
/// </summary>
/// <typeparam name="TUser">The type representing a user in the identity system.</typeparam>
/// <typeparam name="TRole">The type representing a role in the identity system.</typeparam>
public partial class IdentityManagerApiEndpointRouteBuilder<TUser, TRole> : IdentityManagerApiEndpointRouteBuilder<TUser>
    where TUser : class, new()
    where TRole : class, new()
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IdentityManagerApiEndpointRouteBuilder{TUser, TRole}"/> class.
    /// </summary>
    /// <param name="endpoints">The route builder used to configure endpoint routes.</param>
    public IdentityManagerApiEndpointRouteBuilder(IEndpointRouteBuilder endpoints) : base(endpoints)
    {
    }
}
