namespace TestApp.Data.Model;

public sealed record MessageModel
(
	int Id,
	string Message,
	PersonModel Sender,
	PersonModel Recipient,
	DateTime Sent
);
