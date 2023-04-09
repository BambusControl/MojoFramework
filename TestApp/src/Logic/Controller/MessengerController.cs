using MojoFramework.Attributes.Component;
using MojoFramework.Attributes.Configuration;
using TestApp.Data.DTO.Response;
using TestApp.Data.Model;
using TestApp.Logic.Service;

namespace TestApp.Logic.Controller;

[Controller]
public class MessengerController
{
	private readonly MessageService messageService;

	public MessengerController(
		MessageService messageService
	)
	{
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
