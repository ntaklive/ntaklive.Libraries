using System.Reflection;
using ntaklive.Libraries.EndpointDefinitions.Interfaces;

namespace ntaklive.Libraries.EndpointDefinitions.Extensions;

public static class HostApplicationBuilderExtensions
{
    public static void MapWebApiEndpoints(this IEndpointRouteBuilder builder, params Assembly[] assemblies)
    {
        if (assemblies.Length == 0)
        {
            assemblies = [Assembly.GetCallingAssembly()];
        }
        
        var modules = new List<IWebApiEndpointDefinition>();
        foreach (var assembly in assemblies)
        {
            var types = assembly.ExportedTypes.Where(x => !x.IsAbstract && typeof(IWebApiEndpointDefinition).IsAssignableFrom(x));
            var instances = types.Select(Activator.CreateInstance).Cast<IWebApiEndpointDefinition>().ToArray();
            modules.AddRange(instances);
        }
        
        modules.ForEach(module => module.ConfigureEndpoint(builder));
    }
}