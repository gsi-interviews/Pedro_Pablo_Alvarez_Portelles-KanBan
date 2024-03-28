using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using FastEndpoints;
using KanBanApi.Application.Dtos;
using KanBanApi.Application.Services;
using KanBanApi.Domain.Entities;

namespace KanBanApi.Application.Queries.TodoHistory;

public class TodoHistoryCommandHandler(IUnitOfWork _unitOfWork, IActiveSession _activeSession) : CommandHandler<TodoHistoryCommand, IEnumerable<TodoHistoryResponse>>
{
    public override async Task<IEnumerable<TodoHistoryResponse>> ExecuteAsync(TodoHistoryCommand command, CancellationToken ct = default)
    {
        var userId = _activeSession.UserId();
        if (userId is null) ThrowError("No valid active user");

        var filters = new Expression<Func<Domain.Entities.TodoHistory, bool>>[] { f => f.Todo.OwnerId == userId, f => f.TodoId == Guid.Parse(command.TodoId) };
        var historyRepo = _unitOfWork.GetRepository<Domain.Entities.TodoHistory>();
        var todoHistory = (await historyRepo.GetAllListOnlyAsync(filters: filters))
                                            .OrderBy(f => f.Modified)
                                            .Select(x => new TodoHistoryResponse(command.TodoId, x.Status.ToString(), x.Title, x.Message, GetDueDate(x.Message), GetListName(x.Message), x.Modified))
                                            .ToArray();

        return todoHistory;
    }

    private static DateOnly? GetDueDate(string message)
    {
        var regex = new Regex("due\\s\\d{1,2}\\/\\d{1,2}\\/\\d{2,4}");

        var matches = regex.Matches(message);

        if (matches.Count != 1) return null;

        try
        {
            var date = matches.First().Value.Substring(4);
            return DateOnly.Parse(date);
        }
        catch
        {
            return null;
        }
    }

    private static string GetListName(string message)
    {
        var regex = new Regex("\\+[A-Za-z0-9]+");
        var matches = regex.Matches(message);

        if (matches.Count != 1) return "Default";

        return matches.First().Value.Substring(1).ToLower();
    }
}