using IdentityEndpoints.Components.Role;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;

namespace Microsoft.AspNetCore.Routing;

/// <summary>
/// Provides extension methods for <see cref="IdentityManagerApiEndpointRouteBuilder{TUser,TRole}"/>.
/// </summary>
public static partial class IdentityManagerApiEndpointRouteBuilderExtensions
{
    /// <summary>
    /// Maps role management endpoints with the default configuration, using the <see cref="IdentityRole"/> data mapper.
    /// </summary>
    /// <typeparam name="TUser">The type representing the identity user.</typeparam>
    /// <typeparam name="TRole">The type representing the identity role.</typeparam>
    /// <param name="builder">The route builder used to map the role endpoints.</param>
    /// <returns>An <see cref="IEndpointConventionBuilder"/> for further endpoint customization.</returns>
    public static IEndpointConventionBuilder MapRoleEndpoints<TUser, TRole>(
        this IdentityManagerApiEndpointRouteBuilder<TUser, TRole> builder
    )
        where TUser : IdentityUser, new()
        where TRole : IdentityRole, new()
    {
        return builder.MapRoleEndpoints(new IdentityRoleDataMapper());
    }

    /// <summary>
    /// Maps role management endpoints with the default configuration, using the <see cref="IdentityRole"/> data mapper.
    /// </summary>
    /// <typeparam name="TUser">The type representing the identity user.</typeparam>
    /// <typeparam name="TRole">The type representing the identity role.</typeparam>
    /// <param name="builder">The route builder used to map the role endpoints.</param>
    /// <param name="optionsBuilder">A delegate to configure which endpoints to include.</param>
    /// <returns>An <see cref="IEndpointConventionBuilder"/> for further endpoint customization.</returns>
    public static IEndpointConventionBuilder MapRoleEndpoints<TUser, TRole>(
        this IdentityManagerApiEndpointRouteBuilder<TUser, TRole> builder,
        Action<RoleApiEndpointRouteBuilder<TRole, IdentityRoleRequest, IdentityRoleResponse>> optionsBuilder
    )
        where TUser : IdentityUser, new()
        where TRole : IdentityRole, new()
    {
        return builder.MapRoleEndpoints(new IdentityRoleDataMapper(), optionsBuilder);
    }
}
