namespace TestApp.Exceptions;

public class NullValueException : UnexpectedValueException
{
	public NullValueException(string propertyName)
		: base($"Expected a non null value '{propertyName}'")
	{
	}

	public NullValueException(string propertyName, Exception? innerException) 
		: base($"Expected a non null value '{propertyName}'", innerException)
	{
	}
}