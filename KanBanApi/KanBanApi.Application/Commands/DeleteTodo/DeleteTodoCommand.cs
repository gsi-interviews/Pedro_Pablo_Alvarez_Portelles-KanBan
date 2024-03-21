using FastEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace KanBanApi.Application.Commands.DeleteTodo;

public record DeleteTodoCommand : ICommand
{
    [FromRoute]
    public string TodoId { get; set; } = null!;
}