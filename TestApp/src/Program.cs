using Microsoft.Extensions.DependencyInjection;
using MojoFramework;
using MojoFramework.Attributes;
using TestApp.Services;

namespace TestApp;

[MojoApplication]
public class Program
{
	public static void Main(string[] args)
	{
		Mojo.Run<Program>(args);
	}

	[MojoStart]
	private static void DoStuff(IServiceProvider provider)
	{
		var one = provider.GetRequiredService<WriteService>();
		var two = provider.GetRequiredService<MathService>();
		var three = provider.GetRequiredService<MathService>();
		one.WritePowersOfTwo(count: 5);
	}
}
