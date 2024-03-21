using System.Linq.Expressions;
using FastEndpoints;
using KanBanApi.Application.Dtos;
using KanBanApi.Application.Services;
using KanBanApi.Domain.Entities;

namespace KanBanApi.Application.Queries.ListTodos;

public class ListTodosCommandHandler(IUnitOfWork _unitOfWork, IActiveSession _activeSession) : CommandHandler<ListTodosCommand, TodoListResponse>
{
    public override async Task<TodoListResponse> ExecuteAsync(ListTodosCommand command, CancellationToken ct = default)
    {
        var userId = _activeSession.UserId();
        if (userId is null) ThrowError("No valid active user");

        var includes = new Expression<Func<Todo, object>>[] { i => i.TodoList! };
        var filters = new Expression<Func<Todo, bool>>[] { f => f.OwnerId == userId };

        if (command.ListName != null) filters.Append(f => f.TodoList!.ListName == command.ListName);
        if (command.DueDate != null) filters.Append(f => f.DueDate! <= command.DueDate);

        var todoRepo = _unitOfWork.GetRepository<Todo>();
        var todos = (await todoRepo.GetAllListOnlyAsync(filters: filters))
                                   .Select(x => new TodoResponse(x.Id.ToString(), x.Status.ToString(), x.Title, x.Message, x.DueDate, x.TodoList!.ListName))
                                   .ToArray();

        var response = new TodoListResponse(
            todos.Where(x => x.Status == TodoStatus.Todo.ToString()).ToArray(),
            todos.Where(x => x.Status == TodoStatus.Doing.ToString()).ToArray(),
            todos.Where(x => x.Status == TodoStatus.Review.ToString()).ToArray(),
            todos.Where(x => x.Status == TodoStatus.Done.ToString()).ToArray()
        );

        return response;
    }
}