using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MojoFramework.Attributes.Component;
using MojoFramework.Attributes.Component.Scope;
using MojoFramework.Attributes.Configuration;
using MojoFramework.Attributes.Instance;
using MojoFramework.Common.Extensions;
using MojoFramework.Exceptions;

namespace MojoFramework.Registration;

public static class ComponentScanner
{
	private const PredefinedScope DefaultScope = PredefinedScope.Singleton;

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
			var scope = scopeAttribute?.scope ?? DefaultScope;

			// Provide component implementation
			AddComponent(services, scope, implementation);

			// Bind component implementation to component interface
			AddComponentInterface(services, scope, implementation);
		}

		// User-defined instance factories - Configuration classes
		var configTypes = types.WithCustomAttribute<ConfigurationAttribute, Type>();

		foreach (var configType in configTypes)
		{
			CreateInjectableInstances(configType, services);
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

			case PredefinedScope.Prototype:
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

			case PredefinedScope.Prototype:
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

	private static object? InvokeWithInjectedParameters(
		Type injectableInstance,
		MethodBase method,
		IServiceProvider serviceProvider
	)
	{
		var instance = serviceProvider.GetRequiredService(injectableInstance);
		return InvokeWithInjectedParameters(instance, method, serviceProvider);
	}

	private static object? InvokeWithInjectedParameters(
		object instance,
		MethodBase method,
		IServiceProvider serviceProvider
	)
	{
		var parameters = method.GetParameters()
			.Select(p => serviceProvider.GetRequiredService(p.ParameterType))
			.ToArray();

		return method.Invoke(instance, parameters);
	}

	private static void CreateInjectableInstances(Type configurationType, IServiceCollection services)
	{
		var instanceFactoryMethods = configurationType
			.GetMethods(BindingFlags.Instance | BindingFlags.Public)
			.WithCustomAttribute<InstanceAttribute>();

		foreach (var factoryMethod in instanceFactoryMethods)
		{
			var serviceType = factoryMethod.ReturnType;

			object ServiceFactory(IServiceProvider serviceProvider)
				=> InvokeWithInjectedParameters(configurationType, factoryMethod, serviceProvider)
					?? throw new LogicException("Factory method cannot return null");

			var scope = factoryMethod.GetCustomAttribute<ScopeAttribute>()?.scope;
			var serviceScope = (scope ?? DefaultScope).ToServiceLifetime();

			services.Add(ServiceDescriptor.Describe(
				serviceType,
				ServiceFactory,
				serviceScope
			));

			foreach (var @interface in serviceType.GetInterfaces())
			{
				services.AddTransient(@interface, serviceType);
			}
		}
	}
}
