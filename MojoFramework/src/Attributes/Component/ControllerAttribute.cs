namespace MojoFramework.Attributes.Component;

/// <summary>
/// Controllers are responsible for handling incoming requests from external clients.
/// They interpret the incoming requests,
/// call the appropriate application services to execute the requested operation,
/// and return the results to the client.
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class ControllerAttribute : ComponentAttribute
{
}
