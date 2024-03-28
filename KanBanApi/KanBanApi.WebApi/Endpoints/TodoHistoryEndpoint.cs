using FastEndpoints;
using KanBanApi.Application.Dtos;
using KanBanApi.Application.Queries.TodoHistory;

namespace KanBanApi.WebApi.Endpoints;

public class TodoHistoryEndpoint : Endpoint<TodoHistoryCommand, IEnumerable<TodoHistoryResponse>>
{
    public override void Configure()
    {
        Get("/todo-history/{todoId}");
    }

    public override async Task<IEnumerable<TodoHistoryResponse>> ExecuteAsync(TodoHistoryCommand req, CancellationToken ct)
    {
        return await req.ExecuteAsync(ct);
    }
}