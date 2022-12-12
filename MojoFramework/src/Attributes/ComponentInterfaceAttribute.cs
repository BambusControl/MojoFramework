namespace MojoFramework.Attributes;

/// <summary>
/// Marks this as an MojoFramework injectable type.
/// </summary>
/// <remarks>
/// For dependency injection to work an implementation of the interface must be provided
/// and given the <see cref="ComponentAttribute"/>.
/// </remarks>
[AttributeUsage(AttributeTargets.Interface)]
public sealed class ComponentInterfaceAttribute : Attribute
{
}
