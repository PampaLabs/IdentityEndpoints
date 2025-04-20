using Microsoft.AspNetCore.Identity;

namespace IdentityEndpoints.Components.Role;

/// <summary>
/// Maps data between <see cref="IdentityRole"/> entities and their corresponding request and response models.
/// </summary>
public class IdentityRoleDataMapper : IEndpointDataMapper<IdentityRole, IdentityRoleRequest, IdentityRoleResponse>
{
    /// <summary>
    /// Populates an <see cref="IdentityRole"/> entity with data from an <see cref="IdentityRoleRequest"/>.
    /// </summary>
    /// <param name="source">The request containing the role data.</param>
    /// <param name="destination">The entity to populate.</param>
    public void FromRequest(IdentityRoleRequest source, IdentityRole destination)
    {
        destination.Name = source.Name;
    }

    /// <summary>
    /// Populates an <see cref="IdentityRoleResponse"/> with data from an <see cref="IdentityRole"/> entity.
    /// </summary>
    /// <param name="source">The entity containing the role data.</param>
    /// <param name="destination">The response object to populate.</param>
    public void ToResponse(IdentityRole source, IdentityRoleResponse destination)
    {
        destination.Id = source.Id;
        destination.Name = source.Name;
    }
}
