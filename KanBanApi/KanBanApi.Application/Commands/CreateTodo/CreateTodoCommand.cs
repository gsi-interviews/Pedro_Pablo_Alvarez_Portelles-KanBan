using FastEndpoints;
using KanBanApi.Application.Dtos;

namespace KanBanApi.Application.Commands.CreateTodo;

public record CreateTodoCommand(string Title, string Message) : ICommand<TodoResponse>;