namespace KanBanApi.Application.Dtos;

public record TodoResponse(string Id, string Status, string Title, string Message, DateOnly? DueDate, string? List);