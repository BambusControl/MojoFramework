namespace MojoFramework.Attributes.Configuration;

/// <summary>
///     Makes this a factory method for MojoFramework injectable instances.
/// </summary>
[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public sealed class InstanceAttribute : Attribute
{
}
