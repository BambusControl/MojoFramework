using System.Reflection;
using Microsoft.Extensions.Hosting;
using MojoFramework.Registration;

namespace MojoFramework.Common.Extensions;

internal static class HostBuilderExtensions
{
    internal static IHostBuilder UseComponentScanning(this IHostBuilder hostBuilder, Assembly assembly)
    {
        return hostBuilder.ConfigureServices(services => ComponentScanner.ScanAndExposeComponents(services, assembly));
    }

    internal static IHostBuilder UseComponentScanning(this IHostBuilder hostBuilder, Type app)
    {
        return hostBuilder.UseComponentScanning(app.Assembly);
    }

    internal static IHostBuilder UseComponentScanning<TApp>(this IHostBuilder hostBuilder)
    {
        return hostBuilder.UseComponentScanning(typeof(TApp));
    }
}
