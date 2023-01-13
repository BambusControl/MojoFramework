namespace MojoFramework.Attributes.Common.Scopes;

/// <summary>
///     Specifies the scope of a component
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class ScopeAttribute : Attribute
{
	public readonly PredefinedScope scope;

	public ScopeAttribute(PredefinedScope scope)
	{
		this.scope = scope;
	}
}
