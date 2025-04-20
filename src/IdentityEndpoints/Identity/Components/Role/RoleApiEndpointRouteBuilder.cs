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
/// Defines the route mappings for API endpoints related to role management in the identity system.
/// </summary>
/// <typeparam name="TRole">The type representing a role in the identity system.</typeparam>
/// <typeparam name="TRoleRequest">The type of the request data used to create or update a role.</typeparam>
/// <typeparam name="TRoleResponse">The type of the response data used to represent a role.</typeparam>
public class RoleApiEndpointRouteBuilder<TRole, TRoleRequest, TRoleResponse>
    where TRole : class, new()
    where TRoleRequest : class, new()
    where TRoleResponse : class, new()
{
    private readonly IEndpointRouteBuilder routeGroup;

    private readonly IEndpointDataMapper<TRole, TRoleRequest, TRoleResponse> mapper;

    /// <summary>
    /// Initializes a new instance of <see cref="RoleApiEndpointRouteBuilder{TRole, TRoleRequest, TRoleResponse}"/>.
    /// </summary>
    /// <param name="routeGroup">The endpoint route builder used to map the routes.</param>
    /// <param name="mapper">The data mapper used to transform between request/response models and role entities.</param>
    public RoleApiEndpointRouteBuilder(
        IEndpointRouteBuilder routeGroup,
        IEndpointDataMapper<TRole, TRoleRequest, TRoleResponse> mapper)
    {
        this.routeGroup = routeGroup;
        this.mapper = mapper;
    }

    /// <summary>
    /// Maps the endpoint to get a list of roles with pagination.
    /// </summary>
    /// <returns>A <see cref="RouteHandlerBuilder"/> that can be used to further customize the endpoint.</returns>
    public RouteHandlerBuilder MapListEndpoint()
    {
        return routeGroup.MapGet("", async Task<Results<Ok<IAsyncEnumerable<TRoleResponse>>, ValidationProblem>>
            ([FromServices] IServiceProvider sp, [FromQuery] int pageSize = 20, [FromQuery] int pageCount = 0) =>
        {
            var roleManager = sp.GetRequiredService<RoleManager<TRole>>();

            var roles = roleManager.Roles.Skip(pageCount * pageSize).Take(pageSize).ToAsyncEnumerable();

            var response = roles.Select(role => mapper.ToResponse(role));

            return TypedResults.Ok(response);
        });
    }

    /// <summary>
    /// Maps the endpoint to create a new role.
    /// </summary>
    /// <returns>A <see cref="RouteHandlerBuilder"/> that can be used to further customize the endpoint.</returns>
    public RouteHandlerBuilder MapCreateEndpoint()
    {
        return routeGroup.MapPost("", async Task<Results<Ok<TRoleResponse>, ValidationProblem>>
            ([FromServices] IServiceProvider sp, [FromBody] TRoleRequest data) =>
        {
            var roleManager = sp.GetRequiredService<RoleManager<TRole>>();

            var role = mapper.FromRequest(data);

            var result = await roleManager.CreateAsync(role);

            if (!result.Succeeded)
            {
                return CreateValidationProblem(result);
            }

            var response = mapper.ToResponse(role);

            return TypedResults.Ok(response);
        });
    }

    /// <summary>
    /// Maps the endpoint to get a specific role by its ID.
    /// </summary>
    /// <returns>A <see cref="RouteHandlerBuilder"/> that can be used to further customize the endpoint.</returns>
    public RouteHandlerBuilder MapReadEndpoint()
    {
        return routeGroup.MapGet("/{roleId}", async Task<Results<Ok<TRoleResponse>, ValidationProblem, NotFound>>
            ([FromServices] IServiceProvider sp, [FromRoute] string roleId) =>
        {
            var roleManager = sp.GetRequiredService<RoleManager<TRole>>();

            var role = await roleManager.FindByIdAsync(roleId);

            if (role is null)
            {
                return TypedResults.NotFound();
            }

            var response = mapper.ToResponse(role);

            return TypedResults.Ok(response);
        });
    }

    /// <summary>
    /// Maps the endpoint to update an existing role.
    /// </summary>
    /// <returns>A <see cref="RouteHandlerBuilder"/> that can be used to further customize the endpoint.</returns>
    public RouteHandlerBuilder MapUpdateEndpoint()
    {
        return routeGroup.MapPut("/{roleId}", async Task<Results<Ok, ValidationProblem, NotFound>>
            ([FromServices] IServiceProvider sp, [FromRoute] string roleId, [FromBody] TRoleRequest data) =>
        {
            var roleManager = sp.GetRequiredService<RoleManager<TRole>>();

            var role = await roleManager.FindByIdAsync(roleId);

            if (role is null)
            {
                return TypedResults.NotFound();
            }

            mapper.FromRequest(data, role);

            var result = await roleManager.UpdateAsync(role);

            if (!result.Succeeded)
            {
                return CreateValidationProblem(result);
            }

            return TypedResults.Ok();
        });
    }

    /// <summary>
    /// Maps the endpoint to delete a role.
    /// </summary>
    /// <returns>A <see cref="RouteHandlerBuilder"/> that can be used to further customize the endpoint.</returns>
    public RouteHandlerBuilder MapDeleteEndpoint()
    {
        return routeGroup.MapDelete("/{roleId}", async Task<Results<Ok, ValidationProblem, NotFound>>
            ([FromServices] IServiceProvider sp, [FromRoute] string roleId) =>
        {
            var roleManager = sp.GetRequiredService<RoleManager<TRole>>();

            var role = await roleManager.FindByIdAsync(roleId);

            if (role is null)
            {
                return TypedResults.NotFound();
            }

            var result = await roleManager.DeleteAsync(role);

            if (!result.Succeeded)
            {
                return CreateValidationProblem(result);
            }

            return TypedResults.Ok();
        });
    }

    /// <summary>
    /// Maps the endpoint to get the claims associated with a role.
    /// </summary>
    /// <returns>A <see cref="RouteHandlerBuilder"/> that can be used to further customize the endpoint.</returns>
    public RouteHandlerBuilder MapGetClaimsEndpoint()
    {
        return routeGroup.MapGet("/{roleId}/claims", async Task<Results<Ok<ClaimResponse[]>, ValidationProblem, NotFound>>
            ([FromServices] IServiceProvider sp, [FromRoute] string roleId) =>
        {
            var roleManager = sp.GetRequiredService<RoleManager<TRole>>();

            var role = await roleManager.FindByIdAsync(roleId);

            if (role is null)
            {
                return TypedResults.NotFound();
            }

            var claims = await roleManager.GetClaimsAsync(role);

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
    /// Maps the endpoint to add claims to a role.
    /// </summary>
    /// <returns>A <see cref="RouteHandlerBuilder"/> that can be used to further customize the endpoint.</returns>
    public RouteHandlerBuilder MapAddClaimEndpoint()
    {
        return routeGroup.MapPost("/{roleId}/claims", async Task<Results<Ok, ValidationProblem, NotFound>>
            ([FromServices] IServiceProvider sp, [FromRoute] string roleId, [FromBody] ClaimRequest[] data) =>
        {
            var roleManager = sp.GetRequiredService<RoleManager<TRole>>();

            var role = await roleManager.FindByIdAsync(roleId);

            if (role is null)
            {
                return TypedResults.NotFound();
            }

            var claims = data.Select(item => new Claim(item.Type!, item.Value!));

            foreach (var claim in claims)
            {
                var result = await roleManager.AddClaimAsync(role, claim);

                if (!result.Succeeded)
                {
                    return CreateValidationProblem(result);
                }
            }

            return TypedResults.Ok();
        });
    }

    /// <summary>
    /// Maps the endpoint to remove claims from a role.
    /// </summary>
    /// <returns>A <see cref="RouteHandlerBuilder"/> that can be used to further customize the endpoint.</returns>
    public RouteHandlerBuilder MapRemoveClaimEndpoint()
    {
        return routeGroup.MapDelete("/{roleId}/claims", async Task<Results<Ok, ValidationProblem, NotFound>>
            ([FromServices] IServiceProvider sp, [FromRoute] string roleId, [FromBody] ClaimRequest[] data) =>
        {
            var roleManager = sp.GetRequiredService<RoleManager<TRole>>();

            var role = await roleManager.FindByIdAsync(roleId);

            if (role is null)
            {
                return TypedResults.NotFound();
            }

            var claims = data.Select(item => new Claim(item.Type!, item.Value!));

            foreach (var claim in claims)
            {
                var result = await roleManager.RemoveClaimAsync(role, claim);

                if (!result.Succeeded)
                {
                    return CreateValidationProblem(result);
                }
            }

            return TypedResults.Ok();
        });
    }
}
