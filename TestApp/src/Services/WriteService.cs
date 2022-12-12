using MojoFramework.Attributes;

namespace TestApp.Services;

[ComponentInterface]
public interface WriteService
{
	void WritePowersOfTwo(int count);
}
