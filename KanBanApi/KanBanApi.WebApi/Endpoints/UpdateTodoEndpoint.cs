using FastEndpoints;
using KanBanApi.Application.Commands.UpdateTodo;
using KanBanApi.Application.Dtos;

namespace KanBanApi.WebApi.Endpoints;

public class UpdateTodoEndpoint : Endpoint<UpdateTodoCommand, TodoResponse>
{
    public override void Configure()
    {
        Patch("/todos/{todoId}");
    }

    public override async Task<TodoResponse> ExecuteAsync(UpdateTodoCommand req, CancellationToken ct)
    {
        return await req.ExecuteAsync(ct);
    }
}