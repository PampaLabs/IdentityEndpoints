using IdentityEndpoints.Components.User;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;

namespace Microsoft.AspNetCore.Routing;

/// <summary>
/// Provides extension methods for <see cref="IdentityManagerApiEndpointRouteBuilder{TUser}"/>.
/// </summary>
public static partial class IdentityManagerApiEndpointRouteBuilderExtensions
{
    /// <summary>
    /// Maps user management endpoints with the default configuration, using the <see cref="IdentityUser"/> data mapper.
    /// </summary>
    /// <typeparam name="TUser">The type representing the identity user.</typeparam>
    /// <param name="builder">The route builder used to map the user endpoints.</param>
    /// <returns>An <see cref="IEndpointConventionBuilder"/> for further endpoint customization.</returns>
    public static IEndpointConventionBuilder MapUserEndpoints<TUser>(
        this IdentityManagerApiEndpointRouteBuilder<TUser> builder
    )
        where TUser : IdentityUser, new()
    {
        return builder.MapUserEndpoints(new IdentityUserDataMapper());
    }

    /// <summary>
    /// Maps user management endpoints with a custom configuration, using the <see cref="IdentityUser"/> data mapper.
    /// </summary>
    /// <typeparam name="TUser">The type representing the identity user.</typeparam>
    /// <param name="builder">The route builder used to map the user endpoints.</param>
    /// <param name="optionsBuilder">A delegate to configure which endpoints to include.</param>
    /// <returns>An <see cref="IEndpointConventionBuilder"/> for further endpoint customization.</returns>
    public static IEndpointConventionBuilder MapUserEndpoints<TUser>(
        this IdentityManagerApiEndpointRouteBuilder<TUser> builder,
        Action<UserApiEndpointRouteBuilder<TUser, IdentityUserRequest, IdentityUserResponse>> optionsBuilder
    )
        where TUser : IdentityUser, new()
    {
        return builder.MapUserEndpoints(new IdentityUserDataMapper(), optionsBuilder);
    }
}

