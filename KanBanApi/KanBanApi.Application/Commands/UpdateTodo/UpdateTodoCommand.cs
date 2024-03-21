using FastEndpoints;
using KanBanApi.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace KanBanApi.Application.Commands.UpdateTodo;

public record UpdateTodoCommand(string? Title, string? Message, string? Status) : ICommand<TodoResponse>
{
    [FromRoute]
    public string TodoId { get; set; } = null!;
};