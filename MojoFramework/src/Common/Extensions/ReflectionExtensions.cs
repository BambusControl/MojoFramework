using System.Diagnostics;
using System.Formats.Asn1;
using System.Reflection;
using MojoFramework.Attributes.Common.Scopes;

namespace MojoFramework.Common.Extensions;

internal static class ReflectionExtensions
{
	internal static bool HasCustomAttribute(this ICustomAttributeProvider attributeProvider, Type attributeType)
	{
		return attributeProvider.IsDefined(attributeType, inherit: false);
	}

	internal static bool HasCustomAttribute<TAttribute>(this ICustomAttributeProvider attributeProvider)
		where TAttribute : Attribute
	{
		return attributeProvider.HasCustomAttribute(typeof(TAttribute));
	}

	internal static IEnumerable<T> WithCustomAttribute<T>(this IEnumerable<T> providers, Type attributeType)
		where T : ICustomAttributeProvider
	{
		return providers.Where(t => t.HasCustomAttribute(attributeType));
	}

	internal static IEnumerable<T> WithCustomAttribute<TAttribute, T>(this IEnumerable<T> providers)
		where T : ICustomAttributeProvider
		where TAttribute : Attribute
	{
		return providers.Where(t => t.HasCustomAttribute<TAttribute>());
	}

	#region TypeInferred

	internal static IEnumerable<Type> WithCustomAttribute<TAttribute>(this IEnumerable<Type> types)
		where TAttribute : Attribute
	{
		return types.WithCustomAttribute<TAttribute, Type>();
	}

	internal static IEnumerable<MethodInfo> WithCustomAttribute<TAttribute>(this IEnumerable<MethodInfo> methods)
		where TAttribute : Attribute
	{
		return methods.WithCustomAttribute<TAttribute, MethodInfo>();
	}

	#endregion
}
