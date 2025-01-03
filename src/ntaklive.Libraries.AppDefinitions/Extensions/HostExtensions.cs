using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ntaklive.Libraries.AppDefinitions.Interfaces;

namespace ntaklive.Libraries.AppDefinitions.Extensions;

public static class HostExtensions
{
    public static Task InitializeDefinitionsAsync(this IHost host)
    {
        var modules = host.Services.GetRequiredService<List<IAppDefinition>>();
        var sortedInitializeModules = TopologicalSortHelper.TopologicalSort(modules, x => x.InitializeAsyncDependsOn);
        return Task.WhenAll(sortedInitializeModules.Select(x => x.InitializeAsync(host.Services)));
    }
}