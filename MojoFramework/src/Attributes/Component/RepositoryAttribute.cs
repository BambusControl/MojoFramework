namespace MojoFramework.Attributes.Component;

/// <summary>
/// Repositories are responsible for persisting and retrieving aggregate roots to and from a data store.
/// They abstract the details of the underlying data store and provide a simple,
/// domain-oriented interface for working with the model.
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class RepositoryAttribute : ComponentAttribute
{
}
