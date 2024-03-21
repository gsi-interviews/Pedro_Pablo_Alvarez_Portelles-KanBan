using FastEndpoints;
using KanBanApi.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace KanBanApi.Application.Queries.TodoHistory;

public record TodoHistoryCommand : ICommand<IEnumerable<TodoHistoryResponse>>
{
    [FromRoute]
    public string TodoId { get; set; } = null!;
}