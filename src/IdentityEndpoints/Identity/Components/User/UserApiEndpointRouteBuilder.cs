using System.Security.Claims;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

using IdentityEndpoints;
using IdentityEndpoints.Components.Shared.Data;

namespace Microsoft.AspNetCore.Routing;

using static EndpointRouteBuilderHelper;

/// <summary>
/// Defines the route mappings for API endpoints related to user management in the identity system.
/// </summary>
/// <typeparam name="TUser">The type representing a user in the identity system.</typeparam>
/// <typeparam name="TUserRequest">The type of the request data used to create or update a user.</typeparam>
/// <typeparam name="TUserResponse">The type of the response data used to represent a user.</typeparam>
public class UserApiEndpointRouteBuilder<TUser, TUserRequest, TUserResponse>
    where TUser : class, new()
    where TUserRequest : class, new()
    where TUserResponse : class, new()
{
    private readonly IEndpointRouteBuilder routeGroup;

    private readonly IEndpointDataMapper<TUser, TUserRequest, TUserResponse> mapper;

    /// <summary>
    /// Initializes a new instance of <see cref="UserApiEndpointRouteBuilder{TUser, TUserRequest, TUserResponse}"/>.
    /// </summary>
    /// <param name="routeGroup">The endpoint route builder used to map the routes.</param>
    /// <param name="mapper">The data mapper used to transform between request/response models and user entities.</param>
    public UserApiEndpointRouteBuilder(
        IEndpointRouteBuilder routeGroup,
        IEndpointDataMapper<TUser, TUserRequest, TUserResponse> mapper)
    {
        this.routeGroup = routeGroup;
        this.mapper = mapper;
    }

    /// <summary>
    /// Maps the endpoint to list users with pagination.
    /// </summary>
    /// <returns>A <see cref="RouteHandlerBuilder"/> that can be used to further customize the endpoint.</returns>
    public RouteHandlerBuilder MapListEndpoint()
    {
        return routeGroup.MapGet("", async Task<Results<Ok<IAsyncEnumerable<TUserResponse>>, ValidationProblem>>
            ([FromServices] IServiceProvider sp, [FromQuery] int pageSize = 20, [FromQuery] int pageCount = 0) =>
        {
            var userManager = sp.GetRequiredService<UserManager<TUser>>();

            var users = userManager.Users.Skip(pageCount * pageSize).Take(pageSize).ToAsyncEnumerable();

            var response = users.Select(user => mapper.ToResponse(user));

            return TypedResults.Ok(response);
        });
    }

    /// <summary>
    /// Maps the endpoint to create a new user.
    /// </summary>
    /// <returns>A <see cref="RouteHandlerBuilder"/> that can be used to further customize the endpoint.</returns>
    public RouteHandlerBuilder MapCreateEndpoint()
    {
        return routeGroup.MapPost("", async Task<Results<Ok<TUserResponse>, ValidationProblem>>
            ([FromServices] IServiceProvider sp, [FromBody] TUserRequest data) =>
        {
            var userManager = sp.GetRequiredService<UserManager<TUser>>();

            var user = mapper.FromRequest(data);

            var result = await userManager.CreateAsync(user);

            if (!result.Succeeded)
            {
                return CreateValidationProblem(result);
            }

            var response = mapper.ToResponse(user);

            return TypedResults.Ok(response);
        });
    }

    /// <summary>
    /// Maps the endpoint to get details of a specific user by user ID.
    /// </summary>
    /// <returns>A <see cref="RouteHandlerBuilder"/> that can be used to further customize the endpoint.</returns>
    public RouteHandlerBuilder MapReadEndpoint()
    {
        return routeGroup.MapGet("/{userId}", async Task<Results<Ok<TUserResponse>, ValidationProblem, NotFound>>
            ([FromServices] IServiceProvider sp, [FromRoute] string userId) =>
        {
            var userManager = sp.GetRequiredService<UserManager<TUser>>();

            var user = await userManager.FindByIdAsync(userId);

            if (user is null)
            {
                return TypedResults.NotFound();
            }

            var response = mapper.ToResponse(user);

            return TypedResults.Ok(response);
        });
    }

    /// <summary>
    /// Maps the endpoint to update an existing user's data.
    /// </summary>
    /// <returns>A <see cref="RouteHandlerBuilder"/> that can be used to further customize the endpoint.</returns>
    public RouteHandlerBuilder MapUpdateEndpoint()
    {
        return routeGroup.MapPut("/{userId}", async Task<Results<Ok, ValidationProblem, NotFound>>
            ([FromServices] IServiceProvider sp, [FromRoute] string userId, [FromBody] TUserRequest data) =>
        {
            var userManager = sp.GetRequiredService<UserManager<TUser>>();

            var user = await userManager.FindByIdAsync(userId);

            if (user is null)
            {
                return TypedResults.NotFound();
            }

            mapper.FromRequest(data, user);

            var result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return CreateValidationProblem(result);
            }

            return TypedResults.Ok();
        });
    }

    /// <summary>
    /// Maps the endpoint to delete a user by their user ID.
    /// </summary>
    /// <returns>A <see cref="RouteHandlerBuilder"/> that can be used to further customize the endpoint.</returns>
    public RouteHandlerBuilder MapDeleteEndpoint()
    {
        return routeGroup.MapDelete("/{userId}", async Task<Results<Ok, ValidationProblem, NotFound>>
            ([FromServices] IServiceProvider sp, [FromRoute] string userId) =>
        {
            var userManager = sp.GetRequiredService<UserManager<TUser>>();

            var user = await userManager.FindByIdAsync(userId);

            if (user is null)
            {
                return TypedResults.NotFound();
            }

            var result = await userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                return CreateValidationProblem(result);
            }

            return TypedResults.Ok();
        });
    }

    /// <summary>
    /// Maps the endpoint to get the roles associated with a user.
    /// </summary>
    /// <returns>A <see cref="RouteHandlerBuilder"/> that can be used to further customize the endpoint.</returns>
    public RouteHandlerBuilder MapGetRolesEndpoint()
    {
        return routeGroup.MapGet("/{userId}/roles", async Task<Results<Ok<string[]>, ValidationProblem, NotFound>>
            ([FromServices] IServiceProvider sp, [FromRoute] string userId) =>
        {
            var userManager = sp.GetRequiredService<UserManager<TUser>>();

            var user = await userManager.FindByIdAsync(userId);

            if (user is null)
            {
                return TypedResults.NotFound();
            }

            var roles = await userManager.GetRolesAsync(user);

            return TypedResults.Ok(roles.ToArray());
        });
    }

    /// <summary>
    /// Maps the endpoint to assign roles to a user.
    /// </summary>
    /// <returns>A <see cref="RouteHandlerBuilder"/> that can be used to further customize the endpoint.</returns>
    public RouteHandlerBuilder MapAddRoleEndpoint()
    {
        return routeGroup.MapPost("/{userId}/roles", async Task<Results<Ok, ValidationProblem, NotFound>>
            ([FromServices] IServiceProvider sp, [FromRoute] string userId, [FromBody] string[] roles) =>
        {
            var userManager = sp.GetRequiredService<UserManager<TUser>>();

            var user = await userManager.FindByIdAsync(userId);

            if (user is null)
            {
                return TypedResults.NotFound();
            }

            var result = await userManager.AddToRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                return CreateValidationProblem(result);
            }

            return TypedResults.Ok();
        });
    }

    /// <summary>
    /// Maps the endpoint to remove roles from a user.
    /// </summary>
    /// <returns>A <see cref="RouteHandlerBuilder"/> that can be used to further customize the endpoint.</returns>
    public RouteHandlerBuilder MapRemoveRoleEndpoint()
    {
        return routeGroup.MapDelete("/{userId}/roles", async Task<Results<Ok, ValidationProblem, NotFound>>
            ([FromServices] IServiceProvider sp, [FromRoute] string userId, [FromBody] string[] roles) =>
        {
            var userManager = sp.GetRequiredService<UserManager<TUser>>();

            var user = await userManager.FindByIdAsync(userId);

            if (user is null)
            {
                return TypedResults.NotFound();
            }

            var result = await userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                return CreateValidationProblem(result);
            }

            return TypedResults.Ok();
        });
    }

    /// <summary>
    /// Maps the endpoint to get the claims associated with a user.
    /// </summary>
    /// <returns>A <see cref="RouteHandlerBuilder"/> that can be used to further customize the endpoint.</returns>
    public RouteHandlerBuilder MapGetClaimsEndpoint()
    {
        return routeGroup.MapGet("/{userId}/claims", async Task<Results<Ok<ClaimResponse[]>, ValidationProblem, NotFound>>
            ([FromServices] IServiceProvider sp, [FromRoute] string userId) =>
        {
            var userManager = sp.GetRequiredService<UserManager<TUser>>();

            var user = await userManager.FindByIdAsync(userId);

            if (user is null)
            {
                return TypedResults.NotFound();
            }

            var claims = await userManager.GetClaimsAsync(user);

            var response = claims.Select(claim => new ClaimResponse
            {
                Type = claim.Type!,
                Value = claim.Value!
            })
            .ToArray();

            return TypedResults.Ok(response);
        });
    }

    /// <summary>
    /// Maps the endpoint to add claims to a user.
    /// </summary>
    /// <returns>A <see cref="RouteHandlerBuilder"/> that can be used to further customize the endpoint.</returns>
    public RouteHandlerBuilder MapAddClaimEndpoint()
    {
        return routeGroup.MapPost("/{userId}/claims", async Task<Results<Ok, ValidationProblem, NotFound>>
            ([FromServices] IServiceProvider sp, [FromRoute] string userId, [FromBody] ClaimRequest[] data) =>
        {
            var userManager = sp.GetRequiredService<UserManager<TUser>>();

            var user = await userManager.FindByIdAsync(userId);

            if (user is null)
            {
                return TypedResults.NotFound();
            }

            var claims = data.Select(item => new Claim(item.Type!, item.Value!));

            var result = await userManager.AddClaimsAsync(user, claims);

            if (!result.Succeeded)
            {
                return CreateValidationProblem(result);
            }

            return TypedResults.Ok();
        });
    }

    /// <summary>
    /// Maps the endpoint to remove claims from a user.
    /// </summary>
    /// <returns>A <see cref="RouteHandlerBuilder"/> that can be used to further customize the endpoint.</returns>
    public RouteHandlerBuilder MapRemoveClaimEndpoint()
    {
        return routeGroup.MapDelete("/{userId}/claims", async Task<Results<Ok, ValidationProblem, NotFound>>
            ([FromServices] IServiceProvider sp, [FromRoute] string userId, [FromBody] ClaimRequest[] data) =>
        {
            var userManager = sp.GetRequiredService<UserManager<TUser>>();

            var user = await userManager.FindByIdAsync(userId);

            if (user is null)
            {
                return TypedResults.NotFound();
            }

            var claims = data.Select(item => new Claim(item.Type!, item.Value!));

            var result = await userManager.RemoveClaimsAsync(user, claims);

            if (!result.Succeeded)
            {
                return CreateValidationProblem(result);
            }

            return TypedResults.Ok();
        });
    }
}
