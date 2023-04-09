namespace MojoFramework.Attributes.Component.Scope;

/// <summary>
///     Specifies the scope of this component as Transient
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false)]
public sealed class TransientAttribute : ScopeAttribute
{
	public TransientAttribute() : base(PredefinedScope.Transient)
	{
	}
}
