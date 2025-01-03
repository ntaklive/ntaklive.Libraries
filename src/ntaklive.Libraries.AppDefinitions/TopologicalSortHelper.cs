using ntaklive.Libraries.AppDefinitions.Interfaces;

namespace ntaklive.Libraries.AppDefinitions;

internal static class TopologicalSortHelper
{
    public static List<IAppDefinition> TopologicalSort(List<IAppDefinition> modules, Func<IAppDefinition, Type[]> getDependencies)
    {
        var sorted = new List<IAppDefinition>();
        var visited = new Dictionary<Type, bool>();

        foreach (var module in modules)
        {
            Visit(module, visited, sorted, modules, getDependencies);
        }

        return sorted;
    }

    private static void Visit(IAppDefinition module, Dictionary<Type, bool> visited, List<IAppDefinition> sorted, List<IAppDefinition> allModules, Func<IAppDefinition, Type[]> getDependencies)
    {
        var alreadyVisited = visited.TryGetValue(module.GetType(), out var inProcess);

        if (alreadyVisited)
        {
            if (inProcess)
            {
                throw new InvalidOperationException("Cyclic dependency found.");
            }
        }
        else
        {
            visited[module.GetType()] = true;

            foreach (var dependency in getDependencies(module))
            {
                var dependentModule = allModules.FirstOrDefault(x => x.GetType() == dependency);
                if (dependentModule == null)
                {
                    throw new InvalidOperationException($"Dependency {dependency.Name} not found for {module.GetType().Name}.");
                }
                Visit(dependentModule, visited, sorted, allModules, getDependencies);
            }

            visited[module.GetType()] = false;
            sorted.Add(module);
        }
    }
}