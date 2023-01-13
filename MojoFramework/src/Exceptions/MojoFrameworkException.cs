using System.Runtime.Serialization;

namespace MojoFramework.Exceptions;

/// <summary>
///		Base exception for MojoFramework
/// </summary>
public class MojoFrameworkException : Exception
{
	public MojoFrameworkException()
	{
	}

	protected MojoFrameworkException(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}

	public MojoFrameworkException(string? message) : base(message)
	{
	}

	public MojoFrameworkException(string? message, Exception? innerException) : base(message, innerException)
	{
	}
}

/// <summary>
///		Thrown on code-breaking occasions
/// </summary>
public sealed class LogicException : MojoFrameworkException
{
	public LogicException(string? message) : base(message)
	{
	}
}
