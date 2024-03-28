using FastEndpoints;
using KanBanApi.Application.Dtos;
using KanBanApi.Application.Queries.ListTodos;

namespace KanBanApi.WebApi.Endpoints;

public class ListTodosEndpoint : Endpoint<ListTodosCommand, TodoListResponse>
{
    public override void Configure()
    {
        Get("/todos");
    }

    public override async Task<TodoListResponse> ExecuteAsync(ListTodosCommand req, CancellationToken ct)
    {
        return await req.ExecuteAsync(ct);
    }
}