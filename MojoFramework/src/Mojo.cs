using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MojoFramework.Attributes;
using MojoFramework.Common;
using MojoFramework.Common.Extensions;

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
			.GetTypes();

		var startMethod = assemblyTypes
			.WithCustomAttribute<MojoApplicationAttribute>().First()
			.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
			.WithCustomAttribute<MojoStartAttribute>().First();

		var parameters = startMethod.GetParameters()
			.Select(p => services.GetRequiredService(p.ParameterType))
			.ToArray();

		_ = startMethod.Invoke(obj: null, parameters);
	}
}
