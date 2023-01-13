using MojoFramework.Attributes.Configuration;
using TestApp.Data.Entity;

namespace TestApp.Logic.Repository;

[Component]
public sealed class MessageRepository
{
	private readonly List<MessageEntity> messages;
	private int idSeq;

	public MessageRepository()
	{
		idSeq = 0;
		var now = DateTime.Now;
		messages = new List<MessageEntity>
		{
			new(GenerateId(), "Scarlett", "Josh", "Hello", now - new TimeSpan(hours: 18, minutes: 10, seconds: 23)),
			new(GenerateId(), "Scarlett", "Josh", "How are you doing",
				now - new TimeSpan(hours: 18, minutes: 10, seconds: 18)),
			new(GenerateId(), "Josh", "Scarlett", "I'm great", now - new TimeSpan(hours: 18, minutes: 8, seconds: 53)),
			new(GenerateId(), "Scarlett", "Nancy", "Hello from the other side",
				now - new TimeSpan(hours: 10, minutes: 2, seconds: 11)),
		};
	}

	private int GenerateId()
	{
		return idSeq++;
	}

	public MessageEntity? GetById(int id)
	{
		return messages.FirstOrDefault(message => message.Id == id);
	}

	public IEnumerable<MessageEntity> GetBySenderName(string senderName)
	{
		return messages.Where(message => message.SenderName == senderName);
	}

	public IEnumerable<MessageEntity> GetByRecipientName(string recipientName)
	{
		return messages.Where(message => message.RecipientName == recipientName);
	}

	public MessageEntity AddMessage(MessageDraftEntity draft)
	{
		var message = new MessageEntity(
			GenerateId(),
			draft.SenderName,
			draft.RecipientName,
			draft.Message,
			DateTime.Now
		);

		messages.Add(message);

		return message;
	}
}
