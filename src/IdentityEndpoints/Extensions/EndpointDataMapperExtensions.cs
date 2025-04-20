namespace IdentityEndpoints;

/// <summary>
/// Provides extension methods for <see cref="IEndpointDataMapper{TEntity, TRequest, TResponse}"/>.
/// </summary>
public static class EndpointDataMapperExtensions
{
    /// <summary>
    /// Creates a new entity instance and populates it with data from a request object using the mapper.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity to create.</typeparam>
    /// <typeparam name="TRequest">The type of the request object.</typeparam>
    /// <typeparam name="TResponse">The type of the response object.</typeparam>
    /// <param name="mapper">The mapper used to perform the data transformation.</param>
    /// <param name="source">The request object containing the data.</param>
    /// <returns>A new entity populated with data from the request.</returns>
    public static TEntity FromRequest<TEntity, TRequest, TResponse>(this IEndpointDataMapper<TEntity, TRequest, TResponse> mapper, TRequest source)
        where TEntity : new()
    {
        var destination = new TEntity();
        mapper.FromRequest(source, destination);
        return destination;
    }

    /// <summary>
    /// Creates a new response instance and populates it with data from an entity using the mapper.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity object.</typeparam>
    /// <typeparam name="TRequest">The type of the request object.</typeparam>
    /// <typeparam name="TResponse">The type of the response object to create.</typeparam>
    /// <param name="mapper">The mapper used to perform the data transformation.</param>
    /// <param name="source">The entity containing the data.</param>
    /// <returns>A new response object populated with data from the entity.</returns>
    public static TResponse ToResponse<TEntity, TRequest, TResponse>(this IEndpointDataMapper<TEntity, TRequest, TResponse> mapper, TEntity source)
        where TResponse : new()
    {
        var destination = new TResponse();
        mapper.ToResponse(source, destination);
        return destination;
    }
}
