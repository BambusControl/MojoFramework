namespace TestApp.Exceptions;

/// <summary>
/// Base class for all user defined exceptions in this app
/// </summary>
public abstract class TestAppException : Exception
{
	public TestAppException()
	{
	}

	public TestAppException(string? message) : base(message)
	{
	}

	public TestAppException(string? message, Exception? innerException) : base(message, innerException)
	{
	}
}
