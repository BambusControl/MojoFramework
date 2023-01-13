using MojoFramework.Attributes.Configuration;
using TestApp.Common;
using TestApp.Data.Model;
using TestApp.Exceptions;
using TestApp.Logic.Repository;

namespace TestApp.Logic.Service;

[Component]
public sealed class AddressBookService
{
	private readonly MessageRepository msgRepo;
	private readonly PersonRepository personRepo;

	public AddressBookService(
		PersonRepository personRepo,
		MessageRepository msgRepo
	)
	{
		this.personRepo = personRepo;
		this.msgRepo = msgRepo;
	}

	public PersonModel GetPerson(string name)
	{
		return personRepo.GetByName(name)?.ToPersonModel()
			?? throw new NonExistentValueException(nameof(name), name);
	}

	public IEnumerable<PersonModel> GetContactsOf(PersonModel person)
	{
		var sender = personRepo.GetByName(person.Name)
			?? throw new NonExistentValueException(nameof(person.Name), person.Name);

		return msgRepo.GetBySenderName(sender.Name)
			.Select(msg => msg.RecipientName)
			.Distinct()
			.Select(personRepo.GetByName)
			.WhereNotDefault()
			.Select(Mapper.ToPersonModel);
	}
}
