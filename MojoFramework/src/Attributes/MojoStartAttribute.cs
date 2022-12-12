namespace MojoFramework.Attributes;

/// <summary>
/// Labels a method to be invoked after application host is initialized.
/// </summary>
/// <remarks>
/// The method must be static and declared inside a type with the <see cref="MojoApplicationAttribute"/>.
/// </remarks>
[AttributeUsage(AttributeTargets.Method)]
public sealed class MojoStartAttribute : Attribute
{
}
