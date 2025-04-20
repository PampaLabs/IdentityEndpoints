using Microsoft.AspNetCore.Identity;

namespace IdentityEndpoints.Components.User;

/// <summary>
/// Maps data between <see cref="IdentityUser"/> entities and their corresponding request and response models.
/// </summary>
public class IdentityUserDataMapper : IEndpointDataMapper<IdentityUser, IdentityUserRequest, IdentityUserResponse>
{
    /// <summary>
    /// Populates an <see cref="IdentityUser"/> entity with data from an <see cref="IdentityUserRequest"/>.
    /// </summary>
    /// <param name="source">The request containing the user data.</param>
    /// <param name="destination">The entity to populate.</param>
    public void FromRequest(IdentityUserRequest source, IdentityUser destination)
    {
        destination.UserName = source.UserName;
        destination.Email = source.Email;
        destination.EmailConfirmed = source.EmailConfirmed;
        destination.PhoneNumber = source.PhoneNumber;
        destination.PhoneNumberConfirmed = source.PhoneNumberConfirmed;
        destination.TwoFactorEnabled = source.TwoFactorEnabled;
        destination.LockoutEnd = source.LockoutEnd;
        destination.LockoutEnabled = source.LockoutEnabled;
    }

    /// <summary>
    /// Populates an <see cref="IdentityUserResponse"/> with data from an <see cref="IdentityUser"/> entity.
    /// </summary>
    /// <param name="source">The entity containing the user data.</param>
    /// <param name="destination">The response object to populate.</param>
    public void ToResponse(IdentityUser source, IdentityUserResponse destination)
    {
        destination.Id = source.Id;
        destination.UserName = source.UserName;
        destination.Email = source.Email;
        destination.EmailConfirmed = source.EmailConfirmed;
        destination.PhoneNumber = source.PhoneNumber;
        destination.PhoneNumberConfirmed = source.PhoneNumberConfirmed;
        destination.TwoFactorEnabled = source.TwoFactorEnabled;
        destination.LockoutEnd = source.LockoutEnd;
        destination.LockoutEnabled = source.LockoutEnabled;
    }
}

