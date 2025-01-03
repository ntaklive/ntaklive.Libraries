using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ntaklive.Libraries.AppDefinitions.Interfaces;

namespace ntaklive.Libraries.AppDefinitions;

/// <summary>
///     Base implementation of <see cref="IAppDefinition" />
/// </summary>
public abstract class AppDefinition : IAppDefinition
{
    /// <summary>
    ///     Order of <see cref="ConfigureServices" /> invoking
    /// </summary>
    /// <example>
    /// Specify AppDefinition types on which the current AppDefinition will depend:<br/>
    ///     <code>
    ///         public override Type[] InitializeAsyncDependsOn => [typeof(YourAppDefinition)];
    ///     </code>
    /// </example>
    public virtual Type[] ConfigureServicesDependsOn => [];
    
    /// <summary>
    ///     Order of <see cref="InitializeAsync" /> invoking
    /// </summary>
    /// <example>
    /// Specify AppDefinition types on which the current AppDefinition will depend:<br/>
    ///     <code>
    ///         public override Type[] InitializeAsyncDependsOn => [typeof(YourAppDefinition)];
    ///     </code>
    /// </example>
    public virtual Type[] InitializeAsyncDependsOn => [];

    /// <summary>
    ///     Configure DI container (register your services here)
    /// </summary>
    /// <param name="services">Host's Service collection</param>
    /// <param name="configuration">Host's configuration</param>
    public virtual void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
    }

    /// <summary>
    ///     Initialize the current AppDefinition
    /// </summary>
    /// <param name="provider">Host's service provider</param>
    public virtual Task InitializeAsync(IServiceProvider provider)
    {
        provider.GetRequiredService<ILogger<AppDefinition>>()
            .LogInformation("{AppDefinitionName} initialized.", GetType().Name);

        return Task.CompletedTask;
    } 
} 