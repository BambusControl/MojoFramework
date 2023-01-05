namespace TestApp.Data.Entity;

public sealed record MessageDraftEntity
(
	string SenderName,
	string RecipientName,
	string Message
);
