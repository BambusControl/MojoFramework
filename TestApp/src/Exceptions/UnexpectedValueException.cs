namespace TestApp.Exceptions;

public abstract class UnexpectedValueException : TestAppException
{
	protected UnexpectedValueException()
	{
	}

	protected UnexpectedValueException(string? message) : base(message)
	{
	}

	protected UnexpectedValueException(string? message, Exception? innerException) : base(message, innerException)
	{
	}
}