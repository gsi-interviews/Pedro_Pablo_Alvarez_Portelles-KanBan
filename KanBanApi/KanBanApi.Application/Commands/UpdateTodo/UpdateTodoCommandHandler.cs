using System.Linq.Expressions;
using System.Text.RegularExpressions;
using FastEndpoints;
using KanBanApi.Application.Dtos;
using KanBanApi.Application.Services;
using KanBanApi.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KanBanApi.Application.Commands.UpdateTodo;

public class UpdateTodoCommandHandler(IUnitOfWork _unitOfWork, IActiveSession _activeSession, UserManager<IdentityUser> _userManager) : CommandHandler<UpdateTodoCommand, TodoResponse>
{
    public override async Task<TodoResponse> ExecuteAsync(UpdateTodoCommand command, CancellationToken ct = default)
    {
        var userId = _activeSession.UserId();
        if (userId is null) ThrowError("No valid active user");

        var user = await _userManager.FindByIdAsync(userId);
        if (user is null) ThrowError("No valid active user");

        var todoRepo = _unitOfWork.GetRepository<Todo>();

        var filters = new Expression<Func<Todo, bool>>[] { f => f.Id == Guid.Parse(command.TodoId) };
        var include = new Expression<Func<Todo, object>>[] { i => i.TodoList! };

        var todo = await (await todoRepo.GetAllListOnlyAsync(filters: filters, includes: include)).FirstOrDefaultAsync();

        if (todo is null || todo.OwnerId != userId) ThrowError("This todo does not exists");

        var todoHistoryRepo = _unitOfWork.GetRepository<TodoHistory>();
        await todoHistoryRepo.SaveAsync(new TodoHistory
        {
            Title = todo.Title,
            Message = todo.Title,
            TodoId = todo.Id,
            Modified = DateTime.UtcNow
        });

        var listName = todo.TodoList?.ListName;

        if (command.Title != null) todo.Title = command.Title;

        if (command.Message != null)
        {
            var dueDate = GetDueDate(command.Message);
            listName = GetListName(command.Message);

            var todoListRepo = _unitOfWork.GetRepository<TodoList>();

            var todoListFilters = new Expression<Func<TodoList, bool>>[] { f => f.ListName == listName };

            var todoList = await (await todoListRepo.GetAllListOnlyAsync(filters: todoListFilters)).FirstOrDefaultAsync();

            if (todoList is null && listName != null)
            {
                todoList = new TodoList { ListName = listName };
                await todoListRepo.SaveAsync(todoList);
            }

            todo.Message = command.Message;
            todo.DueDate = dueDate;
            todo.TodoListId = todoList?.Id;
        }

        if (command.Status != null)
        {
            var status = TodoStatus.Todo;

            switch (command.Status)
            {
                case "Todo": status = TodoStatus.Todo; break;
                case "Doing": status = TodoStatus.Doing; break;
                case "Review": status = TodoStatus.Review; break;
                case "Done": status = TodoStatus.Done; break;
            }
            todo.Status = status;
        }

        await todoRepo.UpdateAsync(todo);

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

        if (matches.Count != 1) return "Default";

        return matches.First().Value.Substring(1).ToLower();
    }
}