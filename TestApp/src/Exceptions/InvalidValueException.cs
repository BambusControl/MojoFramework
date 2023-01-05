namespace TestApp.Exceptions;

public class InvalidValueException : UnexpectedValueException
{
	public InvalidValueException(string propertyName, string reason)
		: base($"Invalid value for '{propertyName}'. Reason: {reason}")
	{
	}

	public InvalidValueException(string propertyName, string reason, Exception? innerException)
		: base($"Invalid value for '{propertyName}'. Reason: {reason}", innerException)
	{
	}
}