using MojoFramework.Attributes.Common;

namespace MojoFramework.Attributes.ComponentDefinition.Scopes;

/// <summary>
/// Specifies the scope of this component as Singleton
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class SingletonAttribute : ScopeAttribute
{
	public SingletonAttribute() : base(PredefinedScope.Singleton)
	{
	}
}
