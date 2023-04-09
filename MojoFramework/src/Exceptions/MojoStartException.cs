using System.Runtime.Serialization;

namespace MojoFramework.Exceptions;

[Serializable]
public class MojoStartException : MojoFrameworkException
{
	public MojoStartException()
	{
	}

	protected MojoStartException(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}

	public MojoStartException(string? message) : base(message)
	{
	}

	public MojoStartException(string? message, Exception? innerException) : base(message, innerException)
	{
	}
}
