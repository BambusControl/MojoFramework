using System.Collections.Immutable;
using MojoFramework.Attributes;
using TestApp.Data.DTO.Response;
using TestApp.Data.Model;
using TestApp.Logic.Service;

namespace TestApp.Logic.Controller;

[Component]
public class MessengerController
{
	private readonly AddressBookService addressService;
	private readonly MessageService messageService;

	public MessengerController(
		AddressBookService addressService,
		MessageService messageService
	)
	{
		this.addressService = addressService;
		this.messageService = messageService;
	}

	public InboxResponseDto GetInbox(string name)
	{
		return new InboxResponseDto(
			messageService.GetMessagesFor(name),
			messageService.GetMessagesBy(name)
		);
	}

	public MessageModel WriteMessage(MessageDraftModel message)
	{
		return messageService.SendMessage(message);
	}
}
