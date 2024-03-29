﻿namespace TestApp.Common;

public static class Extensions
{
	public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
	{
		foreach (var item in enumerable)
		{
			action.Invoke(item);
		}
	}
}
