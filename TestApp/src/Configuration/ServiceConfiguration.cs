using MojoFramework.Attributes.Common.Scopes;
using MojoFramework.Attributes.Configuration;
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
