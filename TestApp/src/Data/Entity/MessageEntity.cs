namespace TestApp.Data.Entity;

public sealed record MessageEntity
(
	int Id,
	string SenderName,
	string RecipientName,
	string Message,
	DateTime Sent
);