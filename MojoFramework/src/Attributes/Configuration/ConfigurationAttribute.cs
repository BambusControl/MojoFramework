using MojoFramework.Attributes.Component;
using MojoFramework.Attributes.Instance;

namespace MojoFramework.Attributes.Configuration;

/// <summary>
///     Marks a configuration class providing factory methods
///     for MojoFramework injectable instances via <see cref="InstanceAttribute" />.
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class ConfigurationAttribute : ComponentAttribute
{
}
