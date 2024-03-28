using FastEndpoints;
using KanBanApi.Application.Dtos;

namespace KanBanApi.Application.Queries.ListTodos;

public record ListTodosCommand(string? ListName, DateOnly? DueDate) : ICommand<TodoListResponse>;