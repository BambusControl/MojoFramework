namespace MojoFramework.Attributes;

/// <summary>
/// Marks this as an MojoFramework injectable instance.
/// </summary>
/// <remarks>
/// Currently, all components adapt the 'singleton' lifecycle.
/// </remarks>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class ComponentAttribute : Attribute
{
}
