using Microsoft.AspNetCore.Identity;

namespace IdentityEndpoints.Components.Role;

/// <summary>
/// Represents the response related to <see cref="IdentityRole"/> model.
/// </summary>
public class IdentityRoleResponse
{
    /// <summary>
    /// The unique identifier of the role.
    /// </summary>
    public string Id { get; set; } = default!;

    /// <summary>
    /// The name of the role.
    /// </summary>
    public string? Name { get; set; }
}
