using Microsoft.Extensions.DependencyInjection;
using MojoFramework.Attributes.Common;

namespace MojoFramework.Common.Extensions;

internal static class LifetimeScopeExtensions
{
	internal static ServiceLifetime ToServiceLifetime(this PredefinedScope predefinedScope) => predefinedScope switch
	{
		PredefinedScope.Singleton => ServiceLifetime.Singleton,
		PredefinedScope.Scoped => ServiceLifetime.Scoped,
		PredefinedScope.Transient => ServiceLifetime.Transient,
		_ => throw new ArgumentOutOfRangeException(nameof(predefinedScope), predefinedScope, null)
	};

	internal static PredefinedScope ToPredefinedScope(this ServiceLifetime serviceLifetime) => serviceLifetime switch
	{
		ServiceLifetime.Singleton => PredefinedScope.Singleton,
		ServiceLifetime.Scoped => PredefinedScope.Scoped,
		ServiceLifetime.Transient => PredefinedScope.Transient,
		_ => throw new ArgumentOutOfRangeException(nameof(serviceLifetime), serviceLifetime, null)
	};
}
