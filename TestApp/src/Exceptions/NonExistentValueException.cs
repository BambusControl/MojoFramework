namespace TestApp.Exceptions;

public class NonExistentValueException : UnexpectedValueException
{
	public NonExistentValueException(string propertyName, string propertyValue)
		: base($"No value found for '{propertyName} = {propertyValue}'")
	{
	}

	public NonExistentValueException(string propertyName, string propertyValue, Exception? innerException)
		: base($"No value found for '{propertyName} = {propertyValue}'", innerException)
	{
	}
}