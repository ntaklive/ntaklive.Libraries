using ntaklive.Libraries.EndpointDefinitions.Interfaces;

namespace ntaklive.Libraries.EndpointDefinitions;

/// <summary>
/// Base implementation of <see cref="IWebApiEndpointDefinition"/>
/// </summary>
public abstract class WebApiEndpointDefinition : IWebApiEndpointDefinition
{
    public virtual void ConfigureEndpoint(IEndpointRouteBuilder builder) {}
}   