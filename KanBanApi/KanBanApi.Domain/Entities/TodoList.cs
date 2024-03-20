namespace KanBanApi.Domain.Entities;

public class TodoList : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string ListName { get; set; } = null!;
}