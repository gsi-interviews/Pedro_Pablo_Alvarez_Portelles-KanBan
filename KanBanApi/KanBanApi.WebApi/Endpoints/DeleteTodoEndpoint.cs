using FastEndpoints;
using KanBanApi.Application.Commands.DeleteTodo;
using Microsoft.AspNetCore.Http.HttpResults;

namespace KanBanApi.WebApi.Endpoints;

public class DeleteTodoEndpoint : Endpoint<DeleteTodoCommand, NoContent>
{
    public override void Configure()
    {
        Delete("/todos/{todoId}");
    }

    public override async Task HandleAsync(DeleteTodoCommand req, CancellationToken ct)
    {
        await req.ExecuteAsync(ct);
        await SendNoContentAsync();
    }
}