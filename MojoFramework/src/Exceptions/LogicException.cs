using System.Runtime.Serialization;

namespace MojoFramework.Exceptions;

/// <summary>
///		Thrown on code-breaking occasions
/// </summary>
[Serializable]
public sealed class LogicException : MojoFrameworkException
{
	public LogicException(string? message) : base(message)
	{
	}

	private LogicException(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}
}