namespace IdentityEndpoints;

/// <summary>
/// Defines methods to map data between request/response objects and an entity.
/// </summary>
/// <typeparam name="TEntity">The type of the entity involved in the mapping.</typeparam>
/// <typeparam name="TRequest">The type of the request object.</typeparam>
/// <typeparam name="TResponse">The type of the response object.</typeparam>
public interface IEndpointDataMapper<in TEntity, in TRequest, in TResponse>
{
    /// <summary>
    /// Maps data from a request object to an entity.
    /// </summary>
    /// <param name="source">The request object containing incoming data.</param>
    /// <param name="destination">The entity to populate with data from the request.</param>
    void FromRequest(TRequest source, TEntity destination);

    /// <summary>
    /// Maps data from an entity to a response object.
    /// </summary>
    /// <param name="source">The entity containing the source data.</param>
    /// <param name="destination">The response object to populate with data from the entity.</param>
    void ToResponse(TEntity source, TResponse destination);
}
