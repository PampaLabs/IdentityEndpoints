using IdentityEndpoints;

using Microsoft.AspNetCore.Builder;

namespace Microsoft.AspNetCore.Routing;

public partial class IdentityManagerApiEndpointRouteBuilder<TUser>
{
    /// <summary>
    /// Maps user management endpoints with the default configuration.
    /// </summary>
    /// <typeparam name="TUserRequest">The request type used to create or update a user.</typeparam>
    /// <typeparam name="TUserResponse">The response type returned by the endpoints.</typeparam>
    /// <param name="mapper">The mapper responsible for converting between entity and DTO types.</param>
    /// <returns>An <see cref="IEndpointConventionBuilder"/> for further endpoint customization.</returns>
    public IEndpointConventionBuilder MapUserEndpoints<TUserRequest, TUserResponse>(
        IEndpointDataMapper<TUser, TUserRequest, TUserResponse> mapper
    )
        where TUserRequest : class, new()
        where TUserResponse : class, new()
    {
        return MapUserEndpoints(mapper, options =>
        {
            options.MapCreateEndpoint();
            options.MapReadEndpoint();
            options.MapUpdateEndpoint();
            options.MapDeleteEndpoint();
            options.MapListEndpoint();

            options.MapGetRolesEndpoint();
            options.MapAddRoleEndpoint();
            options.MapRemoveRoleEndpoint();

            options.MapGetClaimsEndpoint();
            options.MapAddClaimEndpoint();
            options.MapRemoveClaimEndpoint();
        });
    }

    /// <summary>
    /// Maps user management endpoints with a custom configuration.
    /// </summary>
    /// <typeparam name="TUserRequest">The request type used for user input.</typeparam>
    /// <typeparam name="TUserResponse">The response type returned from the endpoints.</typeparam>
    /// <param name="mapper">The data mapper used to convert between user entities and DTOs.</param>
    /// <param name="optionsBuilder">A delegate to configure which endpoints to include.</param>
    /// <returns>An object to further configure the mapped endpoints.</returns>
    public IEndpointConventionBuilder MapUserEndpoints<TUserRequest, TUserResponse>(
        IEndpointDataMapper<TUser, TUserRequest, TUserResponse> mapper,
        Action<UserApiEndpointRouteBuilder<TUser, TUserRequest, TUserResponse>> optionsBuilder
    )
        where TUserRequest : class, new()
        where TUserResponse : class, new()
    {
        var routeGroup = _endpoints.MapGroup("/users");

        var builder = new UserApiEndpointRouteBuilder<TUser, TUserRequest, TUserResponse>(routeGroup, mapper);
        optionsBuilder?.Invoke(builder);

        return routeGroup;
    }
}
