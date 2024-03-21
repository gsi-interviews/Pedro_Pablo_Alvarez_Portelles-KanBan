using FastEndpoints;
using KanBanApi.Application.Commands.CreateTodo;
using KanBanApi.Application.Dtos;

namespace KanBanApi.WebApi.Endpoints;

public class CreateTodoEndpoint : Endpoint<CreateTodoCommand, TodoResponse>
{
    public override void Configure()
    {
        Post("/todos");
    }

    public override async Task<TodoResponse> ExecuteAsync(CreateTodoCommand req, CancellationToken ct)
    {
        return await req.ExecuteAsync(ct);
    }
}