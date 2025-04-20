using System.Security.Claims;

namespace IdentityEndpoints.Components.Shared.Data;

/// <summary>
/// Represents the response related to <see cref="Claim"/> model.
/// </summary>
public class ClaimResponse
{
    /// <summary>
    /// Gets or sets the type for this claim.
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// Gets or sets the value for this claim.
    /// </summary>
    public string? Value { get; set; }
}
