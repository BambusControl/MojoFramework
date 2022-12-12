using MojoFramework.Attributes.Common;

namespace MojoFramework.Attributes.ComponentDefinition.Scopes;

/// <summary>
/// Specifies the scope of this component as Transient
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class TransientAttribute : ScopeAttribute
{
	public TransientAttribute() : base(PredefinedScope.Transient)
	{
	}
}
