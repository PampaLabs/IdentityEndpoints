using IdentityEndpoints.Components.Role;
using IdentityEndpoints.Components.User;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;

namespace Microsoft.AspNetCore.Routing;

/// <summary>
/// Provides extension methods for <see cref="IEndpointRouteBuilder"/>.
/// </summary>
public static class EndpointRouteBuilderExtensions
{
    /// <summary>
    /// Maps the identity manager API routes for a specific <see cref="IdentityUser"/> type.
    /// </summary>
    /// <typeparam name="TUser">The type representing a user in the identity system.</typeparam>
    /// <param name="endpoints">The endpoint route builder to map the routes to.</param>
    /// <returns>An endpoint convention builder to configure the mapped routes.</returns>
    public static IEndpointConventionBuilder MapIdentityManagerApi<TUser>(this IEndpointRouteBuilder endpoints)
        where TUser : IdentityUser, new()
    {
        return MapIdentityManagerApi<TUser>(endpoints, options =>
        {
            options.MapUserEndpoints();
        });
    }

    /// <summary>
    /// Maps the identity manager API routes for a specific <see cref="IdentityUser"/> type with custom options.
    /// </summary>
    /// <typeparam name="TUser">The type representing a user in the identity system.</typeparam>
    /// <param name="endpoints">The endpoint route builder to map the routes to.</param>
    /// <param name="optionsBuilder">A delegate used to configure the individual user-related routes.</param>
    /// <returns>An endpoint convention builder to configure the mapped routes.</returns>
    public static IEndpointConventionBuilder MapIdentityManagerApi<TUser>(this IEndpointRouteBuilder endpoints, Action<IdentityManagerApiEndpointRouteBuilder<TUser>> optionsBuilder)
        where TUser : class, new()
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        var routeGroup = endpoints.MapGroup("").RequireAuthorization();

        var builder = new IdentityManagerApiEndpointRouteBuilder<TUser>(routeGroup);
        optionsBuilder.Invoke(builder);

        return routeGroup;
    }

    /// <summary>
    /// Maps the identity manager API routes for a specific <see cref="IdentityUser"/> and <see cref="IdentityRole"/> type.
    /// </summary>
    /// <typeparam name="TUser">The type representing a user in the identity system.</typeparam>
    /// <typeparam name="TRole">The type representing a role in the identity system.</typeparam>
    /// <param name="endpoints">The endpoint route builder to map the routes to.</param>
    /// <returns>An endpoint convention builder to configure the mapped routes.</returns>
    public static IEndpointConventionBuilder MapIdentityManagerApi<TUser, TRole>(this IEndpointRouteBuilder endpoints)
        where TUser : IdentityUser, new()
        where TRole : IdentityRole, new()
    {
        return MapIdentityManagerApi<TUser, TRole>(endpoints, options =>
        {
            options.MapUserEndpoints();
            options.MapRoleEndpoints();
        });
    }

    /// <summary>
    /// Maps the identity manager API routes for a specific <see cref="IdentityUser"/> and <see cref="IdentityRole"/> type with custom options.
    /// </summary>
    /// <typeparam name="TUser">The type representing a user in the identity system.</typeparam>
    /// <typeparam name="TRole">The type representing a role in the identity system.</typeparam>
    /// <param name="endpoints">The endpoint route builder to map the routes to.</param>
    /// <param name="optionsBuilder">A delegate used to configure the individual user and role-related routes.</param>
    /// <returns>An endpoint convention builder to configure the mapped routes.</returns>
    public static IEndpointConventionBuilder MapIdentityManagerApi<TUser, TRole>(this IEndpointRouteBuilder endpoints, Action<IdentityManagerApiEndpointRouteBuilder<TUser, TRole>> optionsBuilder)
        where TUser : class, new()
        where TRole : class, new()
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        var routeGroup = endpoints.MapGroup("").RequireAuthorization();

        var builder = new IdentityManagerApiEndpointRouteBuilder<TUser, TRole>(routeGroup);
        optionsBuilder.Invoke(builder);

        return routeGroup;
    }
}
