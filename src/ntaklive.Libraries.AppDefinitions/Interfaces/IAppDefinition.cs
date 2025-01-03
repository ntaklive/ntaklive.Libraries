using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ntaklive.Libraries.AppDefinitions.Interfaces;

public interface IAppDefinition
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration);
    public Task InitializeAsync(IServiceProvider provider);
    public Type[] ConfigureServicesDependsOn { get; }
    public Type[] InitializeAsyncDependsOn { get; }
}