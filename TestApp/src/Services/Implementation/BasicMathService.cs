using MojoFramework.Attributes;
using MojoFramework.Attributes.ComponentDefinition.Scopes;

namespace TestApp.Services.Implementation;

[Component, Transient]
public class BasicMathService : MathService
{
	public BasicMathService()
	{
		Console.WriteLine("HELLO");
	}

	public IEnumerable<ulong> PowersOfTwo()
	{
		ulong value = 1;

		while (true)
		{
			yield return value;
			value *= 2;
		}

		// ReSharper disable once IteratorNeverReturns
	}
}
