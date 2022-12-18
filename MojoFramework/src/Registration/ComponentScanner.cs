using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MojoFramework.Attributes;
using MojoFramework.Attributes.Common;
using MojoFramework.Attributes.ComponentDefinition.Scopes;
using MojoFramework.Common.Extensions;

namespace MojoFramework.Registration;

public static class ComponentScanner
{
    public static void ScanAndExposeComponents(IServiceCollection services, Assembly assembly)
    {
        // TODO DEBUG: Added service provider
        services.AddSingleton(serviceProvider => serviceProvider);

        var types = assembly.GetTypes();
        var implementations = types.WithCustomAttribute<ComponentAttribute, Type>();

        foreach (var implementation in implementations)
        {
            // Get requested scope of an instance
            var scopeAttribute = implementation.GetCustomAttribute<ScopeAttribute>();

            // Assign singleton scope by default
            var scope = scopeAttribute?.scope ?? PredefinedScope.Singleton;

			// Provide component implementation
            AddComponent(services, scope, implementation);

            // Bind component implementation to component interface
            AddComponentInterface(services, scope, implementation);
        }
    }

    private static void AddComponentInterface(IServiceCollection services, PredefinedScope scope, Type implementation)
    {
        // Get component interfaces
		var definitions = implementation.GetInterfaces();

        foreach (var definition in definitions)
        {
            AddComponent(services, scope, definition, provider => provider.GetRequiredService(implementation));
        }
    }

    private static void AddComponent(IServiceCollection services, PredefinedScope scope, Type type)
    {
        switch (scope)
        {
            case PredefinedScope.Singleton:
            {
                services.AddSingleton(type);
                break;
            }

            case PredefinedScope.Scoped:
            {
                services.AddScoped(type);
                break;
            }

            case PredefinedScope.Transient:
            {
                services.AddTransient(type);
                break;
            }

            default:
            {
                throw new ArgumentOutOfRangeException(nameof(scope), scope.ToString(), "Unhandled scope definition");
            }
        }
    }

    private static void AddComponent(
        IServiceCollection services,
        PredefinedScope scope,
        Type type,
        Func<IServiceProvider, object> implementationFactory
    )
    {
        switch (scope)
        {
            case PredefinedScope.Singleton:
            {
                services.AddSingleton(type, implementationFactory);
                break;
            }

            case PredefinedScope.Scoped:
            {
                services.AddScoped(type, implementationFactory);
                break;
            }

            case PredefinedScope.Transient:
            {
                services.AddTransient(type, implementationFactory);
                break;
            }

            default:
            {
                throw new ArgumentOutOfRangeException(nameof(scope), scope.ToString(), "Unhandled scope definition");
            }
        }
    }
}
