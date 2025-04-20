using Microsoft.AspNetCore.Identity;

namespace IdentityEndpoints.Components.User;

/// <summary>
/// Represents the response related to <see cref="IdentityUser"/> model.
/// </summary>
public class IdentityUserResponse
{
    /// <summary>
    /// Gets or sets the primary key for this user.
    /// </summary>
    public string Id { get; set; } = default!;

    /// <summary>
    /// Gets or sets the user name for this user.
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// Gets or sets the email address for this user.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets a flag indicating if a user has confirmed their email address.
    /// </summary>
    /// <value>True if the email address has been confirmed, otherwise false.</value>
    public bool EmailConfirmed { get; set; }

    /// <summary>
    /// Gets or sets a telephone number for the user.
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Gets or sets a flag indicating if a user has confirmed their telephone address.
    /// </summary>
    /// <value>True if the telephone number has been confirmed, otherwise false.</value>
    public bool PhoneNumberConfirmed { get; set; }

    /// <summary>
    /// Gets or sets a flag indicating if two factor authentication is enabled for this user.
    /// </summary>
    /// <value>True if 2fa is enabled, otherwise false.</value>
    public bool TwoFactorEnabled { get; set; }

    /// <summary>
    /// Gets or sets the date and time, in UTC, when any user lockout ends.
    /// </summary>
    /// <remarks>
    /// A value in the past means the user is not locked out.
    /// </remarks>
    public DateTimeOffset? LockoutEnd { get; set; }

    /// <summary>
    /// Gets or sets a flag indicating if the user could be locked out.
    /// </summary>
    /// <value>True if the user could be locked out, otherwise false.</value>
    public bool LockoutEnabled { get; set; }
}
