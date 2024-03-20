namespace KanBanApi.Domain.Entities;

public class TodoHistory : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Message { get; set; } = null!;

    public Guid TodoId { get; set; }
    public Todo Todo { get; set; } = null!;
}