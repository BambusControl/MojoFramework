namespace MojoFramework.Attributes.Component.Scope;

/// <summary>
///     Specifies the scope of this component as Prototype
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false)]
public sealed class PrototypeAttribute : ScopeAttribute
{
	public PrototypeAttribute() : base(PredefinedScope.Prototype)
	{
	}
}
