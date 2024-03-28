namespace KanBanApi.Application.Dtos;

public record TodoHistoryResponse(string Id, string Status, string Title, string Message, DateOnly? DueDate, string? List, DateTime Modified);