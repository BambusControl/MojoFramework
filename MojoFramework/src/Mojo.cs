using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MojoFramework.Attributes;
using MojoFramework.Common;
using MojoFramework.Common.Extensions;
using MojoFramework.Exceptions;

namespace MojoFramework;

public static class Mojo
{
	/// <summary>
	///     Starts the MojoFramework application context.
	/// </summary>
	/// <remarks>
	///     Component scanning is performed on the assembly in which the <see cref="TApplicationClass" /> is declared.
	/// </remarks>
	public static void Run<TApplicationClass>(string[] args)
	{
		Run(typeof(TApplicationClass), args);
	}

	/// <summary>
	///     Starts the MojoFramework application context.
	/// </summary>
	/// <remarks>
	///     Component scanning is performed on the assembly in which the <see cref="applicationClass" /> is declared.
	/// </remarks>
	public static void Run(Type applicationClass, string[] args)
	{
		StartupHostBuilder hostBuilder = new(applicationClass.Assembly);
		using var host = hostBuilder.BuildHost(args);
		RunHost(host).GetAwaiter().GetResult();
	}

	private static async Task RunHost(IHost host)
	{
		Console.WriteLine(Resources.BANNER);

		// Build & start the application host
		await host.StartAsync();

		RunApplication(host.Services);

		// Stop the host
		await host.StopAsync();
	}


	private static void RunApplication(IServiceProvider services)
	{
		var assemblyTypes = services
			.GetRequiredService<Assembly>()
			.ExportedTypes;

		var mojoStart = FindMojoStart(assemblyTypes);

		var parameters = mojoStart.GetParameters()
			.Select(p => services.GetRequiredService(p.ParameterType))
			.ToArray();

		_ = mojoStart.Invoke(obj: null, parameters);
	}

	private static MethodInfo FindMojoStart(IEnumerable<Type> assemblyTypes)
	{
		var mojoApp = assemblyTypes
			.WithCustomAttribute<MojoApplicationAttribute>()
			.SingleOrDefault();

		if (mojoApp == null)
		{
			throw new MojoStartException($"Exactly one type with {nameof(MojoApplicationAttribute)} is required.");
		}

		var mojoStart = mojoApp
			.GetMethods(BindingFlags.Static | BindingFlags.Public)
			.WithCustomAttribute<MojoStartAttribute>()
			.SingleOrDefault();

		if (mojoStart == null)
		{
			throw new MojoStartException($"Exactly one method with {nameof(MojoApplicationAttribute)} is required.");
		}

		return mojoStart;
	}
}
