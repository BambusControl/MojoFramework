using System.Runtime.Serialization;

namespace MojoFramework.Exceptions;

/// <summary>
///		Base exception for MojoFramework
/// </summary>
[Serializable]
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
