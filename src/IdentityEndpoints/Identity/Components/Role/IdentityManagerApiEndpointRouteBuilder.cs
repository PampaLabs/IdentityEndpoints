using IdentityEndpoints;

using Microsoft.AspNetCore.Builder;

namespace Microsoft.AspNetCore.Routing;

public partial class IdentityManagerApiEndpointRouteBuilder<TUser, TRole>
{
    /// <summary>
    /// Maps role management endpoints with the default configuration.
    /// </summary>
    /// <typeparam name="TRoleRequest">The request type used to create or update a role.</typeparam>
    /// <typeparam name="TRoleResponse">The response type returned by the endpoints.</typeparam>
    /// <param name="mapper">The mapper responsible for converting between entity and DTO types.</param>
    /// <returns>An <see cref="IEndpointConventionBuilder"/> for further endpoint customization.</returns>
    public IEndpointConventionBuilder MapRoleEndpoints<TRoleRequest, TRoleResponse>(
        IEndpointDataMapper<TRole, TRoleRequest, TRoleResponse> mapper
    )
        where TRoleRequest : class, new()
        where TRoleResponse : class, new()
    {
        return MapRoleEndpoints(mapper, options =>
        {
            options.MapCreateEndpoint();
            options.MapReadEndpoint();
            options.MapUpdateEndpoint();
            options.MapDeleteEndpoint();
            options.MapListEndpoint();

            options.MapGetClaimsEndpoint();
            options.MapAddClaimEndpoint();
            options.MapRemoveClaimEndpoint();
        });
    }

    /// <summary>
    /// Maps role management endpoints with a custom configuration.
    /// </summary>
    /// <typeparam name="TRoleRequest">The request type used to create or update a role.</typeparam>
    /// <typeparam name="TRoleResponse">The response type returned by the endpoints.</typeparam>
    /// <param name="mapper">The mapper responsible for converting between entity and DTO types.</param>
    /// <param name="optionsBuilder">A delegate to configure which endpoints to include.</param>
    /// <returns>An <see cref="IEndpointConventionBuilder"/> for further endpoint customization.</returns>
    public IEndpointConventionBuilder MapRoleEndpoints<TRoleRequest, TRoleResponse>(
        IEndpointDataMapper<TRole, TRoleRequest, TRoleResponse> mapper,
        Action<RoleApiEndpointRouteBuilder<TRole, TRoleRequest, TRoleResponse>> optionsBuilder
    )
        where TRoleRequest : class, new()
        where TRoleResponse : class, new()
    {
        var routeGroup = _endpoints.MapGroup("/roles");

        var builder = new RoleApiEndpointRouteBuilder<TRole, TRoleRequest, TRoleResponse>(routeGroup, mapper);
        optionsBuilder?.Invoke(builder);

        return routeGroup;
    }
}
