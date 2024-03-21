using System.Linq.Expressions;
using System.Text.RegularExpressions;
using FastEndpoints;
using KanBanApi.Application.Dtos;
using KanBanApi.Application.Services;
using KanBanApi.Domain.Entities;
using Microsoft.AspNetCore.Identity;

using Microsoft.EntityFrameworkCore;

namespace KanBanApi.Application.Commands.CreateTodo;

public class CreateTodoCommandHandler(IUnitOfWork _unitOfWork, IActiveSession _activeSession, UserManager<IdentityUser> _userManager) : CommandHandler<CreateTodoCommand, TodoResponse>
{
    public override async Task<TodoResponse> ExecuteAsync(CreateTodoCommand command, CancellationToken ct = default)
    {
        var userId = _activeSession.UserId();
        if (userId is null) ThrowError("No valid active user");

        var user = await _userManager.FindByIdAsync(userId);
        if (user is null) ThrowError("No valid active user");

        var dueDate = GetDueDate(command.Message);
        var listName = GetListName(command.Message);

        var todoRepo = _unitOfWork.GetRepository<Todo>();
        var todoListRepo = _unitOfWork.GetRepository<TodoList>();

        var todoListFilters = new Expression<Func<TodoList, bool>>[] { f => f.ListName == listName };

        var todoList = await (await todoListRepo.GetAllListOnlyAsync(filters: todoListFilters)).FirstOrDefaultAsync();

        if (todoList is null && listName != null)
        {
            todoList = new TodoList { ListName = listName };
            await todoListRepo.SaveAsync(todoList);
        }

        var todo = new Todo
        {
            Title = command.Title,
            Message = command.Message,
            DueDate = dueDate,
            TodoListId = todoList?.Id,
            OwnerId = userId
        };

        await todoRepo.SaveAsync(todo);

        return new TodoResponse(todo.Id.ToString(),
                                todo.Status.ToString(),
                                todo.Title,
                                todo.Message,
                                todo.DueDate,
                                listName);
    }

    private DateOnly? GetDueDate(string message)
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

    private string? GetListName(string message)
    {
        var regex = new Regex("\\+[A-Za-z0-9]+");
        var matches = regex.Matches(message);

        if (matches.Count != 1) return null;

        return matches.First().Value.Substring(1).ToLower();
    }
}