using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MojoFramework.Common.Extensions;

namespace MojoFramework;

public sealed class StartupHostBuilder
{
	private readonly Assembly scanAssembly;

	public StartupHostBuilder(Assembly scanAssembly)
	{
		this.scanAssembly = scanAssembly;
	}

	public IHost BuildHost(string[]? arguments = null)
	{
		return Host
			.CreateDefaultBuilder(arguments)
			.ConfigureLogging(ConfigureLogging)
			.ConfigureServices(MojoConfigure)
			.UseComponentScanning(scanAssembly)
			.Build();
	}


	private static void ConfigureLogging(HostBuilderContext context, ILoggingBuilder builder)
	{
		builder.ClearProviders()
			.SetMinimumLevel(LogLevel.Information)
			.AddConsole();
	}

	private void MojoConfigure(HostBuilderContext context, IServiceCollection services)
	{
		services.AddSingleton(scanAssembly);
	}
}
