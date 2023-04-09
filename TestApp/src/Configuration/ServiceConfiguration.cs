using MojoFramework.Attributes.Component.Scope;
using MojoFramework.Attributes.Configuration;
using MojoFramework.Attributes.Instance;
using TestApp.Logic.Repository;
using TestApp.Logic.Service;

namespace TestApp.Configuration;

[Configuration]
public class ServiceConfiguration
{

	[Instance, Transient]
	public AddressBookService AddressBookServiceInstance(PersonRepository personRepo, MessageRepository msgRepo)
	{
		return new AddressBookService(personRepo, msgRepo);
	}
}
