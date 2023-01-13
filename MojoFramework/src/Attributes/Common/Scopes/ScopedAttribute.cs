namespace MojoFramework.Attributes.Common.Scopes;

/// <summary>
///     Specifies the scope of this component as Scoped
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class ScopedAttribute : ScopeAttribute
{
	public ScopedAttribute() : base(PredefinedScope.Scoped)
	{
	}
}
