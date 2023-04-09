using System.Collections.Immutable;
using MojoFramework.Attributes.Component;
using MojoFramework.Attributes.Configuration;
using TestApp.Common;
using TestApp.Data.Entity;
using TestApp.Data.Model;
using TestApp.Exceptions;
using TestApp.Logic.Repository;

namespace TestApp.Logic.Service;

[Service]
public sealed class MessageService
{
	private readonly MessageRepository msgRepo;
	private readonly PersonRepository personRepo;

	public MessageService(
		PersonRepository personRepo,
		MessageRepository msgRepo
	)
	{
		this.personRepo = personRepo;
		this.msgRepo = msgRepo;
	}

	public IEnumerable<MessageModel> GetMessagesFor(string name)
	{
		var recipient = personRepo.FindByName(name).FirstOrDefault()
			?? throw new NonExistentValueException(nameof(name), name);

		var messages = msgRepo.GetByRecipientName(recipient.Name).ToImmutableList();

		var senders = messages.Select(msg => msg.SenderName)
			.Distinct()
			.Select(personRepo.GetByName)
			.WhereNotDefault()
			.ToImmutableDictionary(p => p.Name, Mapper.ToPersonModel);

		return messages.Select(msg => new MessageModel(
			msg.Id,
			msg.Message,
			senders[msg.SenderName],
			recipient.ToPersonModel(),
			msg.Sent
		));
	}

	public IEnumerable<MessageModel> GetMessagesBy(string name)
	{
		var sender = personRepo.FindByName(name).FirstOrDefault()
			?? throw new NonExistentValueException(nameof(name), name);

		var messages = msgRepo.GetBySenderName(sender.Name).ToImmutableList();

		var recipients = messages.Select(msg => msg.RecipientName)
			.Distinct()
			.Select(personRepo.GetByName)
			.WhereNotDefault()
			.ToImmutableDictionary(p => p.Name, Mapper.ToPersonModel);

		return messages.Select(msg => new MessageModel(
			msg.Id,
			msg.Message,
			sender.ToPersonModel(),
			recipients[msg.RecipientName],
			msg.Sent
		));
	}

	public MessageModel SendMessage(MessageDraftModel draft)
	{
		if (string.IsNullOrWhiteSpace(draft.Message))
		{
			throw new InvalidValueException(draft.Message, "empty or whitespace");
		}

		var sender = personRepo.GetByName(draft.From)?.ToPersonModel()
			?? throw new NonExistentValueException(nameof(draft.From), draft.From);

		var recipient = personRepo.GetByName(draft.To)?.ToPersonModel()
			?? throw new NonExistentValueException(nameof(draft.To), draft.To);

		var ent = msgRepo.AddMessage(new MessageDraftEntity(
			sender.Name,
			recipient.Name,
			draft.Message
		));

		return new MessageModel(
			ent.Id,
			ent.Message,
			sender,
			recipient,
			ent.Sent
		);
	}
}
