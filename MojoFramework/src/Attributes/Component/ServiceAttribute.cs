namespace MojoFramework.Attributes.Component;

/// <summary>
/// Services fulfill a specific use case or business operation.
/// Implemented as stateless,
/// transactional components they orchestrate the interactions between the model and other components.
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class ServiceAttribute : ComponentAttribute
{
}
