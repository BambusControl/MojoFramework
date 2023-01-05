using System.Diagnostics.CodeAnalysis;

namespace TestApp.Common;

public static class Extensions
{
	public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
	{
		foreach (var item in enumerable)
		{
			action.Invoke(item);
		}
	}

	[SuppressMessage("ReSharper", "NullableWarningSuppressionIsUsed")]
	public static IEnumerable<T> WhereNotDefault<T>(this IEnumerable<T?> enumerable)
	{
		return enumerable.Where(value => !Equals(default(T), value))
			.Select(value => (T) value!);
	}
}
