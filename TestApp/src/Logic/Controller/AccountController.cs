using MojoFramework.Attributes.Configuration;
using TestApp.Data.DTO.Response;
using TestApp.Logic.Service;

namespace TestApp.Logic.Controller;

[Component]
public class AccountController
{
	private readonly AddressBookService addressService;
	private readonly MessageService messageService;

	public AccountController(
		AddressBookService addressService,
		MessageService messageService
	)
	{
		this.addressService = addressService;
		this.messageService = messageService;
	}

	public UserInfoResponseDto GetUserInfo(string name)
	{
		var person = addressService.GetPerson(name);
		var contactList = addressService.GetContactsOf(person).Select(c => c.Name);
		var recievedCount = messageService.GetMessagesFor(person.Name).Count();
		var sentCount = messageService.GetMessagesBy(person.Name).Count();

		return new UserInfoResponseDto(
			person.Name,
			person.BirthDate,
			contactList,
			recievedCount,
			sentCount
		);
	}
}
