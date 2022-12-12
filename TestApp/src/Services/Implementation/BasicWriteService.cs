using MojoFramework.Attributes;
using TestApp.Common;

namespace TestApp.Services.Implementation;

[Component]
public class BasicWriteService : WriteService
{
	private readonly MathService mathService;

	public BasicWriteService(MathService mathService)
	{
		this.mathService = mathService;
	}

	public void WritePowersOfTwo(int count)
	{
		mathService
			.PowersOfTwo()
			.Take(count)
			.ForEach(Console.WriteLine);
	}
}
