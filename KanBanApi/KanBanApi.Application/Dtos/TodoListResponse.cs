namespace KanBanApi.Application.Dtos;

public record TodoListResponse(IEnumerable<TodoResponse> Todo, IEnumerable<TodoResponse> Doing, IEnumerable<TodoResponse> Review, IEnumerable<TodoResponse> Done);