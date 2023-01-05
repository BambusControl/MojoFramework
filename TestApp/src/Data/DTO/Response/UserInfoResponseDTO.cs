namespace TestApp.Data.DTO.Response;

public sealed record UserInfoResponseDto(
	string Name,
	DateOnly BirthDate,
	IEnumerable<string> Contacts,
	int MessagesRecieved,
	int MessagesSent
);
