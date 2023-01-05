using TestApp.Data.Model;

namespace TestApp.Data.DTO.Response;

public sealed record InboxResponseDto(
	IEnumerable<MessageModel> ReceivedMessages,
	IEnumerable<MessageModel> SentMessages
);
