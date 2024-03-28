using FastEndpoints;
using KanBanApi.Application.Services;
using KanBanApi.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace KanBanApi.Application.Commands.DeleteTodo;

public class DeleteTodoCommandHandler(IUnitOfWork _unitOfWork, IActiveSession _activeSession, UserManager<IdentityUser> _userManager) : CommandHandler<DeleteTodoCommand>
{
    public override async Task ExecuteAsync(DeleteTodoCommand command, CancellationToken ct = default)
    {
        var userId = _activeSession.UserId();
        if (userId is null) ThrowError("No valid active user");

        var user = await _userManager.FindByIdAsync(userId);
        if (user is null) ThrowError("No valid active user");

        var todoRepo = _unitOfWork.GetRepository<Todo>();

        var todo = await todoRepo.GetByIdAsync(Guid.Parse(command.TodoId));

        if (todo is null || todo.OwnerId != userId) ThrowError("This todo does not exists");

        await todoRepo.DeleteAsync(todo);
    }
}