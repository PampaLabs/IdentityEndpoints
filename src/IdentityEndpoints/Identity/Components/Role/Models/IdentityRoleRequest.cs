using Microsoft.AspNetCore.Identity;

namespace IdentityEndpoints.Components.Role;

/// <summary>
/// Represents the request related to <see cref="IdentityRole"/> model.
/// </summary>
public class IdentityRoleRequest
{
    /// <summary>
    /// The name of the role.
    /// </summary>
    public string? Name { get; set; }
}

