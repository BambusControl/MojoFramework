using MojoFramework.Attributes;

namespace TestApp.Services;

[ComponentInterface]
public interface MathService
{
	IEnumerable<ulong> PowersOfTwo();
}
