using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ntaklive.Libraries.AppDefinitions.Interfaces;

namespace ntaklive.Libraries.AppDefinitions.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddDefinitions(this IHostApplicationBuilder builder, params Assembly[] assemblies)
    {
        if (assemblies.Length == 0)
        {
            assemblies = [Assembly.GetCallingAssembly()];
        }
        
        var modules = new List<IAppDefinition>();
        foreach (var assembly in assemblies)
        {
            var types = assembly.ExportedTypes.Where(x => !x.IsAbstract && typeof(IAppDefinition).IsAssignableFrom(x));
            var instances = types.Select(Activator.CreateInstance).Cast<IAppDefinition>().ToArray();
            modules.AddRange(instances);
        }

        // Sort modules based on dependencies for configuration
        var sortedConfigureServicesModules = TopologicalSortHelper.TopologicalSort(modules, x => x.ConfigureServicesDependsOn);

        builder.Services.AddSingleton(sortedConfigureServicesModules);
        sortedConfigureServicesModules.ForEach(module => module.ConfigureServices(builder.Services, builder.Configuration));
    }
}