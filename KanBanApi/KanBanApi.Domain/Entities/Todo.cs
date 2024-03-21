using Microsoft.AspNetCore.Identity;

namespace KanBanApi.Domain.Entities;

public class Todo : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Message { get; set; } = null!;
    public DateOnly DueDate { get; set; }
    public TodoStatus Status { get; set; } = TodoStatus.Todo;

    public string OwnerId { get; set; } = null!;
    public IdentityUser Owner { get; set; } = null!;

    public Guid TodoListId { get; set; }
    public TodoList TodoList { get; set; } = null!;

    IQueryable<TodoHistory> TodoHistories { get; set; } = null!;
}